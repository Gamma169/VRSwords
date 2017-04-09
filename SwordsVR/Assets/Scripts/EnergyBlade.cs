using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBlade : MonoBehaviour {

	// a testing variable so that I can control two swords with different keys
	public bool testSword;

	public float basicBladeLength = 2.5f;
	public float disruptTime = 1.5f;

	private CapsuleCollider cc;
	private Material mat;
	private Color regColor;

	private bool attackActive;

	private bool disrupted;
	private float timeToRegen;

	//This is the latest energy blade collided with so we don't have to grab it every frame.  It gets reset when you exit the collider
	private EnergyBlade otherBlade;

	// Use this for initialization
	void Start () {

		mat = GetComponent<MeshRenderer>().material;
		regColor = mat.color;

		timeToRegen = disruptTime;
	}
	
	// Update is called once per frame
	void Update () {

		if (disrupted)
			UpdateRegen();

		if (Input.GetKey(KeyCode.Space) && !testSword) {
			mat.color = Color.red;
			attackActive = true;
		}
		else { 
			mat.color = regColor;
			attackActive = false;
		}

	}

	private void UpdateRegen(){
		timeToRegen -= Time.deltaTime;
		if (timeToRegen < 0) {
			timeToRegen = disruptTime;
			disrupted = false;
			StartCoroutine(RegenBlade());
		}
	}

 
	void OnCollisionEnter(Collision collision) {
    	
		if (collision.collider.gameObject.tag.Equals("EnergyBlade")) {
			EnergyBlade blade = collision.collider.gameObject.GetComponent<EnergyBlade>();
			if (blade != null) {
				print("test");
			}
			else {
				print("Error: Blade Collided with something tagged as 'EnergyBlade' but didn't have an EnergyBlade component script attached");
			}
		}

	}

	void OnCollisionExit(Collision collision) {
	
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
			transform.localPosition = Vector3.Lerp(new Vector3(0, .95f, 0), startPos, lerpVal);
			transform.localScale = Vector3.Lerp(new Vector3(.5f, .1f, .5f), startSize, lerpVal);

			lerpVal -=  15 * Time.deltaTime;

			yield return null;
		}

		transform.localPosition = new Vector3(0, .95f, 0);
		transform.localScale = new Vector3(.5f, .1f, .5f);
		timeToRegen = disruptTime;
	}

	private IEnumerator RegenBlade() {
		float lerpVal = 0;
		Vector3 startPos = transform.localPosition;
		Vector3 startSize = transform.localScale;
		while (lerpVal < 1 && !disrupted) {
			transform.localPosition = Vector3.Lerp(startPos, new Vector3(0, 3.5f, 0), lerpVal);
			transform.localScale = Vector3.Lerp(startSize, new Vector3(.5f, basicBladeLength, .5f), lerpVal);

			lerpVal += 4 * Time.deltaTime;
			yield return null;
		}
		if (!disrupted) {
			transform.localPosition = new Vector3(0, 3.5f, 0);
			transform.localScale = new Vector3(0.5f, basicBladeLength, 0.5f);
		}
	}

	public bool IsDisrupted() {
		return disrupted;
	}

	public bool IsAttackActive() {
		return attackActive;
	}

}
