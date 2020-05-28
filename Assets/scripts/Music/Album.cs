using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReachBeyond.VariableObjects;

namespace ReachBeyond.Music {

	[CreateAssetMenu(fileName = "NewAlbum", menuName = "ReachBeyond/Music/Album")]
	public class Album : ScriptableObject {

#pragma warning disable CS0649
		[SerializeField] private StringConstReference _artist;
		[SerializeField] private Track[] _tracks;
#pragma warning disable CS0649

		public string Artist => _artist.ConstValue;
		public Track[] Tracks => _tracks;

	}

}
