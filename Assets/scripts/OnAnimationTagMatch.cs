using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class OnAnimationTagMatch : MonoBehaviour {
#pragma warning disable CS0649
	[SerializeField] private Animator anim;
	[SerializeField] private int layerIndex = 0;
	[SerializeField] private string targetTag = "End";

	[Space(10)]
	[SerializeField] private bool disableOnMatch = true;
	[SerializeField] private UnityEvent onMatch;
#pragma warning restore CS0649

	private void Awake() {
		Assert.IsNotNull(anim);
	}

	private void Update() {
		if(anim.GetCurrentAnimatorStateInfo(layerIndex).IsTag(targetTag)) {
			if(disableOnMatch) {
				enabled = false;
			}

			onMatch.Invoke();
		}
	}
}
