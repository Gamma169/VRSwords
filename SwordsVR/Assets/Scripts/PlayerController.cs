using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public bool isMainPlayer;

	public GameObject VRHead;

	public EnergyBlade blade;

	public bool damaged;
	public bool bladeOffensive;

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

		/*
		if (VRHead != null) {
			mrs[0].enabled = false;
		} 
		*/

		blade.mainPlayerSword = isMainPlayer;
	}
	
	// Update is called once per frame
	void Update () {

		if (VRHead != null) {
			gameObject.transform.position = VRHead.transform.position;
			gameObject.transform.eulerAngles = new Vector3(0, VRHead.transform.eulerAngles.y, 0);
		}

		UpdateDamageIndication();

		if (isMainPlayer)
			bladeOffensive = blade.IsOffensive();
		else
			blade.SetOffenseMode(bladeOffensive);
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "EnergyBlade") {
			EnergyBlade otherBlade = col.gameObject.GetComponent<EnergyBlade>();
			if ( !otherBlade.Equals(blade) && !damaged && otherBlade.IsOffensive()) {
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
}
