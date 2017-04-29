using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Holojam.Tools;

public class GameControllerPIinfoHolojamSender : Synchronizable {

	public int sendingPInfo = 1;

	public bool playerDamaged;

	public bool playerOffensive;

	public override string Label {
		get { return "SendingPInfo-" + sendingPInfo; }
	}

	public override bool Host {
		get { 
			return !BuildManager.IsMasterClient();
		}
	}

	public override bool AutoHost {
		get { return false; }
	}


	public int playerOffenseInt {
	get { return data.ints[0]; }
	set { data.ints[0] = value; }
	}
	public float playerDamagedFloat {
		get { return data.floats[0]; }
		set { data.floats[0] = value; }
	}


	public override void ResetData() {
		data = new Holojam.Network.Flake(
			0, 0, 1, 1, 0, false
		);
	}
		
	protected override void Sync() {
		//base.Sync();

		if (sendingPInfo == 2) {
			if (Input.GetKeyDown("n"))
				playerDamaged = true;
			if (Input.GetKeyUp("n"))
				playerDamaged = false;
		}

		if (Sending) {

			/*
			for (int i = 0; i < numPlayers; i++) {
				data.ints[i] = playersOffensive[i] ? 1 : 0;

				data.floats[i] = playersDamaged[i] ? 1 : 0;
			}
			//Debug.Log("SynchronizableTemplate: sending data on " + Brand);
			*/

			playerDamagedFloat = playerDamaged ? 1 : 0; 
			playerOffenseInt = playerOffensive ? 1 : 0;

		}

		// If this synchronizable is listening for data on the Label
		else {

			playerDamaged = playerDamagedFloat == 1;
			playerOffensive = playerOffenseInt == 1;


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
