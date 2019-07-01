using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Level {
	public class LevelLayout {
		private Dictionary<Vector2Int, IRoom> _rooms;
		private IRoomFactory roomFactory;

		public LevelLayout(IRoomFactory factory) {
			roomFactory = factory;
			_rooms = new Dictionary<Vector2Int, IRoom>();
		}

		public LevelLayout() : this(new Room.Factory()) { }

		public IRoom[] Rooms {
			get { return _rooms.Values.ToArray(); }
		}
		
		public Vector2Int[] Coordinates {
			get { return _rooms.Keys.ToArray(); }
		}

		/// <summary>
		/// Gets the number of rooms.
		/// </summary>
		public int Count {
			get {
				return _rooms.Count;
			}
		}

		/// <summary>
		/// Returns true if a room exists at the location.
		/// </summary>
		/// <param name="location">Location to check.</param>
		/// <returns>True if there is a room present.</returns>
		public bool RoomExists(Vector2Int location) {
			return _rooms.ContainsKey(location);
		}

		/// <summary>
		/// Connects two previously existing, adjacent rooms.
		/// 
		/// This operation is indempotent.
		/// </summary>
		/// <param name="location1">Location of room one.</param>
		/// <param name="location2">Location of room two.</param>
		/// <exception cref="ArgumentOutOfRangeException">Thrown if rooms aren't adjacent or rooms are null.</exception>
		public void ConnectRooms(Vector2Int location1, Vector2Int location2) {

			IRoom room1 = GetRoom(location1);
			IRoom room2 = GetRoom(location2);

			if(room1 == null || room2 == null) {
				throw new ArgumentOutOfRangeException("Cannot form connection between null rooms");
			}

			room1.Openings[(int) GetDirection(location1, location2)] = true;
			room2.Openings[(int) GetDirection(location2, location1)] = true;
		}

		/// <summary>
		/// Creates a new room at the given location, assuming it isn't already taken.
		/// If it is, the room is simply returned. (i.e. this is indempotent)
		/// </summary>
		/// <param name="location">The location for the new room.</param>
		/// <returns>The new room.</returns>
		public IRoom AddRoom(Vector2Int location) {
			IRoom result = GetRoom(location);

			if(result == null) {
				result = roomFactory.MakeRoom(location); //new Room(location);
				_rooms.Add(location, result);
			}

			return result;
		}

		/// <summary>
		/// Gets the room at a location, or null if there is no room there.
		/// </summary>
		/// <param name="location">The location of the room.</param>
		/// <returns>The room or null.</returns>
		public IRoom GetRoom(Vector2Int location) {
			return (RoomExists(location) ? _rooms[location] : null);
		}

		/// <summary>
		/// Constructs an array of rooms adjacent to the given room coordinate.
		///
		/// This array can be indexed into it using the Direction enum. Empty rooms
		/// will return null. There doesn't need to be a room at the given coordinates
		/// for this to work.
		/// </summary>
		/// <param name="center">The "center," from which we'll get the adjacent cells.</param>
		/// <returns>The array of rooms.</returns>
		public IRoom[] GetAdjacent(Vector2Int center) {
			Vector2Int[] adjCoords = GetAdjacentCoordinates(center);
			IRoom[] result = new Room[adjCoords.Length];

			for(int i = 0; i < result.Length; i++) {
				result[i] = GetRoom(adjCoords[i]);
			}

			return result;
		}

		/// <summary>
		/// Gets a list of rooms with null neighbors. This should always have something
		/// unless there are no rooms at all.
		/// </summary>
		/// <returns>The array of rooms.</returns>
		public Vector2Int[] RoomsWithMissingNeighbors() {
			Stack<Vector2Int> rooms = new Stack<Vector2Int>();
			foreach(Vector2Int coord in _rooms.Keys) {
				if(GetAdjacent(coord).Contains(null)) {
					rooms.Push(coord);
				}
			}

			return rooms.ToArray();
		}

		/// <summary>
		/// Gets a list of coordinates adjacent to a given coordinate.
		/// </summary>
		/// <param name="center">Center of the four points.</param>
		/// <returns>List of coordinates.</returns>
		public static Vector2Int[] GetAdjacentCoordinates(Vector2Int center) {
			Vector2Int[] result = new Vector2Int[(int) Direction.Count];

			result[(int) Direction.Up] = center + Vector2Int.up;
			result[(int) Direction.Down] = center + Vector2Int.down;
			result[(int) Direction.Right] = center + Vector2Int.right;
			result[(int) Direction.Left] = center + Vector2Int.left;

			return result;
		}

		/// <summary>
		/// Checks if two points are adjacent.
		/// </summary>
		/// <returns>True if adjacent.</returns>
		public static bool AreAdjacent(Vector2Int point1, Vector2Int point2) {
			return GetAdjacentCoordinates(point1).Contains(point2);
		}

		/// <summary>
		/// Calculates the direction between two adjacent coordinates.
		/// </summary>
		/// <param name="from">Source point.</param>
		/// <param name="to">Destination point.</param>
		/// <returns>The direction.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Thrown if coordinates aren't adjacent.</exception>
		public static Direction GetDirection(Vector2Int from, Vector2Int to) {
			return (to - from).ToDirection();
		}

		public override string ToString() {
			return string.Join(
				"\n",
				_rooms.Values.Select(x => x.ToString()).ToArray()
			);
		}
	}

}
