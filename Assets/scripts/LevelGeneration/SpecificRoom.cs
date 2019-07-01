using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level {

	public enum RoomPurpose {
		Normal, Boss, Loot
	}

	public class SpecificRoom : Room {

		public RoomPurpose Purpose {
			get; set;
		}

		protected SpecificRoom(Vector2Int position) : base(position) {
			Purpose = RoomPurpose.Normal;
		}

		public new class Factory : IRoomFactory {

			public IRoom MakeRoom(Vector2Int position) {
				return new SpecificRoom(position);
			}
		}

	} // End class
} // End namespace
