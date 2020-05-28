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
		public struct MusicChannel {
			public TrackConstReference currentTrack;
			public AudioSource source;

			[HideInInspector]
			public AudioSource startSource;
		}

		[Tooltip("Set true to keep make this game object stick around between scenes")]
		public bool persistent = false;
		public bool playOnStart = true;
		public MusicChannel[] channels;

		private Track playingTrack = null;

		private void Awake() {
			foreach(MusicChannel channel in channels) {
				Assert.IsNotNull(channel.source);

				Assert.IsFalse(channel.source.gameObject == gameObject);
				AudioSource startSource = Instantiate(channel.source, channel.source.gameObject.transform.parent) as AudioSource;

				Debug.Log("Is start source null? " + startSource == null);
				
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

		private void Update() {
			/*
			if(isFading) {
				float fadeTime = Time.time - fadeStart;
				float newVolume = 1 - fadeTime / fadeDuration;

				SetVolume(newVolume);
			}
			*/
		}


		public void SetVolume(float volume) {
			//loopStartSource.volume = volume;
			//loopBodySource.volume = volume;
		}

		public void ApplyCurrentTrack() {
			Stop();

			//playingTrack = currentTrack;

			//loopStartSource.clip = playingTrack?.TrackStart;
			//loopBodySource.clip = playingTrack?.TrackBody;
		}


		/// <summary>
		/// This doesn't apply the current track, nor does it change the volume.
		/// It simply loads up the new clips and tells the sources to play.
		/// </summary>
		private void Play() {
			//float startClipLength = (loopStartSource.clip != null ? loopStartSource.clip.length : 0f);

			//loopStartSource.Play();
			//loopBodySource.PlayDelayed(startClipLength);
		}

		private void Stop() {
			//loopStartSource.Stop();
			//loopBodySource.Stop();
		}

	}
}
