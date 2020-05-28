using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using ReachBeyond.VariableObjects;

namespace ReachBeyond.Music {

	// Note that this is a specialized version of the music system,
	// so do NOT copy stuff out of here.
	public class MusicManager : MonoBehaviour {

		[System.Serializable]
		public class MusicChannel {
			public TrackConstReference track;
			public AudioSource bodySource;

			[HideInInspector]
			public AudioSource startSource;
		}

		[Tooltip("Set true to keep make this game object stick around between scenes")]
		public bool persistent = false;
		public bool playOnStart = true;
		public MusicChannel[] channels;

		private void Awake() {
			foreach(MusicChannel channel in channels) {
				AudioSource bodySource = channel.bodySource;
				Assert.IsNotNull(bodySource);

				Assert.IsFalse(bodySource.gameObject == gameObject);
				AudioSource startSource = Instantiate(bodySource, bodySource.gameObject.transform.parent) as AudioSource;

				channel.startSource = startSource;

				startSource.clip = channel.track.ConstValue.TrackStart;
				bodySource.clip = channel.track.ConstValue.TrackBody;
			}

			if(persistent) {
				DontDestroyOnLoad(gameObject);
			}
		}

		private void Start() {
			if(playOnStart) {
				Play();
			}
		}

		/*
		public void SetVolume(float volume) {
			loopStartSource.volume = volume;
			loopBodySource.volume = volume;
		}
		*/

		/*
		public void ApplyCurrentTrack() {
			Stop();

			//playingTrack = currentTrack;

			//loopStartSource.clip = playingTrack?.TrackStart;
			//loopBodySource.clip = playingTrack?.TrackBody;
		}
		*/


		/// <summary>
		/// This doesn't apply the current track, nor does it change the volume.
		/// It simply loads up the new clips and tells the sources to play.
		/// </summary>
		private void Play() {
			foreach(MusicChannel channel in channels) {
				AudioSource startSource = channel.startSource;
				AudioSource bodySource = channel.bodySource;

				float startClipLength = (startSource.clip != null ? startSource.clip.length : 0f);

				startSource.Play();
				bodySource.PlayDelayed(startClipLength);
			}
		}

		private void Stop() {
			foreach(MusicChannel channel in channels) {
				channel.startSource.Stop();
				channel.bodySource.Stop();
			}
		}

	}
}
