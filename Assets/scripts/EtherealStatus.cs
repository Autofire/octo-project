using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtherealStatus : MonoBehaviour
{
	public static void Apply(GameObject target) {
		EtherealStatus targetStatus = target.GetComponent<EtherealStatus>();

		if(targetStatus == null) {
			target.AddComponent<EtherealStatus>();
		}
		else {
			targetStatus.Refresh();
		}
	}

	public float duration = 10f;

	private float applicationTime;
	private float ExpirationTime {
		get {
			return applicationTime + duration;
		}
	}

	private void Refresh() {
		applicationTime = Time.time;
		Debug.Log("Status expries at " + ExpirationTime);
	}

	private void Start() {
		Refresh();
	}

	private void Update() {
		if(Time.time > ExpirationTime) {
			Debug.Log("Expiring");
			Destroy(this);
		}
	}
}
