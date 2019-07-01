using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Level {
	public enum Direction : int {
		Up = 0, Down, Right, Left, Count, None
	}

	public static class DirectionMethods {

		/// <summary>
		/// Figures out the direction of a unit vector.
		/// </summary>
		/// <param name="v">Vector to check.</param>
		/// <returns>The direction.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Thrown if the vector passed in is not a unit vector.</exception>
		public static Direction ToDirection(this Vector2Int v) {

			if(v == Vector2Int.left) {
				return Direction.Left;
			}
			else if(v == Vector2Int.right) {
				return Direction.Right;
			}
			else if(v == Vector2Int.up) {
				return Direction.Up;
			}
			else if(v == Vector2Int.down) {
				return Direction.Down;
			}
			else {
				throw new ArgumentOutOfRangeException("Can only convert unit vectors");
			}
		}

		public static Vector2Int ToVector(this Direction dir) {
			Vector2Int result = Vector2Int.zero;
			switch(dir) {
				case Direction.Up:
					result = Vector2Int.up;
					break;
				case Direction.Down:
					result = Vector2Int.down;
					break;
				case Direction.Right:
					result = Vector2Int.right;
					break;
				case Direction.Left:
					result = Vector2Int.left;
					break;
				default:
					throw new ArgumentOutOfRangeException("Cannot convert direction to vector: " + dir);
			}

			return result;
		}

		public static Direction[] OtherDirections(this Direction dir) {
			List<Direction> branchDirs = new Direction[] { Direction.Up, Direction.Down, Direction.Right, Direction.Left }.ToList();

			branchDirs.Remove(dir);

			return branchDirs.ToArray();
		}

		public static Direction Opposite(this Direction dir) {
			switch(dir) {
				case Direction.Up:    return Direction.Down;
				case Direction.Down:  return Direction.Up;
				case Direction.Right: return Direction.Left;
				case Direction.Left:  return Direction.Right;
				default:
					throw new ArgumentOutOfRangeException("Cannot convert direction to vector: " + dir);
			}
		}
	} // End class
} // End namespace
