using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Level {

	public interface IRoom {
		bool[] Openings { get; }
		Vector2Int Position { get; }
		RoomPurpose Purpose { get; set; }
	}

	public interface IRoomFactory {
		IRoom MakeRoom(Vector2Int position);
	}

	public enum RoomPurpose {
		Normal, Entry, Exit, Special
	}

	[System.Serializable]
	public class Room : IRoom {
		[SerializeField] private bool[] _openings;
		[SerializeField] private Vector2Int _position;
		[SerializeField] private RoomPurpose _purpose;

		public bool[] Openings {
			get {
				return _openings;
			}
		}

		public Vector2Int Position {
			get {
				return _position;
			}
		}

		// Originally, this went in a separate inheritor of the this Room object.
		// However, doing so meant doing all sorts of messy casts. It was
		// more convenient to do it a like this for now.
		//
		// TODO If this gets reused, strip this out and come up with a better solution.
		public RoomPurpose Purpose {
			get {
				return _purpose;
			}
			set {
				_purpose = value;
			}
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


		private Room() {

		}

		protected Room(Vector2Int position) {
			_position = position;
			_openings = new bool[(int) Direction.Count];
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
				result += " (" + string.Join(", ", OpeningDirections.Select(x => x.ToString()).ToArray()) + ")";
			}

			if(Purpose != RoomPurpose.Normal) {
				result += " (" + Purpose + ")";
			}

			return result;
		}
	}


}
