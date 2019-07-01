using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutGenerator : MonoBehaviour
{
	public enum Direction : int {
		North=0, South, East, West
	}
	public const int DirectionCount = 4;

	/// <summary>
	/// Objects of this type represent a row-column pair.
	/// The only reason this is 40 lines long is to make
	/// it work with all of the C# data structures. 
	/// </summary>
	public struct RoomCoordinates : IEquatable<RoomCoordinates> {
		public int X {
			get;
		}

		public int Y {
			get;
		}

		public RoomCoordinates(int x, int y) {
			X = x;
			Y = y;
		}

		public RoomCoordinates[] AdjacentCoordinates() {
			// North, Sound, East, West
			return new RoomCoordinates[] {
				new RoomCoordinates()
			};
		}

		public override bool Equals(object obj) {
			return obj is RoomCoordinates && Equals((RoomCoordinates) obj);
		}

		public bool Equals(RoomCoordinates other) {
			return X == other.X &&
				   Y == other.Y;
		}

		public override int GetHashCode() {
			var hashCode = 1084646500;

			// Thought this is mostly the default hash code method, I have pulled some advice from
			// a blog post, wrapping the math in an unchecked block. Source:
			// https://www.loganfranken.com/blog/692/overriding-equals-in-c-part-2/
			unchecked {
				hashCode = hashCode * -1521134295 + X.GetHashCode();
				hashCode = hashCode * -1521134295 + Y.GetHashCode();
			}

			return hashCode;
		}

		public static bool operator ==(RoomCoordinates coordinates1, RoomCoordinates coordinates2) {
			return coordinates1.Equals(coordinates2);
		}

		public static bool operator !=(RoomCoordinates coordinates1, RoomCoordinates coordinates2) {
			return !(coordinates1 == coordinates2);
		}
	}

	public class Room {

		public bool[] Openings { get; }
		public Vector2Int Position { get; }

		public Room(Vector2Int position) {
			Position = position;
			Openings = new bool[DirectionCount];
		}

	}

	public class Layout {
		private Dictionary<Vector2Int, Room> Rooms { get; }

		public Layout() {
			Rooms = new Dictionary<Vector2Int, Room>();
		}

		/// <summary>
		/// Gets the number of rooms.
		/// </summary>
		public int Count {
			get { return Rooms.Count; }
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

		public static Vector2Int[] GetAdjacentCoordinates(Vector2Int center) {
			Vector2Int[] result = new Vector2Int[DirectionCount];

			result[(int)Direction.North] = center + Vector2Int.up;
			result[(int)Direction.South] = center + Vector2Int.down;
			result[(int)Direction.East] = center + Vector2Int.right;
			result[(int)Direction.West] = center + Vector2Int.left;

			return result;
		}
	}

	private void Start() {

	}

}
