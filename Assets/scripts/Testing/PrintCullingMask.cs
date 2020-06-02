using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintCullingMask : MonoBehaviour
{
	public Camera cam;

	public void OnEnable() {

		OnDisable();
	}

	public void OnDisable() {
		Debug.Log(cam.cullingMask);
	}
}
