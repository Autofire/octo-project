using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Level {
	public class Layout {
		private Dictionary<Vector2Int, Room> Rooms {
			get;
		}

		public Layout() {
			Rooms = new Dictionary<Vector2Int, Room>();
		}

		/// <summary>
		/// Gets the number of rooms.
		/// </summary>
		public int Count {
			get {
				return Rooms.Count;
			}
		}

		/// <summary>
		/// Returns true if a room exists at the location.
		/// </summary>
		/// <param name="location">Location to check.</param>
		/// <returns>True if there is a room present.</returns>
		public bool RoomExists(Vector2Int location) {
			return Rooms.ContainsKey(location);
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

			Room room1 = GetRoom(location1);
			Room room2 = GetRoom(location2);

			if(room1 == null || room2 == null) {
				throw new ArgumentOutOfRangeException("Cannot form connection between null rooms");
			}

			room1.Openings[(int) GetDirection(location1, location2)] = true;
			room2.Openings[(int) GetDirection(location2, location1)] = true;
		}

		/// <summary>
		/// Creates a new room at the given location, assuming it isn't already taken.
		/// </summary>
		/// <param name="location">The location for the new room.</param>
		/// <returns>The new room.</returns>
		/// <exception cref="ArgumentException">Thrown if the location is already taken.</exception>
		public Room AddRoom(Vector2Int location) {
			Room newRoom = new Room(location);
			Rooms.Add(location, newRoom);

			return newRoom;
		}

		/// <summary>
		/// Gets the room at a location, or null if there is no room there.
		/// </summary>
		/// <param name="location">The location of the room.</param>
		/// <returns>The room or null.</returns>
		public Room GetRoom(Vector2Int location) {
			return (RoomExists(location) ? Rooms[location] : null);
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
		public Room[] GetAdjacent(Vector2Int center) {
			Vector2Int[] adjCoords = GetAdjacentCoordinates(center);
			Room[] result = new Room[adjCoords.Length];

			for(int i = 0; i < result.Length; i++) {
				result[i] = GetRoom(adjCoords[i]);
			}

			return result;
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
			return VectorToDirection(to - from);
		}

		/// <summary>
		/// Figures out the direction of a unit vector.
		/// </summary>
		/// <param name="unitVector">Vector to check.</param>
		/// <returns>The direction.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Thrown if the vector passed in is not a unit vector.</exception>
		public static Direction VectorToDirection(Vector2Int unitVector) {

			if(unitVector == Vector2Int.left) {
				return Direction.Left;
			}
			else if(unitVector == Vector2Int.right) {
				return Direction.Right;
			}
			else if(unitVector == Vector2Int.up) {
				return Direction.Up;
			}
			else if(unitVector == Vector2Int.down) {
				return Direction.Down;
			}
			else {
				throw new ArgumentOutOfRangeException("Can only convert unit vectors");
			}
		}
	}

}
