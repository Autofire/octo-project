using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ManualAction : MonoBehaviour
{
	public UnityEvent action;

	public void Trigger() {
		action.Invoke();
	}
}
