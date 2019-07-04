using UnityEngine;
using ReachBeyond.VariableObjects;
using ReachBeyond.EventObjects;

namespace Attacks {
	public class Hitbox : MonoBehaviour {
#pragma warning disable CS0649
		[SerializeField] private TeamObject sourceTeam;
		[SerializeField] private IntConstReference damage;

		[SerializeField] private EventObjectInvoker onConnectEnemy;
		[SerializeField] private EventObjectInvoker onConnectFriend;
#pragma warning restore CS0649

		private void OnTriggerEnter2D(Collider2D collision) {

			Hurtbox otherHurtbox = collision.GetComponent<Hurtbox>();

			if(otherHurtbox != null) {

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
