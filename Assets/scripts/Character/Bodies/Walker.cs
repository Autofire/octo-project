﻿using UnityEngine;
using UnityEngine.Assertions;
using Characters.Bodies.Interfaces;
using ReachBeyond.VariableObjects;

namespace Characters.Bodies {

	[RequireComponent(typeof(FacingHandler))]
	[RequireComponent(typeof(Rigidbody2D))]
	public class Walker : MonoBehaviour, IWalk {

#pragma warning disable CS0649
		[SerializeField] private FloatConstReference _walkSpeed;

		[Header("Animations")]
		[SerializeField] private Animator anim;
		[SerializeField] private string walkAnimName = "Walk";
		[SerializeField] private string idleAnimName = "Idle";
#pragma warning restore CS0649

		private FacingHandler facingComp;
		private Vector2 desiredDirection;
		private Vector2 mostRecentDirection;
		private new Rigidbody2D rigidbody;

		public float WalkSpeed {
			get {
				return _walkSpeed.ConstValue;
			}
		}

		public void Walk(Vector2 direction) {

			if(enabled) {
				// We want to clamp this so that we don't ever exceed a total magnitude of 1.
				if(direction.sqrMagnitude > 1f) {
					direction = direction.normalized;
				}

				if(direction.sqrMagnitude > Mathf.Epsilon) {
					mostRecentDirection = direction.normalized;
				}

				desiredDirection = direction;

				if(anim != null) {
					Vector2 dir = Vector2.zero;

					if(desiredDirection.sqrMagnitude > Mathf.Epsilon) {
						anim.Play(walkAnimName);
						//dir = desiredDirection;
						facingComp.Facing = desiredDirection;
					}
					else {
						anim.Play(idleAnimName);
						//dir = mostRecentDirection;
					}

					//facingComp.Facing = dir;

				}

				rigidbody.velocity = (desiredDirection * WalkSpeed);
			}
		}

		#region Unity events
		private void Awake() {
			rigidbody = GetComponent<Rigidbody2D>();
			Assert.IsNotNull(rigidbody);

			facingComp = GetComponent<FacingHandler>();
			Assert.IsNotNull(facingComp);
		}

		private void Start() {
			// Adding the enabled box
		}

		private void OnDisable() {
			rigidbody.velocity = Vector2.zero;
		}
		#endregion

	} // End class
} // End namespace
