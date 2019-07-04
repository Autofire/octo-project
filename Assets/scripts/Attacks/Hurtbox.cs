using UnityEngine;
using ReachBeyond.EventObjects;

namespace Attacks {
	[RequireComponent(typeof(LifePool))]
	public class Hurtbox : MonoBehaviour {
#pragma warning disable CS0649
		[SerializeField] private TeamObject _team;
		[SerializeField] private EventObjectInvoker onHit;
		[SerializeField] private EventObjectInvoker onDie;
#pragma warning restore CS0649

		private LifePool pool;

		public TeamObject Team {
			get { return _team; }
		}


		private void Awake() {
			pool = GetComponent<LifePool>();
		}


		public void GetHit(int damage) {
			pool.CurrentLife -= damage;

			if(pool.CurrentLife > 0) {
				onHit.Invoke();
			}
			else {
				onDie.Invoke();
			}
		}

		public void Die() {
			Destroy(gameObject);
		}
	}
}
