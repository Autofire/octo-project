using UnityEngine;

namespace Level {
	public class Room {

		public bool[] Openings {
			get;
		}
		public Vector2Int Position {
			get;
		}

		public Room(Vector2Int position) {
			Position = position;
			Openings = new bool[(int) Direction.Count];
		}

	}

}
