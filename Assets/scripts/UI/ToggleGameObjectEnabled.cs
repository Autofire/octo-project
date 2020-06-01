using ReachBeyond.VariableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGameObjectEnabled : MonoBehaviour
{
	public GameObject[] objects;
	public float period;

	private float timeToNextToggle = 0f;

	public void Update() {
		if(Time.time > timeToNextToggle) {
			timeToNextToggle = Time.time + period;

			foreach(GameObject obj in objects) {
				obj.SetActive(!obj.activeSelf);
			}
		}
	}
}
