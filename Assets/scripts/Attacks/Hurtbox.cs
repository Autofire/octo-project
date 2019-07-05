using UnityEngine;
using ReachBeyond.VariableObjects;
using ReachBeyond.EventObjects;

namespace Attacks {
	[RequireComponent(typeof(LifePool))]
	[DisallowMultipleComponent]
	public class Hurtbox : MonoBehaviour {
#pragma warning disable CS0649
		[SerializeField] private TeamObject _team;
		[SerializeField] private EventObjectInvoker onHeal;
		[SerializeField] private EventObjectInvoker onHit;
		[SerializeField] private EventObjectInvoker onDie;

		[Header("Merce frames")]
		[SerializeField] private bool _mercyFramesEnabled = true;
		[SerializeField] private FloatConstReference mercyDuration;

		[Header("Effects")]
		[SerializeField] private SpriteRenderer sprite;
		[SerializeField] private Color hitColor = Color.red;
		[SerializeField] private float hitEffectDuration = 0.1f;
		[Range(0,1)]
		[SerializeField] private float invulnerableAlpha = 0.75f;
#pragma warning restore CS0649

		private LifePool pool;
		private float timeOfLastHit = -100;

		public bool MercyFramesEnabled {
			get {
				return _mercyFramesEnabled;
			}
			set {
				_mercyFramesEnabled = value;
			}
		}

		public bool IsInvulnerable {
			get {
				return MercyFramesEnabled && (Time.time < timeOfLastHit + mercyDuration);
			}
		}

		public TeamObject Team {
			get {
				return _team;
			}
		}


		private void Awake() {
			pool = GetComponent<LifePool>();
		}

		private void Update() {
			if(sprite != null) {
				Color resultColor = (Time.time < timeOfLastHit + hitEffectDuration ? hitColor : Color.white);

				if(IsInvulnerable) {
					resultColor.a = invulnerableAlpha;
				}

				sprite.color = resultColor;
			}
		}


		public void GetHit(int damage) {
			if(!IsInvulnerable) {
				pool.CurrentLife -= damage;
				timeOfLastHit = Time.time;

				if(pool.CurrentLife > 0) {
					onHit.Invoke();
				}
				else {
					onDie.Invoke();
					Die();
				}
			}
		}

		public void GetHealed(int healAmount) {
			int previousLife = pool.CurrentLife;

			pool.CurrentLife += healAmount;

			if(pool.CurrentLife != previousLife) {
				onHeal.Invoke();
			}
		}

		private void Die() {
			Destroy(gameObject);
		}
	}
}
