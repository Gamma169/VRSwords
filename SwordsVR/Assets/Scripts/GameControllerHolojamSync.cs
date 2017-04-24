using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Holojam;
using Holojam.Tools;

public class GameControllerHolojamSync : Synchronizable {

	public int numPlayers = 2;

	public string label = "Controller";

	public bool sending;

	public bool[] DamagedPlayers { get { return playersDamaged;}}
	private bool[] playersDamaged;
	public bool[] OffensivePlayers{ get { return playersOffensive;}}
	private bool[] playersOffensive;

	public override string Label {
		get { return label; }
	}

	public override bool Host {
		get { 
			if (BuildManager.IsMasterClient() || BuildManager.IsSpectator())
				return !sending;
			else
				return sending;
		}
	}

	public override bool AutoHost {
		get { return false; }
	}

	/*
	public int bladeOffenseInt {
	get { return data.ints[0]; }
	set { data.ints[0] = value; }
	}
	*/

	public override void ResetData() {
		data = new Holojam.Network.Flake(
			0, 0, numPlayers, numPlayers, 0, false
		);
		playersDamaged = new bool[numPlayers];
		playersOffensive = new bool[numPlayers];
	}

	public void ResetData(int numberPlayers) {
		data = new Holojam.Network.Flake(0, 0, numberPlayers, numberPlayers, 0, false);
		playersDamaged = new bool[numberPlayers];
		playersDamaged = new bool[numberPlayers];
		numPlayers = numberPlayers;
	}


	protected override void Sync() {
		//base.Sync();

		if (Sending) {
			
			for (int i = 0; i < numPlayers; i++) {

				playersOffensive[i] = data.ints[i] == 1;

				playersDamaged[i] = data.floats[i] == 1;
			
			
			}
			//Debug.Log("SynchronizableTemplate: sending data on " + Brand);
		}

		// If this synchronizable is listening for data on the Label
		else {

			for (int i = 0; i < numPlayers; i++) {

				if (i + 1 != BuildManager.BUILD_INDEX) {
					data.ints[i] = playersOffensive[i] ? 1 : 0;

					data.floats[i] = playersDamaged[i] ? 1 : 0;
				}

			}


			/*
			if (Tracked) { // Do something with the incoming data if it's tracked
				Debug.Log(
					"SynchronizableTemplate: data is coming in on " + Brand
					+ " from " + Source
					+ " (MyInt = " + MyInt + ")",
					this
				);

				color = MyColor; // Set the color in the inspector
			}

			// Not tracked--either nobody is hosting on the Label, or this client
			// is not connected to the network
			else {
				Debug.Log(
					"SynchronizableTemplate: no data coming in on " + Brand,
					this
				);
			}
			*/

		}
	}


}
