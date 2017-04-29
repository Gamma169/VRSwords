using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Holojam.Tools;

public class GameControllerPIinfoHolojamSender : Synchronizable {

	public int sendingPInfo = 1;

	public string label = "SendingPInfo-1";



	public bool[] DamagedPlayers { get { return playersDamaged;}}
	public bool[] playersDamaged;
	public bool[] OffensivePlayers{ get { return playersOffensive;}}
	public bool[] playersOffensive;

	public override string Label {
		get { return label; }
	}

	public override bool Host {
		get { 
			return !BuildManager.IsMasterClient();
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
			0, 0, 1, 1, 0, false
		);
		label = "SendingPInfo-" + sendingPInfo;
	}




	protected override void Sync() {
		//base.Sync();

		if (Sending) {

			/*
      
			for (int i = 0; i < numPlayers; i++) {
				data.ints[i] = playersOffensive[i] ? 1 : 0;

				data.floats[i] = playersDamaged[i] ? 1 : 0;
			}
			//Debug.Log("SynchronizableTemplate: sending data on " + Brand);

			*/
		}

		// If this synchronizable is listening for data on the Label
		else {




			/*
			for (int i = 0; i < numPlayers; i++) {

			
				if (i + 1 != BuildManager.BUILD_INDEX) {
					data.ints[i] = playersOffensive[i] ? 1 : 0;

					data.floats[i] = playersDamaged[i] ? 1 : 0;
				}
    		  
				if (i + 1 != BuildManager.BUILD_INDEX) {
					playersOffensive[i] = data.ints[i] == 1;
					playersDamaged[i] = data.floats[i] == 1;
				}
			}
			*/

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
