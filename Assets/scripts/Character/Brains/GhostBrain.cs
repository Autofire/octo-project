using UnityEngine;
using ReachBeyond.VariableObjects;
using Characters.Bodies.Interfaces;

namespace Characters.Brains {

	public class GhostBrain : MonoBehaviour {

#pragma warning disable CS0649
		[SerializeField] private GameObjectConstReference huntTarget;
		[SerializeField] private LayerMask visionMask;
#pragma warning restore CS0649

		[Space(10)]
		public IWalk walkComp;
		public IAttack attackComp;

		private void Awake() {
			if(walkComp == null) {
				walkComp = GetComponent<IWalk>();
			}

			if(attackComp == null) {
				attackComp = GetComponent<IAttack>();
			}
		}

		private void Update() {
			if(huntTarget.ConstValue == null) {
				MopeAround();
			}
			else {
				RaycastHit2D hit = Physics2D.Raycast(
					origin: transform.position,
					direction: huntTarget.ConstValue.transform.position - transform.position,
					layerMask: visionMask.value,
					distance: Mathf.Infinity
				);

				if(hit.collider != null && hit.transform.gameObject == huntTarget.ConstValue) {
					// We can see 'em!
					Debug.Log("Visible");
				}
			}

		}

		private void MopeAround() {

		}

	}
}
