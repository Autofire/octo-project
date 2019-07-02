using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReachBeyond.EventObjects;

public class TriggerResponder : MonoBehaviour
{
	public EventObjectInvoker onTriggerEnter;
	public EventObjectInvoker onTriggerStay;
	public EventObjectInvoker onTriggerExit;

	private void Start() {
		// This is just here to make the 'enabled' tick box appear
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if(enabled) {
			onTriggerEnter.Invoke();
		}
	}

	private void OnTriggerStay2D(Collider2D collision) {
		if(enabled) {
			onTriggerStay.Invoke();
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if(enabled) {
			onTriggerExit.Invoke();
		}
	}
}
