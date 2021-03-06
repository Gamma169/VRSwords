﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHittableBodyPart : MonoBehaviour {

	public PlayerController pc;

	private bool damaged;

	// Use this for initialization
	void Start () {
		
	}
		
	// Update is called once per frame
	void Update () {
		damaged = pc.IsDamaged();
	}

	void OnTriggerEnter(Collider col) {
	    if (col.gameObject.tag == "EnergyBlade" && !damaged) {
			EnergyBlade otherBlade = col.gameObject.GetComponent<EnergyBlade>();
			if (!otherBlade.Equals(pc.GetLeftBlade()) && otherBlade.IsOffensive() && !damaged) {
	        	StartCoroutine(pc.HitByBlade());
	    	}
	    }
	}

	void OnTriggerStay(Collider col) {
		if (col.gameObject.tag == "EnergyBlade") {
    		EnergyBlade otherBlade = col.gameObject.GetComponent<EnergyBlade>();
			if (!otherBlade.Equals(pc.GetLeftBlade()) && otherBlade.IsOffensive() && !damaged) {
				StartCoroutine(pc.HitByBlade());
			}
		}
	}


}
