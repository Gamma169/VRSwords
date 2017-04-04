using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBlade : MonoBehaviour {

	public bool disrupted;

	public float bladeLength = 2.5f;
	public float disruptTime = 1.5f;


	private CapsuleCollider cc;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Disruptor") {
			StartCoroutine(DisruptBlade());
		}
	}

	private IEnumerator DisruptBlade() {
		float lerpVal = 1;
		disrupted = true;
		while (lerpVal > 0) {
			transform.localPosition = Vector3.Lerp(new Vector3(0, .8f, 0), new Vector3(0, 3.5f, 0), lerpVal);
			transform.localScale = Vector3.Lerp(new Vector3(.5f, .1f, .5f), new Vector3(.5f, 2.5f, .5f), lerpVal);

			lerpVal -=  3 * Time.deltaTime;

			yield return null;
		}
	}


}
