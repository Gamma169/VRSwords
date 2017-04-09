using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBlade : MonoBehaviour {

	// a testing variable so that I can control two swords with different keys
	public bool testSword;

	public Transform fullBladeTransform;

	public float basicBladeLength = 2.5f;
	public float disruptTime = 1.5f;

	private CapsuleCollider cc;
	private Material mat;
	private Color regColor;

	private ParticleSystem ps;

	private bool offenseMode;

	private bool disrupted;
	private float powerLevel;
	private float timeToRegen;

	private Coroutine recharge;
	private Coroutine regen;

	//This is the latest energy blade collided with so we don't have to grab it every frame.  It gets reset when you exit the collider
	//private EnergyBlade otherBlade;

	// Use this for initialization
	void Start () {

		mat = GetComponent<MeshRenderer>().material;
		regColor = mat.color;

		ps = GetComponentInChildren<ParticleSystem>();

		timeToRegen = disruptTime;
		powerLevel = 100;
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
			
		AdjustSwordLength();

		//if (recharge != null && Input.GetKeyDown("o"))
		//	print(recharge);

	}

	private void AdjustSwordLength() {
		float lerpVal = powerLevel / 100f;
		transform.localPosition = Vector3.Lerp(new Vector3(0, 1, 0), new Vector3(0, 1+basicBladeLength, 0), lerpVal);
		transform.localScale = Vector3.Lerp(new Vector3(.5f, .1f, .5f), new Vector3(.5f, basicBladeLength, .5f), lerpVal);
		fullBladeTransform.localPosition = new Vector3(0, 1 + basicBladeLength, 0);
		fullBladeTransform.localScale = new Vector3(0.5f, basicBladeLength, 0.5f);

		if (ps != null) {
			var sh = ps.shape;
			sh.length = lerpVal;
			var em = ps.emission;
			em.rateOverTime = 20 + (30 * lerpVal);
		}
	}

 	
	void OnCollisionEnter(Collision collision) {
		
		if (collision.collider.gameObject.tag.Equals("EnergyBlade") && !disrupted) {
			EnergyBlade blade = collision.collider.gameObject.GetComponent<EnergyBlade>();
			if (blade != null) {
				//otherBlade = blade;
				if (offenseMode && !disrupted && powerLevel > 0) {
					float localHitpoint = fullBladeTransform.InverseTransformPoint(collision.contacts[0].point).y;
					float newPowerLevel = 50 * localHitpoint + 50;


					if (newPowerLevel < 95 && newPowerLevel > 10 && newPowerLevel < powerLevel) 
						StartCoroutine(DisruptBlade(newPowerLevel));
					else if (newPowerLevel <= 10)
						StartCoroutine(DisruptBlade(0));

				}
			}
			else {
				print("Error: Blade Collided with something tagged as 'EnergyBlade' but didn't have an EnergyBlade component script attached");
			}
		}

	}

	void OnCollisionStay(Collision collision) {
		if (collision.collider.gameObject.tag.Equals("EnergyBlade") && !disrupted) {
			if (offenseMode && powerLevel > 0) {
				float localHitpoint = fullBladeTransform.InverseTransformPoint(collision.contacts[0].point).y;
				float newPowerLevel = 50 * localHitpoint + 50;
				timeToRegen = disruptTime;

				if (newPowerLevel < 95 && newPowerLevel > 10 && newPowerLevel < powerLevel)
					powerLevel = newPowerLevel;
				else if (newPowerLevel <= 10)
					StartCoroutine(DisruptBlade(0));
			}
		}
	}


	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Disruptor" && !disrupted && powerLevel > 0) {
				StartCoroutine(DisruptBlade(0));
		}
	}

	void OnTriggerStay(Collider col) {
		if (col.gameObject.tag == "Disruptor") {
			timeToRegen = disruptTime;
		}
	}


	private IEnumerator DisruptBlade(float toLevel) {
		if (toLevel > powerLevel)
			print("Error: Trying to disrupt sword to a power level greater than it already has.  Nothing done.");
		else {
			disrupted = true;
			if (recharge != null)
				StopCoroutine(recharge);
			if (regen != null)
				StopCoroutine(regen);

			float timeToDisrupt = .07f;
			//float timeToDisrupt = 1f;
			powerLevel -= powerLevel * (Time.deltaTime / timeToDisrupt);
			while (powerLevel > toLevel) {
				yield return null;
				powerLevel -= 100 * (Time.deltaTime / timeToDisrupt);
			}
			powerLevel = toLevel;
			disrupted = false;
			powerLevel = toLevel;
			timeToRegen = disruptTime;

			if (!disrupted)
				recharge = StartCoroutine(RechargeBlade());

		}
	}

	private IEnumerator RechargeBlade() {
		
		while (!disrupted && timeToRegen > 0) {
			timeToRegen -= Time.deltaTime;
			yield return null;
		}

		if (!disrupted)
			regen = StartCoroutine(RegenBlade());
			
	}

	private IEnumerator RegenBlade() {
		float regenTime = .25f;
		while (powerLevel < 100 && !disrupted) {
			powerLevel += 100 * (Time.deltaTime / regenTime);
			yield return null;
		}
		if (!disrupted) {
			powerLevel = 100;
			timeToRegen = disruptTime;
		}
	}

	public bool IsDisrupted() {
		return disrupted;
	}

	public bool IsOffensive() {
		return offenseMode;
	}

}
