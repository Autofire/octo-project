using UnityEngine;
using ReachBeyond.VariableObjects;
using ReachBeyond.EventObjects;

namespace Attacks {
	[DisallowMultipleComponent]
	public class Hitbox : MonoBehaviour {
#pragma warning disable CS0649
		[SerializeField] private TeamObject sourceTeam;
		[SerializeField] private IntConstReference damage;
		[SerializeField] private BoolConstReference allowRepeatHits;

		[SerializeField] private EventObjectInvoker onConnectEnemy;
		[SerializeField] private EventObjectInvoker onConnectFriend;
#pragma warning restore CS0649

		private void OnTriggerEnter2D(Collider2D collision) {
			Hit(collision.gameObject);
		}

		private void OnTriggerStay2D(Collider2D collision) {
			if(allowRepeatHits) {
				Hit(collision.gameObject);
			}
		}

		private void Hit(GameObject target) {
			Hurtbox otherHurtbox = null;

			if(target != null) {
				otherHurtbox = target.GetComponent<Hurtbox>();
			}

			if(otherHurtbox != null && !otherHurtbox.IsInvulnerable) {

				if(sourceTeam.IsAgainst(otherHurtbox.Team)) {
					onConnectEnemy.Invoke();
					otherHurtbox.GetHit(damage);
				}
				else {
					onConnectFriend.Invoke();
				}
			}
		}


		public void DestroySelf() {
			Destroy(gameObject);
		}
	}
}
