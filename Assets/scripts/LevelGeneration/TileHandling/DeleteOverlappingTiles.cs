using UnityEngine;
using UnityEngine.Tilemaps;

namespace Level.Tiles {

	public class DeleteOverlappingTiles : MonoBehaviour {

		public Tilemap overlay;
		public Tilemap deletionTarget;

		private void Start() {
			//TileBase[] tiles = overlay.GetTilesBlock(overlay.cellBounds);

			foreach(Vector3Int pos in overlay.cellBounds.allPositionsWithin) {
				Debug.Log(pos + ": " + overlay.GetTile(pos)?.name);
				//Debug.Log(pos + " -> " + overlay.CellToWorld(pos) + " -> " + deletionTarget.WorldToCell(overlay.CellToWorld(pos)));

				if(overlay.GetTile(pos)?.name == "UpArrow") {
					Vector3Int cellToDelete = deletionTarget.WorldToCell(overlay.CellToWorld(pos));
					deletionTarget.SetTile(cellToDelete, null);
				}
			}
		}

	} // End class
} // End namespace
