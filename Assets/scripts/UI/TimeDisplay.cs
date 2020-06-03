using ReachBeyond.VariableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeDisplay : MonoBehaviour
{
	public Text text;
	public FloatConstReference time;

	private void OnGUI() {
		int totalSeconds = Mathf.RoundToInt(time);
		int minutes = totalSeconds / 60;
		int seconds = totalSeconds % 60;

		text.text = minutes + ":" + seconds.ToString("D2");
	}
}
