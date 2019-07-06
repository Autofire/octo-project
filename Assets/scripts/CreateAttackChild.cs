using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAttackChild : MonoBehaviour
{
	public GameObject attackObjectPrefab;
	public float prefabSpawnDistance;
	public FacingHandler facing;

    void Start()
    {
		Debug.Log("Creating child");
		Vector2 direction = facing.Facing;

		GameObject attackObj = Instantiate(
			attackObjectPrefab,
			transform.position + (Vector3) direction * prefabSpawnDistance,
			attackObjectPrefab.transform.rotation,
			transform
		) as GameObject;

		FacingHandler[] attackFacingComps = attackObj.GetComponentsInChildren<FacingHandler>();
		foreach(FacingHandler attackFacingComp in attackFacingComps) {
			attackFacingComp.Facing = direction;
		}
	}

}
