using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnKeypress : MonoBehaviour
{
	public string buttonName = "Fire1";
	public UnityEvent onPress;

	private void Update() {
		if(Input.GetButtonDown(buttonName)) {
			onPress.Invoke();
		}
	}
}
