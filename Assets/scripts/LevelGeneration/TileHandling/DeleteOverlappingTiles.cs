using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Tilemaps;
using Level;
using System.Linq;

namespace Level.Tiles {

	[RequireComponent(typeof(RoomComponent))]
	public class DeleteOverlappingTiles : MonoBehaviour {

		public Tilemap overlay;
		public Tilemap deletionTarget;
		public TileBase tileType;
		public EditorDirection associatedDirection;

		private RoomComponent roomComp;

		private void Awake() {
			Assert.IsNotNull(overlay);
			Assert.IsNotNull(deletionTarget);
			Assert.IsNotNull(tileType);

			roomComp = GetComponent<RoomComponent>();
			Assert.IsNotNull(roomComp);
		}

		private void Start() {
			//TileBase[] tiles = overlay.GetTilesBlock(overlay.cellBounds);

			if(roomComp.room.OpeningDirections.Contains(associatedDirection.ToDirection())) {

				foreach(Vector3Int pos in overlay.cellBounds.allPositionsWithin) {
					Debug.Log(pos + ": " + overlay.GetTile(pos)?.name);
					//Debug.Log(pos + " -> " + overlay.CellToWorld(pos) + " -> " + deletionTarget.WorldToCell(overlay.CellToWorld(pos)));

					if(overlay.GetTile(pos)?.name == tileType.name) {
						Vector3Int cellToDelete = deletionTarget.WorldToCell(overlay.CellToWorld(pos));
						deletionTarget.SetTile(cellToDelete, null);
					}
				}
			}
		}

	} // End class
} // End namespace
