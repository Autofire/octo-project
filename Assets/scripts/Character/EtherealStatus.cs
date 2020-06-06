using ReachBeyond.VariableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EtherealStatus : MonoBehaviour
{
	public static void Apply(GameObject target) {
		EtherealStatus targetStatus = target.GetComponent<EtherealStatus>();

		if(targetStatus != null) {
			targetStatus.Apply();
		}
	}

	public float duration = 10f;
	public IntReference remainingPercentage;
	public UnityEvent onApply;
	public UnityEvent onRemove;

	private float applicationTime;
	private float ExpirationTime {
		get {
			return applicationTime + duration;
		}
		set {
			applicationTime = value - duration;
		}
	}

	public void Apply() {
		Debug.Log("Status expries at " + ExpirationTime);

		if(!enabled) {
			enabled = true;
		}
		else {
			applicationTime = Time.time;
		}
	}

	public void Remove() {
		enabled = false;
	}

	public void ReduceDuration(float reductionTime) {
		ExpirationTime = ExpirationTime - reductionTime;
	}

	private void Awake() {
		remainingPercentage.Value = 0;
	}

	private void OnEnable() {
		applicationTime = Time.time;
		onApply.Invoke();
	}

	private void OnDisable() {
		onRemove.Invoke();
	}

	private void Update() {
		remainingPercentage.Value = Mathf.CeilToInt(ExpirationTime - Time.time);

		if(Time.time > ExpirationTime) {
			Debug.Log("Expiring");
			Remove();
		}
	}
}
