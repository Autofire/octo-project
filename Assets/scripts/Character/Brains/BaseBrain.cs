using UnityEngine;
using Characters.Bodies.Interfaces;

namespace Characters.Brains {
	public class BaseBrain : MonoBehaviour {
		public MonoBehaviour body;

		protected virtual void OnValidate() {
			if(body != null && !(body is IBody)) {
				Debug.LogError(gameObject.name + ": A brain's body must implement the IBody interface.");
				body = null;
			}
		}
	} // End class
} // End namespace
