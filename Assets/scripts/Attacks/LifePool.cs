using UnityEngine;
using ReachBeyond.VariableObjects;

namespace Attacks {
	[DisallowMultipleComponent]
	public class LifePool : MonoBehaviour {
#pragma warning disable CS0649
		[SerializeField] private IntConstReference maximum;
		[SerializeField] private IntReference _current;
#pragma warning restore CS0649

		public int CurrentLife {
			get {
				return _current;
			}
			set {
				_current.Value = Mathf.Clamp(value, 0, maximum);
			}
		}


		public void OnValidate() {
			// This is a pressure release valve. Otherwise, OnValidate
			// will run when we're just instantiated which is obviously bad.
			if(maximum == null || _current == null) {
				return;
			}

			if(maximum < 0) {
				Debug.LogWarning("Max life set below zero... that shouldn't happen.");
			}
			else {
				int oldCurrent = _current;
				CurrentLife = _current;

				if(oldCurrent != CurrentLife) {
					Debug.LogWarning("Life total must not be set outside normal range; resetting.");
				}
			}
		}

	}
}
