using ReachBeyond.VariableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SubsequentTitleScreenVisitHandler : MonoBehaviour
{
	public BoolConstReference hasVisited;
	public UnityEvent onFirstVisit;
	public UnityEvent onSubsequentVisit;

	private void Start() {
		if(hasVisited.ConstValue) {
			onSubsequentVisit.Invoke();
		}
		else {
			onFirstVisit.Invoke();
		}
	}
}
