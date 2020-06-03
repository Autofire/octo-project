using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ReachBeyond.VariableObjects;

public class MeterController : MonoBehaviour
{
	public Slider slider;
	public IntConstReference value;
	public IntConstReference maximum;
	public bool setOnAwake = true;

	public void Awake() {
		if(setOnAwake) {
			OnGUI();
		}
	}

	public void OnGUI() {
		slider.value = value / (float) maximum;
	}
}
