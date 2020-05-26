using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReachBeyond.VariableObjects;

[RequireComponent(typeof(Rigidbody2D))]
public class ForceTowardsGameObject : MonoBehaviour
{
	public GameObjectConstReference obj;
	public float force;

	private Rigidbody2D rb;

	private void Awake() {
		rb = GetComponent<Rigidbody2D>();
	}

	private void OnEnable() {
		if(obj.ConstValue != null) {
			Vector2 dir = (obj.ConstValue.transform.position - transform.position).normalized;

			rb.AddForce(dir * force, ForceMode2D.Impulse);
		}
	}
}
