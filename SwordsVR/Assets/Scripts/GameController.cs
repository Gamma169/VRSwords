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

	private GameControllerHolojamReceiver receiver;
	private GameControllerPIinfoHolojamSender[] senders;

	// Use this for initialization
	void Awake () {

		playersDamaged = new bool[players.Length];
		playersOffensive = new bool[players.Length];
		currentBuild = BuildManager.BUILD_INDEX;

		receiver = GetComponent<GameControllerHolojamReceiver>();
		receiver.DamagedPlayers = playersDamaged;
		receiver.OffensivePlayers = playersOffensive;
		receiver.ResetData(players.Length);

		GameControllerPIinfoHolojamSender gcps;
		if (BuildManager.IsMasterClient() && players.Length > 1) {
			for (int i = 1; i < players.Length; i++) {
				gcps = gameObject.AddComponent<GameControllerPIinfoHolojamSender>() as GameControllerPIinfoHolojamSender;
				gcps.sendingPInfo = 0;
			}
		}

		senders = GetComponents<GameControllerPIinfoHolojamSender>();
		if (BuildManager.IsMasterClient()) {
			for (int i = 0; i < senders.Length; i++) {
				senders[i].sendingPInfo = i + 1;
			}
		}
		else {
			senders[0].sendingPInfo = BuildManager.BUILD_INDEX;
		}


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
				
				playersDamaged[i] = senders[i].playerDamaged;
				playersOffensive[i] = senders[i].playerOffensive;

				players[i].SetDamageOffensive(playersDamaged[i], playersOffensive[i]);
			}
			// If we're not, Setting Value based on Sync Component
			else if (i + 1 != currentBuild) {

				// The receiver and the controller share references to the boolean arrays, so they're updated there
				players[i].SetDamageOffensive(playersDamaged[i], playersOffensive[i]);

			}
			// Setting Value based on player
			else {
				playersDamaged[i] = players[i].IsDamaged();
				playersOffensive[i] = players[i].IsOffensive();

				senders[0].playerDamaged = playersDamaged[i];
				senders[0].playerOffensive = playersOffensive[i];

			}
		}


		/*
		for (int i = 0; i < senders.Length; i++) {
			senders[i].playerDamaged = playersDamaged[i];
			senders[i].playerOffensive = playersOffensive[i];
		}
		*/


	}
}
