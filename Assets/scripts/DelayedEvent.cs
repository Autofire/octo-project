using ReachBeyond.VariableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayedAction : MonoBehaviour
{
	public FloatConstReference delay;
	public bool useScaledTime = true;
	public UnityEvent action;

	public void DoAction() {
		StartCoroutine(ActionCoroutine());
	}

	private IEnumerator ActionCoroutine() {
		if(useScaledTime) {
			yield return new WaitForSeconds(delay);
		}
		else {
			yield return new WaitForSecondsRealtime(delay);
		}

		action.Invoke();
	}
}
