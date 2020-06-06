using ReachBeyond.VariableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayClipFromSourceReference : MonoBehaviour
{
	public AudioSourceConstReference source;
	public AudioClip clip;
	[Range(0f, 1f)]
	public float volume = 1f;
	public bool playOnEnable = false;

	public void OnEnable() {
		if(playOnEnable) {
			Play();
		}
	}

	public void Play() {
		source.ConstValue.PlayOneShot(clip, volume);
	}
}
