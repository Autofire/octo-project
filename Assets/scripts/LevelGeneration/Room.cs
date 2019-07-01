using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Level {

	public interface IRoom {
		bool[] Openings { get; }
		Vector2Int Position { get; }
	}

	public interface IRoomFactory {
		IRoom MakeRoom(Vector2Int position);
	}

	public class Room : IRoom {

		public bool[] Openings {
			get;
		}

		public Direction[] OpeningDirections {
			get {
				Stack<Direction> result = new Stack<Direction>();

				for(int i = 0; i < Openings.Length; i++) {
					if(Openings[i]) {
						result.Push((Direction) i);
					}
				}

				return result.ToArray();
			}
		}

		public Vector2Int Position {
			get;
		}

		private Room() {

		}

		protected Room(Vector2Int position) {
			Position = position;
			Openings = new bool[(int) Direction.Count];
		}


		public class Factory : IRoomFactory {

			public IRoom MakeRoom(Vector2Int position) {
				return new Room(position);
			}
		}

		public override string ToString() {
			string result = Position.ToString();

			if(!Openings.Contains(true)) {
				result += " (no openings)";
			}
			else {
				result += " (" + string.Join(", ", OpeningDirections.Select(x => x.ToString()).ToArray())  + ")";
			}

			return result;
		}
	}


}
