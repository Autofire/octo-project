using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
	public Vector3 speed;
	public Vector3 loopStartPosition;
	public float loopWhenYLessThan;

    // Update is called once per frame
    void Update()
    {
		if(transform.position.y < loopWhenYLessThan) {
			transform.position = loopStartPosition;
		}

		transform.position -= speed * Time.deltaTime;
    }
}
