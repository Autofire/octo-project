using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectDestroyer : MonoBehaviour
{
	public GameObject target;
	public float delay = 0f;
	public bool destroyOnStart = false;

	private void Start() {
		if(destroyOnStart) {
			DestroyTarget();		
		}
	}

	public void DestroyTarget() {
		Destroy(target, delay);
	}
}
