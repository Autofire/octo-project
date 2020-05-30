using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using ReachBeyond.VariableObjects;

namespace ReachBeyond.Music {

	// Note that this is a specialized version of the music system,
	// so do NOT copy stuff out of here.
	public class MusicManager : MonoBehaviour {

		[Tooltip("Set true to keep make this game object stick around between scenes")]
		public bool persistent = false;
		public bool playOnStart = true;
		public AudioSource startSource;
		public AudioSource bodySource;
		public TrackConstReference track;

		private void Awake() {
			Assert.IsNotNull(startSource);
			Assert.IsNotNull(bodySource);

			startSource.clip = track.ConstValue.TrackStart;
			bodySource.clip = track.ConstValue.TrackBody;

			startSource.loop = false;
			bodySource.loop = true;

			if(persistent) {
				DontDestroyOnLoad(gameObject);
			}
		}

		private void Start() {
			if(playOnStart) {
				Play();
			}
		}

		public void SetVolume(float volume) {
			startSource.volume = volume;
			bodySource.volume = volume;
		}

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
			float startClipLength = (startSource.clip != null ? startSource.clip.length : 0f);

			startSource.Play();
			bodySource.PlayDelayed(startClipLength);
		}

		private void Stop() {
			startSource.Stop();
			bodySource.Stop();
		}

	}
}
