using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public bool isBuildPlayer;

	public GameObject VRHead;

	public EnergyBlade blade;

	public bool damaged;
	public bool bladeOffensive;

	private TestCustomActor actorScript;

	private MeshRenderer[] mrs;
	private Material headMat;
	private Material bodyMat;

	private Color regColor;

	// Use this for initialization
	void Start () {

		mrs = GetComponentsInChildren<MeshRenderer>();
		headMat = mrs[0].material;
		bodyMat = mrs[1].material;
		regColor = headMat.color;

		actorScript = GetComponent<TestCustomActor>();
		SyncVarsWithActorScript();

		//VRHead = GameObject.FindGameObjectWithTag("MainCamera");
		VRHead = GameObject.Find("Camera (head)");

		/*
		if (isBuildPlayer) {
			mrs[0].enabled = false;
		} 
		*/
	}
	
	// Update is called once per frame
	void Update () {
		SyncVarsWithActorScript();


		if (isBuildPlayer) {
			gameObject.transform.position = VRHead.transform.position;
			gameObject.transform.eulerAngles = new Vector3(0, VRHead.transform.eulerAngles.y, 0);
		}

		UpdateDamageIndication();

		if (isBuildPlayer)
			bladeOffensive = blade.IsOffensive();
		else
			blade.SetOffenseMode(bladeOffensive);
	}

	void OnTriggerEnter(Collider col) {
		if (isBuildPlayer && col.gameObject.tag == "EnergyBlade") {
			EnergyBlade otherBlade = col.gameObject.GetComponent<EnergyBlade>();
			if ( !otherBlade.Equals(blade) && !damaged && otherBlade.IsOffensive()) {
				StartCoroutine(HitByBlade());
			}
		}
	}

  	void OnTriggerStay(Collider col) {
		if (isBuildPlayer && col.gameObject.tag == "EnergyBlade") {
      		EnergyBlade otherBlade = col.gameObject.GetComponent<EnergyBlade>();
      		if (!otherBlade.Equals(blade) && !damaged && otherBlade.IsOffensive()) {
	        	StartCoroutine(HitByBlade());
    		}
    	}
  	}

	private void UpdateDamageIndication() {
		if (damaged) {
			headMat.color = Color.gray;
			bodyMat.color = Color.grey;
		}
		else {
			headMat.color = regColor;
			bodyMat.color = regColor;
		}
	}

	public IEnumerator HitByBlade() {
		damaged = true;
		yield return new WaitForSeconds(4);
		damaged = false;
	}

	private void SyncVarsWithActorScript() {
		isBuildPlayer = actorScript.IsBuild;
		blade.mainPlayerSword = isBuildPlayer;

		if (isBuildPlayer) {
			actorScript.thisPlayerDamaged = damaged;
			actorScript.thisPlayerOffensive = bladeOffensive;
		}
		else {
			damaged = actorScript.thisPlayerDamaged;
			bladeOffensive = actorScript.thisPlayerOffensive;
		}
	}
}
