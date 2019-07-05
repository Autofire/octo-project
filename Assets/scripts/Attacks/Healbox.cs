using UnityEngine;
using ReachBeyond.VariableObjects;
using ReachBeyond.EventObjects;

namespace Attacks {
	[DisallowMultipleComponent]
	public class Healbox : MonoBehaviour {
#pragma warning disable CS0649
		[SerializeField] private TeamObject sourceTeam;
		[SerializeField] private IntConstReference healAmount;
#pragma warning restore CS0649

		private void OnTriggerEnter2D(Collider2D collision) {
			Hit(collision.gameObject);
		}

		private void Hit(GameObject target) {
			Hurtbox otherHurtbox = null;

			if(target != null) {
				otherHurtbox = target.GetComponent<Hurtbox>();
			}

			if(otherHurtbox != null) {

				if(!sourceTeam.IsAgainst(otherHurtbox.Team)) {
					otherHurtbox.GetHealed(healAmount);
					DestroySelf();
				}
			}
		}


		public void DestroySelf() {
			Destroy(gameObject);
		}
	}
}
