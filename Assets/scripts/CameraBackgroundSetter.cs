using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraBackgroundSetter : MonoBehaviour
{
	public Color[] colors;
	public Camera cam;

	private void Awake() {
		Assert.IsNotNull(cam);
	}

	public void SetColor(int colorIndex) {
		cam.backgroundColor = colors[colorIndex];
	}

}
