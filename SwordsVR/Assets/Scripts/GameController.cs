using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Holojam.Tools;

public class GameController : MonoBehaviour {

	public int currentBuild = 0;

	public PlayerController[] players;

	public bool[] playersDamaged;
	public bool[] playersOffensive;


	/*
	private GameControllerHolojamSync[] syncControllers;
	public GameControllerHolojamSync sendingSync { get { return syncControllers[0];}}
	public GameControllerHolojamSync receivingSync { get { return syncControllers[1];}}
	*/

	// Use this for initialization
	void Awake () {

		playersDamaged = new bool[players.Length];
		playersOffensive = new bool[players.Length];
		currentBuild = BuildManager.BUILD_INDEX;

		/*
		syncControllers = GetComponents<GameControllerHolojamSync>();
		syncControllers[0].sending = true;
		syncControllers[0].label = "Controller-Send";
		syncControllers[0].ResetData(players.Length);
		//syncControllers[0].SetDamagedOffensiveArrays(playersDamaged, playersOffensive);
		syncControllers[1].sending = false;
		syncControllers[1].label = "Controller-Receive";
		syncControllers[1].ResetData(players.Length);
		//syncControllers[0].SetDamagedOffensiveArrays(playersDamaged, playersOffensive);

		GameControllerHolojamSync foo = gameObject.AddComponent<GameControllerHolojamSync>() as GameControllerHolojamSync;
		*/

	}
	
	// Update is called once per frame
	void Update () {
  		currentBuild = BuildManager.BUILD_INDEX;
 
		for (int i = 0; i < players.Length; i++) {
			// If we're the MasterClient, we set everything based on what we recieve
			if (BuildManager.IsMasterClient()) {
				/*
				playersDamaged[i] = sendingSync.DamagedPlayers[i];
				playersOffensive[i] = sendingSync.OffensivePlayers[i];
				receivingSync.playersOffensive[i] = playersOffensive[i];
				receivingSync.playersDamaged[i] = playersDamaged[i];	
				*/
			}
			// If we're not, Setting Value based on Sync Component
			else if (i + 1 != currentBuild) {
				/*
				playersDamaged[i] = receivingSync.DamagedPlayers[i];
				playersOffensive[i] = receivingSync.OffensivePlayers[i];

				players[i].SetDamageOffensive(playersDamaged[i], playersOffensive[i]);
				*/
			}
			// Setting Value based on player
			else {
				playersDamaged[i] = players[i].IsDamaged();
				playersOffensive[i] = players[i].IsOffensive();

				/*
        		sendingSync.playersOffensive[i] = playersOffensive[i];
				sendingSync.playersDamaged[i] = playersDamaged[i];

				receivingSync.playersOffensive[i] = playersOffensive[i];
				receivingSync.playersDamaged[i] = playersDamaged[i];
				*/

			}
		}

	}
}
