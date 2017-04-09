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

	private bool offenseMode;

	public bool disrupted;
	private float powerLevel;
	public float timeToRegen;
	private float powerLevelLastFrame;

	//This is the latest energy blade collided with so we don't have to grab it every frame.  It gets reset when you exit the collider
	//private EnergyBlade otherBlade;

	// Use this for initialization
	void Start () {

		mat = GetComponent<MeshRenderer>().material;
		regColor = mat.color;

		timeToRegen = disruptTime;
		powerLevel = 100;
		powerLevelLastFrame = 100;
	}
	
	// Update is called once per frame
	void Update () {




		if ((Input.GetKey(KeyCode.Space) || /*Input.GetButton()*/ false) && !testSword) {
			mat.color = Color.red;
			offenseMode = true;
		}
		else { 
			mat.color = regColor;
			offenseMode = false;
		}

		UpdateRegen();
		AdjustSwordLength();


		//if (!testSword && otherBlade != null)
		//	print(otherBlade.offenseMode);
	}

	private void UpdateRegen(){
		if (disrupted && powerLevelLastFrame <= powerLevel) {
			timeToRegen -= Time.deltaTime;
			if (timeToRegen < 0) {
				timeToRegen = disruptTime;
				disrupted = false;
				StartCoroutine(RegenBlade());
			}
		}
		else {
			timeToRegen = disruptTime;
		}
		powerLevelLastFrame = powerLevel;
	}

	private void AdjustSwordLength() {
		float lerpVal = powerLevel / 100f;
		transform.localPosition = Vector3.Lerp(new Vector3(0, 0.95f, 0), new Vector3(0, 3.5f, 0), lerpVal);
		transform.localScale = Vector3.Lerp(new Vector3(.5f, .1f, .5f), new Vector3(.5f, basicBladeLength, .5f), lerpVal);
	}

 	
	void OnCollisionEnter(Collision collision) {
		
		if (collision.collider.gameObject.tag.Equals("EnergyBlade")) {
			EnergyBlade blade = collision.collider.gameObject.GetComponent<EnergyBlade>();
			if (blade != null) {
				//otherBlade = blade;
				if (offenseMode) {
					
				}
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
		float timeToDisrupt = .07f;
		//float timeToDisrupt = 1f;
		while (powerLevel > 0) {
			powerLevel -=  100 * (Time.deltaTime / timeToDisrupt);
			yield return null;
		}
		powerLevel = 0;
		timeToRegen = disruptTime;
	}

	private IEnumerator RegenBlade() {
		float timeToRegen = .25f;
		while (powerLevel < 100 && !disrupted) {
			powerLevel += 100 * (Time.deltaTime / timeToRegen);
			yield return null;
		}
		if (!disrupted) {
			powerLevel = 100;
		}
	}

	public bool IsDisrupted() {
		return disrupted;
	}

	public bool IsOffensive() {
		return offenseMode;
	}

}
