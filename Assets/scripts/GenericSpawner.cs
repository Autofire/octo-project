using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GenericSpawner : MonoBehaviour
{
	public GameObject prefab;
	public Transform parent;
	public bool spawnOnStart = true;

	[Space(10)]
	public bool passFacingToSpawnedObject = false;
	public FacingHandler facingHandler;

	private void Awake() {
		if(passFacingToSpawnedObject) {
			Assert.IsNotNull(facingHandler);
		}
	}

	private void Start() {
		if(spawnOnStart) {
			Spawn();
		}
	}

	public void Spawn() {
		GameObject newObj = Instantiate(prefab, transform.position, prefab.transform.rotation, parent) as GameObject;

		if(passFacingToSpawnedObject) {
			FacingHandler newObjFacingHandler = newObj.GetComponent<FacingHandler>();

			if(newObjFacingHandler != null) {
				newObjFacingHandler.Facing = facingHandler.Facing;
			}
		}
	}
}
