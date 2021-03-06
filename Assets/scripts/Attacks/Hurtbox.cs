﻿using UnityEngine;
using ReachBeyond.VariableObjects;
using ReachBeyond.EventObjects;
using System;

namespace Attacks {
	[RequireComponent(typeof(LifePool))]
	[DisallowMultipleComponent]
	public class Hurtbox : MonoBehaviour {

#pragma warning disable CS0649
		[SerializeField] private TeamObject _team;
		[SerializeField] private EventObjectInvoker onHeal;
		[SerializeField] private EventObjectInvoker onHit;
		[SerializeField] private EventObjectInvoker onDie;

		[Header("Mercy frames")]
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
		private float damageMultiplier = 1f;

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

			// Check here because our HP can drop and hit zero without getting hit
			// HACK We should actually be listening for events instead.
			if(pool.CurrentLife == 0) {
				Die();
			}
		}

		public void SetDamageMultiplier(float multiplier) {
			damageMultiplier = multiplier;
		}

		public void ResetDamageMultiplier() {
			damageMultiplier = 1f;
		}

		public void GetHit(int damage) {
			if(enabled && !IsInvulnerable) {
				pool.CurrentLife -= Mathf.RoundToInt(damage * damageMultiplier);
				timeOfLastHit = Time.time;

				if(pool.CurrentLife > 0) {
					onHit.Invoke();
				}
				else {
					Die();
				}
			}
		}

		public void GetHealed(int healAmount) {
			if(enabled) {
				int previousLife = pool.CurrentLife;

				pool.CurrentLife += healAmount;

				if(pool.CurrentLife != previousLife) {
					onHeal.Invoke();
				}
			}
		}

		private void Die() {
			onDie.Invoke();
			Destroy(gameObject);
		}
	}
}
