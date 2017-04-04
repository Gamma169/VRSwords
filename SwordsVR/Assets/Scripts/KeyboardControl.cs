using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardControl : MonoBehaviour {


	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	
		Vector3 velocity = new Vector3(0, 0, 0);

		if (Input.GetKey("w"))
			velocity.z += 1;
		if (Input.GetKey("s"))
			velocity.z -= 1;
		if (Input.GetKey("a"))
			velocity.x -= 1;
		if (Input.GetKey("d"))
			velocity.x += 1;

		rb.velocity = velocity;

	}
}
