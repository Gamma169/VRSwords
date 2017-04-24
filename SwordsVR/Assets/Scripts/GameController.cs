using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public PlayerController[] players;

	private bool[] playersDamaged;
	private bool[] playersOffensive;


	private GameControllerHolojamSync[] syncControllers;
	public GameControllerHolojamSync sendingSync { get { return syncControllers[0];}}
	public GameControllerHolojamSync receivingSync { get { return syncControllers[1];}}


	// Use this for initialization
	void Awake () {

		//mainPlayer.isMainPlayer = true;
		//otherPlayer.isMainPlayer = false;

		syncControllers = GetComponents<GameControllerHolojamSync>();
		syncControllers[0].sending = true;
		syncControllers[0].label = "Controller-Send";
		syncControllers[0].ResetData(players.Length);
		syncControllers[1].sending = false;
		syncControllers[1].label = "Controller-Receive";
		syncControllers[1].ResetData(players.Length);


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
