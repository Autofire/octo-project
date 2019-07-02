using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSpawner : MonoBehaviour
{
	public GameObject prefab;
	public Transform parent;

	private void Start() {
		Instantiate(prefab, transform.position, prefab.transform.rotation, parent);
	}
}
