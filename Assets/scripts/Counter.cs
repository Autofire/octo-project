using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReachBeyond.VariableObjects;

public class Counter : MonoBehaviour
{
	#pragma warning disable CS0649
	[SerializeField] private IntReference counter;
	[SerializeField] private FloatConstReference tickFrequency;
	[SerializeField] private IntConstReference changePerTick;
	#pragma warning restore CS0649

	private float timeOfLastTick = 0f;

	private void OnEnable() {
		timeOfLastTick = Time.time;
	}

	void Update()
    {
		// We use a loop for this in the case that tickFrequency
		// ends up being shorter than the Update clock. That way, we'll
		// still count the same way regardless of the time step.
		float timeOfNextTick = timeOfLastTick + tickFrequency;
		while (Time.time >= timeOfNextTick) {
			counter.Value += changePerTick;

			// Rather than setting TimeOfLastTick equal to Time.time,
			// we'd rather pretend that the last tick happened at
			// exactly the correct time. That way, we don't get a slightly
			// slow clock or anything.
			timeOfLastTick = timeOfNextTick;

			timeOfNextTick = timeOfLastTick + tickFrequency;

		}
	}
} // End class
