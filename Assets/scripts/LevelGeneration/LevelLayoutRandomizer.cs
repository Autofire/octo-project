using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace Level {
	public class LevelLayoutRandomizer : MonoBehaviour {

		[System.Serializable]
		public class RoomBucket {
			public RoomPurpose purpose;
			public GameObject[] rooms;
		}

		[Header("Randomization settings")]
		[Range(1, 10)] public int seedCount = 1;
		[Range(1, 10)] public int minBranchLength = 2;
		[Range(1, 10)] public int maxBranchLength = 6;
		[Range(1,  4)] public int minSeedBranches = 2;
		[Range(1,  4)] public int maxSeedBranches = 4;

		[Header("Room spawning")]
		public Vector2 roomSize;
		//public GameObject roomPrefab;
		public RoomBucket[] roomBuckets;

		[Header("Debug")]
		public bool debugGeneration = false;



		public LevelLayout Layout {
			get; private set;
		}

		private HashSet<Vector2Int> branchEnds;
		private HashSet<Vector2Int> seeds;

		public void LoadNextLevel() {
			StartCoroutine(NextLevelCoroutine());
		}

		private IEnumerator NextLevelCoroutine() {
			yield return DespawnLevel();
			Generate();
			SpawnLevel();
		}

		/// <summary>
		/// Creates the layout for the random level. 
		/// </summary>
		private void Generate() {
			Layout = new LevelLayout();

			branchEnds = new HashSet<Vector2Int>();
			seeds = new HashSet<Vector2Int>();

			Layout.AddRoom(Vector2Int.zero);
			branchEnds.Add(Vector2Int.zero);

			int remainingSeeds = seedCount;
			while(remainingSeeds > 0) {
				Vector2Int[] seedCandidateArray = branchEnds.ToArray();

				// There's a slim but real chance that no more valid seedCandidatesExist.
				// If this occurs, we'll have to make some up.
				if(seedCandidateArray.Length == 0) {
					seedCandidateArray = Layout.RoomsWithMissingNeighbors();
				}

				PlantSeed(seedCandidateArray[ Random.Range(0, seedCandidateArray.Length) ]);

				remainingSeeds--;
			}

			// Now to make all the remaining branch ends have a special purpose.
			List<Vector2Int> pointsOfInterest; //new List<Vector2Int>(seeds.Union(branchEnds));

			if(branchEnds.Count < 2) {
				// Since we have less than two branchEnds (somehow?), we'll include seeds too.
				pointsOfInterest = new List<Vector2Int>(seeds.Union(branchEnds));
			}
			else {
				// We have enough branch ends, so we'll just use those.
				pointsOfInterest = new List<Vector2Int>(branchEnds);
			}

			// First, we'll select a room for the entry and exit.
			int randomIndex = Random.Range(0, pointsOfInterest.Count);
			Layout.GetRoom(pointsOfInterest[randomIndex]).Purpose = RoomPurpose.Entry;
			pointsOfInterest.RemoveAt(randomIndex);

			randomIndex = Random.Range(0, pointsOfInterest.Count);
			Layout.GetRoom(pointsOfInterest[randomIndex]).Purpose = RoomPurpose.Exit;
			pointsOfInterest.RemoveAt(randomIndex);

			// Then we'll mark the rest of the rooms of interest as being special.
			foreach(Vector2Int point in pointsOfInterest) {
				Layout.GetRoom(point).Purpose = RoomPurpose.Special;
			}


			if(debugGeneration) {
				Debug.Log("Branch ends: " + string.Join(", ", branchEnds.Select(x => x.ToString()).ToArray()));
				Debug.Log("Seeds: " + string.Join(", ", seeds.Select(x => x.ToString()).ToArray()));
				Debug.Log(Layout.ToString());
			}
		}



		/// <summary>
		/// This will plant a seed and grow it in one step. It will also
		/// update the seeds and branchEnds sets accordingly. If the given
		/// location is a branch end, it's removed from the branch ends list.
		///
		/// This will assume that there already is a room at the given location.
		/// This also assumes that the given location is a branch end.
		/// </summary>
		private void PlantSeed(Vector2Int location) {
			branchEnds.Remove(location);
			seeds.Add(location);

			int branchCount = Random.Range(minSeedBranches, maxSeedBranches + 1);
			List<Direction> branchDirs = Direction.None.OtherDirections().ToList();
			while(branchDirs.Count > branchCount) {
				branchDirs.RemoveAt(Random.Range(0, branchDirs.Count));
			}
			//Debug.Log(branchDirs.Count + " branch directions (" + string.Join(", ", branchDirs.Select(x => x.ToString()).ToArray()) + ")");

			foreach(Direction dir in branchDirs) {
				Vector2Int endPoint = GrowBranch(location, Random.Range(minBranchLength, maxBranchLength + 1), dir);

				if(!branchEnds.Contains(endPoint)) {
					branchEnds.Add(endPoint);
				}
			}
		}

		/// <summary>
		/// This'll create a branch. It will not cross over itself,
		/// and considers the start point part of itself.
		/// </summary>
		/// <param name="startPoint"></param>
		/// <param name="dir"></param>
		/// <returns>The end point. If length is zero, this is the startPoint.</returns>
		private Vector2Int GrowBranch(Vector2Int startPoint, int length, Direction dir) {

			HashSet<Vector2Int> path = new HashSet<Vector2Int>();
			path.Add(startPoint);

			Vector2Int endPoint = startPoint;

			// Only do anything if we're allowed to make at least one thing.
			if(length >= 1) {

				endPoint += dir.ToVector();
				Layout.AddRoom(endPoint);
				Layout.ConnectRooms(startPoint, endPoint);
				path.Add(endPoint);

				// Set this to true if we can't grow for any reason.
				bool stuck = false;

				// Subtract one because we start with the start point in there.
				while(path.Count - 1 < length && !stuck) {
					List<Vector2Int> adjacentPoints = LevelLayout.GetAdjacentCoordinates(endPoint).ToList();

					// We want to choose a direction, but we'll give up if we run out of points.
					Vector2Int? chosenLocation = null;
					while(!chosenLocation.HasValue && adjacentPoints.Count > 0) {
						int randomIndex = Random.Range(0, adjacentPoints.Count);

						// Basically, if this branch hasn't selected this spot before
						if(!path.Contains(adjacentPoints[randomIndex])) {
							chosenLocation = adjacentPoints[randomIndex];
						}
						else {
							adjacentPoints.RemoveAt(randomIndex);
						}
					}

					if(!chosenLocation.HasValue) {
						stuck = true;
					}
					else {

						Layout.AddRoom(chosenLocation.Value);
						Layout.ConnectRooms(endPoint, chosenLocation.Value);
						path.Add(chosenLocation.Value);

						endPoint = chosenLocation.Value;
					}
				} // while(path.Count -1 < length && !stuck)
			}

			if(debugGeneration) {
				Debug.Log("Path taken: " + string.Join(", ", path.Select(x => x.ToString()).ToArray()));
			}

			return endPoint;
		}



		/// <summary>
		/// Spawns a previously generated level.
		/// </summary>
		private void SpawnLevel() {
			foreach(IRoom r in Layout.Rooms) {
				Vector2 roomWorldPos = transform.position;
				roomWorldPos.x += roomSize.x * r.Position.x;
				roomWorldPos.y += roomSize.y * r.Position.y;

				RoomBucket bucket = roomBuckets.FirstOrDefault((b) => b.purpose == r.Purpose);

				if(bucket != null && bucket.rooms.Length > 0) {
					GameObject roomPrefab = bucket.rooms[Random.Range(0, bucket.rooms.Length)];
					GameObject newRoom = Instantiate(roomPrefab, roomWorldPos, roomPrefab.transform.rotation, transform) as GameObject;

					RoomComponent rComp = newRoom.GetComponent<RoomComponent>();
					rComp.room = r as Room;
				}
				else {
					Debug.LogError("Missing/empty bucket for " + r.Purpose);
				}
			}
		}

		/// <summary>
		/// Despawns a previously spawned level.
		/// </summary>
		private IEnumerator DespawnLevel() {
			foreach(Transform child in transform) {
				Destroy(child.gameObject);
			}

			yield return new WaitUntil(() => transform.childCount == 0);
		}


	} // End class
} // End namespace
