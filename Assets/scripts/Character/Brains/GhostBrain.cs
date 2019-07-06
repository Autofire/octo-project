using UnityEngine;
using ReachBeyond.VariableObjects;
using Characters.Bodies.Interfaces;

namespace Characters.Brains {

	public class GhostBrain : MonoBehaviour {

#pragma warning disable CS0649
		[SerializeField] private GameObjectConstReference huntTarget;
		[SerializeField] private LayerMask visionMask;

		[Header("Meander")]
		[SerializeField] private float meanderSpeed = 0.5f;
		[SerializeField] private float pauseMinDuration = 1f;
		[SerializeField] private float pauseMaxDuration = 4f;
		[SerializeField] private float meanderMinDuration = 1f;
		[SerializeField] private float meanderMaxDuration = 3f;

		[Header("Hunt")]
		[SerializeField] private float attackCooldown = 1f;
#pragma warning restore CS0649

		[Space(10)]
		public IWalk walkComp;
		public IAttack attackComp;

		private Vector2 meanderDirection = Vector2.one;
		private float meanderStepEndTime = 0f;
		private float attackResetTime = 0f;

		private void Awake() {
			if(walkComp == null) {
				walkComp = GetComponent<IWalk>();
			}

			if(attackComp == null) {
				attackComp = GetComponent<IAttack>();
			}
		}

		private void Update() {
			if(huntTarget.ConstValue == null || attackComp == null) {
				Meander();
			}
			else {
				RaycastHit2D hit = Physics2D.Raycast(
					origin: transform.position,
					direction: huntTarget.ConstValue.transform.position - transform.position,
					layerMask: visionMask.value,
					distance: Mathf.Infinity
				);

				if(hit.collider != null && hit.transform.gameObject == huntTarget.ConstValue) {
					// We can see 'em!

					// First, we'll tell the meander stuff that we are moving and that we'll
					// stop next time we meander. This way, we'll just freeze the moment
					// we cannot see the player.
					meanderDirection = Vector2.one * Mathf.Epsilon;
					meanderStepEndTime = 0f;

					Hunt();
				}
				else {
					Meander();
				}
			}

		}

		// Only gets called if we have IAttack
		private void Hunt() {
			float attackRange = attackComp.AttackRange;
			float halfAttackRange = attackRange / 2;

			Bounds attackBoundSize = new Bounds(Vector3.zero, new Vector3(halfAttackRange, halfAttackRange, 0f));
			Vector3 huntTargetPos3D = huntTarget.ConstValue.transform.position;
			Vector2 huntTargetPos = new Vector2(huntTargetPos3D.x, huntTargetPos3D.y);

			Vector3 dirToTarget3D = huntTargetPos3D - transform.position;
			Vector2 dirToTarget = new Vector2(dirToTarget3D.x, dirToTarget3D.y);

			/*
			Vector2 lowerPoint = huntTargetPos + Vector2.down * halfAttackRange;
			Vector2 upperPoint = huntTargetPos + Vector2.up * halfAttackRange;
			Vector2 leftPoint = huntTargetPos + Vector2.left * halfAttackRange;
			Vector2 rightPoint = huntTargetPos + Vector2.right * halfAttackRange;
			*/


			if((dirToTarget).magnitude > attackRange) {
				walkComp.Walk(dirToTarget);
			}
			else if(Time.time > attackResetTime) {
				attackComp.Attack(dirToTarget);
				attackResetTime = attackCooldown + Time.time;
			}
		}

		private void Meander() {
			if(Time.time > meanderStepEndTime) {
				ChooseNewMeanderDirection();
			}

			walkComp?.Walk(meanderDirection);
		}

		private void ChooseNewMeanderDirection() {
			if(meanderDirection == Vector2.zero) {
				// We were previously paused; time to walk
				meanderDirection = new Vector2( Random.Range(-1f, 1f), Random.Range(-1f, 1f) );
				meanderDirection = meanderDirection.normalized * meanderSpeed;

				meanderStepEndTime = Time.time + Random.Range(meanderMinDuration, meanderMaxDuration);
			}
			else {
				// We were previously walking; time to pause
				meanderDirection = Vector2.zero;
				meanderStepEndTime = Time.time + Random.Range(pauseMinDuration, pauseMaxDuration);
			}

		}

	}
}
