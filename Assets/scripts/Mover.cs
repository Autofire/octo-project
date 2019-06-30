using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
	public Vector3 speed;
	public Vector3 loopStartPosition;
	public float loopWhenXMagExceeds;
	public float loopWhenYMagExceeds;

    // Update is called once per frame
    void Update()
    {
		if(Mathf.Abs(transform.position.y) > loopWhenYMagExceeds || Mathf.Abs(transform.position.x) > loopWhenXMagExceeds) {
			transform.position = loopStartPosition;
		}

		transform.position += speed * Time.deltaTime;
    }
}
