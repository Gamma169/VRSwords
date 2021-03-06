﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {

	public GameObject VRHead;
	public GameObject VRLeft;
	public GameObject VRRight;

	public GameObject playerLeftHand;
	public GameObject playerRightHand;

	public int playerIndex;

	private TestCustomActor actorScript;

	private TrackableObject leftTrackable;
	private TrackableObject rightTrackable;

	private EnergyBlade leftBlade;
	private EnergyBlade rightBlade;

	public bool isBuild { get { return isBuildPlayer; }}
	private bool isBuildPlayer;

	private bool damaged;
	private bool bladeOffensive;

	private MeshRenderer[] mrs;
	private Material headMat;
	private Material bodyMat;

	private Color regColor;

	private string leftHandLabel;
	private string rightHandLabel;

	// Use this for initialization
	void Start () {

		mrs = GetComponentsInChildren<MeshRenderer>();
		headMat = mrs[0].material;
		bodyMat = mrs[1].material;
		regColor = headMat.color;

		actorScript = GetComponent<TestCustomActor>();
		leftTrackable = playerLeftHand.GetComponent<TrackableObject>();
		rightTrackable = playerRightHand.GetComponent<TrackableObject>();

		leftBlade = playerLeftHand.GetComponentInChildren<EnergyBlade>();
		rightBlade = playerRightHand.GetComponentInChildren<EnergyBlade>();

		leftHandLabel = "L" + playerIndex;
		rightHandLabel = "R" + playerIndex;

		SyncVarsWithActorScript();

		//VRHead = GameObject.FindGameObjectWithTag("MainCamera");
   		

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

			if (VRLeft != null) {
				if (VRLeft.activeSelf) {
					playerLeftHand.transform.parent = this.transform.parent;
					playerLeftHand.transform.position = VRLeft.transform.position;
					playerLeftHand.transform.rotation = VRLeft.transform.rotation;
				}
				else {
					//playerLeftHand.transform.position = new Vector3(transform.position.x - .35f, transform.position.y - .6f, transform.position.z + .35f);
					playerLeftHand.transform.parent = this.transform;
					playerLeftHand.transform.localPosition = new Vector3(-1.75f, -1.5f, 1.75f);
				}
			}
			if (VRRight != null) {
				if (VRRight.activeSelf) {
					playerRightHand.transform.parent = this.transform.parent;
					playerRightHand.transform.position = VRRight.transform.position;
					playerRightHand.transform.rotation = VRRight.transform.rotation;
				}
				else {
					//playerRightHand.transform.position = new Vector3(transform.position.x + .35f, transform.position.y - .6f, transform.position.z + .35f);
					playerRightHand.transform.parent = this.transform;
					playerRightHand.transform.localPosition = new Vector3(1.75f, -1.5f, 1.75f);
				}
			}
		} 
		else {
			gameObject.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
		}

		UpdateDamageIndication();

		if (isBuildPlayer) {
			if (leftBlade != null)
				bladeOffensive = leftBlade.IsOffensive();
		}
		else {
			if (leftBlade != null)
				leftBlade.SetOffenseMode(bladeOffensive);
		}


		if (isBuildPlayer) {
			if (Input.GetKeyDown("k"))
				damaged = true;
			if (Input.GetKeyUp("k"))
				damaged = false;
		}
	}

	void OnTriggerEnter(Collider col) {
		if (isBuildPlayer && col.gameObject.tag == "EnergyBlade") {
			EnergyBlade otherBlade = col.gameObject.GetComponent<EnergyBlade>();
			if (otherBlade != null && !otherBlade.Equals(leftBlade) && !otherBlade.Equals(rightBlade) && otherBlade.IsOffensive() && !damaged) {
				StartCoroutine(HitByBlade());
			}
		}
	}

  	void OnTriggerStay(Collider col) {
		if (isBuildPlayer && col.gameObject.tag == "EnergyBlade") {
      		EnergyBlade otherBlade = col.gameObject.GetComponent<EnergyBlade>();
			if (otherBlade != null && !otherBlade.Equals(leftBlade) && !otherBlade.Equals(rightBlade) && otherBlade.IsOffensive() && !damaged) {
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

	public EnergyBlade GetLeftBlade() {
		return leftBlade;
	}

	public void SetDamageOffensive(bool damage, bool offense) {
		damaged = damage;
		bladeOffensive = offense;
	}

	public bool IsDamaged() { return damaged; }
	public bool IsOffensive() { return bladeOffensive; }

	private void SyncVarsWithActorScript() {
		actorScript.index = playerIndex;
		leftTrackable.associatedPlayer = playerIndex;
		rightTrackable.associatedPlayer = playerIndex;

		isBuildPlayer = actorScript.IsBuild;

		if (leftBlade != null)
			leftBlade.buildPlayerSword = isBuildPlayer;
		if (rightBlade != null)
			rightBlade.buildPlayerSword = isBuildPlayer;

		leftTrackable.label = leftHandLabel;
		rightTrackable.label = rightHandLabel;



		/*
		if (isBuildPlayer) {
			actorScript.thisPlayerDamaged = damaged;
			actorScript.thisPlayerOffensive = bladeOffensive;
		}
		else {
			damaged = actorScript.thisPlayerDamaged;
			bladeOffensive = actorScript.thisPlayerOffensive;
		}
		*/
	}


}
