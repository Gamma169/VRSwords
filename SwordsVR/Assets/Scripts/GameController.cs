using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Holojam.Tools;

public class GameController : MonoBehaviour {

	public int currentBuild = 0;

	public PlayerController[] players;

	private bool[] playersDamaged;
	public bool[] playersOffensive;


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
    //syncControllers[0].SetDamagedOffensiveArrays(playersDamaged, playersOffensive);
		syncControllers[1].sending = false;
		syncControllers[1].label = "Controller-Receive";
		syncControllers[1].ResetData(players.Length);
    //syncControllers[0].SetDamagedOffensiveArrays(playersDamaged, playersOffensive);

    currentBuild = BuildManager.BUILD_INDEX;

		playersDamaged = new bool[players.Length];
		playersOffensive = new bool[players.Length];

	}
	
	// Update is called once per frame
	void Update () {
    currentBuild = BuildManager.BUILD_INDEX;
    // Don't think I need this becuase ideally both bodies are synced up, so if you hit the enemy, he should be hit in both versions and they both know what happened.
    // I might need to change this up a bit later, though
    /*
		mainPlayerHit = mainPlayer.damaged;
		otherPlayer.damaged = otherPlayerHit;
		*/

    for (int i = 0; i < players.Length; i++) {
			// Setting Value based on Sync Component
			if (i + 1 != currentBuild) {
				playersDamaged[i] = receivingSync.DamagedPlayers[i];
				playersOffensive[i] = receivingSync.OffensivePlayers[i];

				players[i].SetDamageOffensive(playersDamaged[i], playersOffensive[i]);
			}
			// Setting Value based on player
			else {
				playersDamaged[i] = players[i].IsDamaged();
				playersOffensive[i] = players[i].IsOffensive();

        sendingSync.playersOffensive[i] = playersOffensive[i];
			}
		}

	}
}
