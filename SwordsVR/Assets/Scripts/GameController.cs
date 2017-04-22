using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public PlayerController[] players;

	private bool[] playersDamaged;
	private bool[] playersOffensive;


	// Use this for initialization
	void Awake () {

		//mainPlayer.isMainPlayer = true;
		//otherPlayer.isMainPlayer = false;

	}
	
	// Update is called once per frame
	void Update () {

		// Don't think I need this becuase ideally both bodies are synced up, so if you hit the enemy, he should be hit in both versions and they both know what happened.
		// I might need to change this up a bit later, though
		/*
		mainPlayerHit = mainPlayer.damaged;
		otherPlayer.damaged = otherPlayerHit;
		*/

		for (int i = 0; i < players.Length; i++) {
			
		
		}

	}
}
