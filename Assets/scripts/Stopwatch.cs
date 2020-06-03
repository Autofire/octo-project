using ReachBeyond.VariableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stopwatch : MonoBehaviour
{
	public FloatReference startTime;
	public FloatReference durationTime;
	public bool startOnEnable = false;

	private bool isRunning = false;

	public void StartTiming() {
		startTime.Value = Time.time;
		isRunning = true;
		UpdateDuration();
	}

	public void StopTiming() {
		if(isRunning) {
			isRunning = false;
			UpdateDuration();
		}
	}

	private void UpdateDuration() {
		durationTime.Value = Time.time - startTime;
	}

	private void OnEnable() {
		if(startOnEnable) {
			StartTiming();
		}
	}

	private void OnDisable() {
		StopTiming();
	}

	private void Update() {
		if(isRunning) {
			UpdateDuration();
		}
	}
}
