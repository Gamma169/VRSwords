using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBlade : MonoBehaviour {

	public bool disrupted;

	public float bladeLength = 2.5f;
	public float disruptTime = 1.5f;


	private CapsuleCollider cc;

	public float timeToRegen;

	// Use this for initialization
	void Start () {
		timeToRegen = disruptTime;
	}
	
	// Update is called once per frame
	void Update () {

		if (disrupted)
			UpdateRegen();

	}

	private void UpdateRegen(){
		timeToRegen -= Time.deltaTime;
		if (timeToRegen < 0) {
			timeToRegen = disruptTime;
			disrupted = false;
			StartCoroutine(RegenBlade());
		}
	}


	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Disruptor") {
			if (!disrupted) {
				disrupted = true;
				StopCoroutine(RegenBlade());
				StartCoroutine(DisruptBlade());
			}
			else
				timeToRegen = disruptTime;
		}
	}

	private IEnumerator DisruptBlade() {
		float lerpVal = 1;
		Vector3 startPos = transform.localPosition;
		Vector3 startSize = transform.localScale;
		while (lerpVal > 0) {
			transform.localPosition = Vector3.Lerp(new Vector3(0, .8f, 0), startPos, lerpVal);
			transform.localScale = Vector3.Lerp(new Vector3(.5f, .1f, .5f), startSize, lerpVal);

			lerpVal -=  15 * Time.deltaTime;

			yield return null;
		}

		transform.localPosition = new Vector3(0, 0.8f, 0);
		transform.localScale = new Vector3(.5f, .1f, .5f);
		timeToRegen = disruptTime;
	}

	private IEnumerator RegenBlade() {
		float lerpVal = 0;
		while (lerpVal < 1 && !disrupted) {
			transform.localPosition = Vector3.Lerp(new Vector3(0, .8f, 0), new Vector3(0, 3.5f, 0), lerpVal);
			transform.localScale = Vector3.Lerp(new Vector3(.5f, .1f, .5f), new Vector3(.5f, bladeLength, .5f), lerpVal);

			lerpVal += 4 * Time.deltaTime;
			yield return null;
		}

		if (!disrupted) {
			print("test");
			transform.localPosition = new Vector3(0, 3.5f, 0);
			transform.localScale = new Vector3(0.5f, bladeLength, 0.5f);
		}
	}

}
