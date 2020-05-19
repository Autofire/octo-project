using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSpawner : MonoBehaviour
{
	public GameObject prefab;
	public Transform parent;
	public bool spawnOnStart = true;

	private void Start() {
		if(spawnOnStart) {
			Spawn();
		}
	}

	public void Spawn() {
		Instantiate(prefab, transform.position, prefab.transform.rotation, parent);
	}
}
