using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RealtimePause : MonoBehaviour
{
	public float pauseDuration;
	public UnityEvent onUnpause;

	private float pauseEndTime = -1f;
	private bool paused = false;
	private float oldTimeScale;

	public void StartPause() {
		pauseEndTime = Time.unscaledTime + pauseDuration;

		oldTimeScale = Time.timeScale;
		Time.timeScale = 0f;
		paused = true;
	}

    private void Update() {
		if(paused && Time.unscaledTime > pauseEndTime) {
			paused = false;
			Time.timeScale = oldTimeScale;

			onUnpause.Invoke();
		}
    }
}
