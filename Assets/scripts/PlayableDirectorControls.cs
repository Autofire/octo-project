using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class PlayableDirectorControls : MonoBehaviour
{
	private PlayableDirector director;

	private void Awake() {
		director = GetComponent<PlayableDirector>();
	}

	public void SkipToEnd() {
		director.time = director.duration;
    }

}
