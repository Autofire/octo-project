using UnityEngine;
using UnityEngine.Assertions;
using Characters.Bodies.Interfaces;

namespace Characters.Bodies {

	[RequireComponent(typeof(Rigidbody2D))]
	public class Walker : MonoBehaviour, IWalk {

#pragma warning disable CS0649
		[SerializeField] private float _walkSpeed;

		[Header("Animations")]
		[SerializeField] private SpriteRenderer sprite;
		[SerializeField] private Animator anim;
		[SerializeField] private string xWalkMagnitudeName = "XWalk";
		[SerializeField] private string yWalkMagnitudeName = "YWalk";
		[SerializeField] private string walkAnimName = "Walk";
		[SerializeField] private string idleAnimName = "Idle";
#pragma warning restore CS0649

		private Vector2 desiredDirection;
		private Vector2 mostRecentDirection;
		private new Rigidbody2D rigidbody;

		public float WalkSpeed {
			get {
				return _walkSpeed;
			}
		}

		public void Walk(Vector2 direction) {

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
					dir = desiredDirection;
				}
				else {
					anim.Play(idleAnimName);
					dir = mostRecentDirection;
				}

				anim.SetFloat(xWalkMagnitudeName, dir.x);
				anim.SetFloat(yWalkMagnitudeName, dir.y);

				if(sprite != null) {
					//Debug.Log(dir.x);
					sprite.flipX = (dir.x < 0);
				}

			}

			rigidbody.velocity = (desiredDirection * WalkSpeed);
		}

		#region Unity events
		private void Awake() {
			rigidbody = GetComponent<Rigidbody2D>();
			Assert.IsNotNull(rigidbody);
		}

		#endregion

	} // End class
} // End namespace
