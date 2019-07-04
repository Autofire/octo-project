using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class RemoveOnAnimationEnd : MonoBehaviour
{
#pragma warning disable CS0649
	[SerializeField] private Animator anim;
	[SerializeField] private int layerIndex = 0;
	[SerializeField] private string endTag = "End";
#pragma warning restore CS0649

	private void Awake() {
		Assert.IsNotNull(anim);
	}

	private void Update() {
		if(anim.GetCurrentAnimatorStateInfo(layerIndex).IsTag(endTag)) {
			Destroy(gameObject);
		}
    }
}
