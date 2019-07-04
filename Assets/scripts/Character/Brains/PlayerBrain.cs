using UnityEngine;
using Characters.Bodies.Interfaces;

namespace Characters.Brains {

	public class PlayerBrain : MonoBehaviour {

#pragma warning disable CS0649
		[SerializeField] private string hAxisName = "Horizontal";
		[SerializeField] private string vAxisName = "Vertical";
		[SerializeField] private string attackButtonName = "Fire1";
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
			Vector2 inputDir = new Vector2(Input.GetAxis(hAxisName), Input.GetAxis(vAxisName));

			walkComp?.Walk(inputDir);

			if(Input.GetButtonDown(attackButtonName)) {
				attackComp?.Attack();
			}
		}

	}
}
