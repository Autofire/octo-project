using UnityEngine;
using ReachBeyond.VariableObjects;
using ReachBeyond.EventObjects;
using UnityEngine.Events;

namespace Attacks {
	[DisallowMultipleComponent]
	public class Healbox : MonoBehaviour {

		public TeamObject sourceTeam;
		public IntConstReference healAmount;
		public bool destroySelfOnHit = true;
		public bool makeTargetEthereal = false;

		private void OnTriggerEnter2D(Collider2D collision) {
			Hit(collision.gameObject);
		}

		private void Hit(GameObject target) {

			if(target != null) {
				Hurtbox otherHurtbox = target.GetComponent<Hurtbox>();

				if(otherHurtbox != null && !sourceTeam.IsAgainst(otherHurtbox.Team)) {
					otherHurtbox.GetHealed(healAmount);

					if(makeTargetEthereal) {
						EtherealStatus.Apply(target);
					}

					if(destroySelfOnHit) {
						Destroy(gameObject);
					}
				}
			}
		}


	}
}
