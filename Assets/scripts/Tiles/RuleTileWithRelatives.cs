using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
[CreateAssetMenu(fileName = "NewRuleTileWithRelatives", menuName = "Tiles/Custom/Rule Tile (w/ Relatives)")]
public class RuleTileWithRelatives : RuleTile {

	public TileBase[] relatives;

	public override bool RuleMatch(int neighbor, TileBase tile) {
		if(tile != null && relatives.Contains(tile)) {
			tile = m_Self;
		}

		return base.RuleMatch(neighbor, tile);
	}
}