using UnityEngine;
using Characters.Bodies.Interfaces;

namespace Characters.Brains {

	public class PlayerBrain : MonoBehaviour {

#pragma warning disable CS0649
		[SerializeField] private string hAxisName = "Horizontal";
		[SerializeField] private string vAxisName = "Vertical";
#pragma warning restore CS0649

		[Space(10)]
		public IWalk walkComp;

		private void Awake() {
			if(walkComp == null) {
				walkComp = GetComponent<IWalk>();
			}
		}

		private void Update() {
			walkComp?.Walk(new Vector2(Input.GetAxis(hAxisName), Input.GetAxis(vAxisName)));
		}

	}
}
