using UnityEngine;
using UnityEngine.Assertions;
using ReachBeyond.EventObjects;
using System.Collections;

namespace Characters.Bodies {

	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(FacingHandler))]
	public class Attacker : MonoBehaviour, Interfaces.IAttack {

#pragma warning disable CS0649
		[SerializeField] private GameObject attackObjectPrefab;
		[SerializeField] private float prefabSpawnDistance;

		[Tooltip("The attack range in units. This obviously depends on the prefab which gets spawed, and it used by AI.")]
		[SerializeField] private float _attackRange;
		[Tooltip("This is the speed WE move when we attack")]
		[SerializeField] private float attackMoveSpeed;
		[SerializeField] private float attackWindupTime = 0f;
		[SerializeField] private bool canChangeDirection = false;

		[Header("Animations")]
		[SerializeField] private Animator anim;
		[SerializeField] private string attackState = "Attack";
		[SerializeField] private int layerIndex = 0;
		[SerializeField] private string attackTag = "Attack";

		[SerializeField] private EventObjectInvoker onAttackAnimStart;
		[SerializeField] private EventObjectInvoker onAttackAnimEnd;
#pragma warning restore CS0649

		private bool IsAttacking {
			get {
				if(anim == null) {
					return false;
				}
				else {
					return anim.GetCurrentAnimatorStateInfo(layerIndex).IsTag(attackTag);
				}
			}
		}

		private bool wasAttackingLastUpdate;
		private FacingHandler facingComp;
		private Rigidbody2D rb;

		#region Unity events
		private void Awake() {
			facingComp = GetComponent<FacingHandler>();
			rb = GetComponent<Rigidbody2D>();
		}

		// This is... a little gross. But we need to make sure that, if this gets disabled
		// in the middle of an attack, whatever cleanup events still occur. Furthermore,
		// we need to do whatever it is we do when attacking if we get re-enabled mid-attack.
		private void OnEnable() {
			if(IsAttacking) {
				wasAttackingLastUpdate = true;
				onAttackAnimStart.Invoke();
			}
		}

		private void OnDisable() {
			if(IsAttacking) {
				wasAttackingLastUpdate = false;
				onAttackAnimEnd.Invoke();
			}
		}

		private void Update() {
			if(IsAttacking) {
				wasAttackingLastUpdate = true;
			}
			else if(wasAttackingLastUpdate) {
				wasAttackingLastUpdate = false;
				onAttackAnimEnd.Invoke();
			}
		}
		#endregion


		public float AttackRange {
			get { return _attackRange; }
		}

		public void Attack() {
			Attack(Vector2.zero);
		}

		public void Attack(Vector2 direction) {
			if(!IsAttacking) {

				if(direction == Vector2.zero) {
					direction = facingComp.Facing;

					if(direction == Vector2.zero) {
						Debug.LogWarning(gameObject.name + ": Unsure which direction to attack; attacking down.");
						direction = Vector2.down;
					}
				}

				if(canChangeDirection) {
					facingComp.Facing = direction;
				}

				// Ok, this'll look a little weird, FacingHandler.Facing does some stuff to the direction.
				// This is the shown direction, so we want to heed what it shows, not what is really
				// being used.
				//facingComp.Facing = direction;
				direction = facingComp.Facing.normalized;

				//Vector2 orthogonalDir = direction;

				/*
				if(Mathf.Abs(orthogonalDir.x) <= Mathf.Abs(orthogonalDir.y)) {
					orthogonalDir.x = 0;
				}
				else {
					orthogonalDir.y = 0;
				}
				*/

				StartCoroutine(StartAttack(direction));

				// Deal with ourselves
				if(anim != null) {
					onAttackAnimStart.Invoke();
					anim.Play(attackState);
				}

				rb.velocity += direction * attackMoveSpeed;
			} // End if(!IsAttacking)
		} // End Attack function

		public IEnumerator StartAttack(Vector2 direction) {
			if(attackWindupTime > Mathf.Epsilon) {
				yield return new WaitForSeconds(attackWindupTime);
			}

			GameObject attackObj = Instantiate(
				attackObjectPrefab,
				transform.position + (Vector3) direction * prefabSpawnDistance,
				attackObjectPrefab.transform.rotation,
				null
			) as GameObject;

			FacingHandler[] attackFacingComps = attackObj.GetComponentsInChildren<FacingHandler>();
			foreach(FacingHandler attackFacingComp in attackFacingComps) {
				attackFacingComp.Facing = direction;
			}

			yield return null;

		}
	}

}
