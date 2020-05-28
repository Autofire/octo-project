using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReachBeyond.Music {

	[CreateAssetMenu(fileName = "NewTrack", menuName = "ReachBeyond/Music/Track")]
	public class Track : ScriptableObject {

#pragma warning disable CS0649
		[SerializeField] private AudioClip _trackStart;
		[SerializeField] private AudioClip _trackBody;
#pragma warning restore CS0649

		public AudioClip TrackStart => _trackStart;
		public AudioClip TrackBody => _trackBody;

	}
}
