using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Holojam;
using Holojam.Tools;

public class GameControllerHolojamSync : SynchronizableTrackable {

	public string label = "Controller";

	//public EnergyBlade blade;

	//private bool bladeOffensive;


	public override string Label {
		get { return label; }
	}

	public override bool Host {
		get { return  !BuildManager.IsMasterClient(); }
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
			0, 0, 0, 0, 0, false
		);
	}


	protected override void Sync() {
		//base.Sync();

		if (Sending) {
			
			/*
			if (blade != null) {
				bladeOffenseInt = (blade.IsOffensive() ? 1 : 0);
			}
			*/
			Debug.Log("SynchronizableTemplate: sending data on " + Brand);
		}

		// If this synchronizable is listening for data on the Label
		else {



			/*
			if (blade != null) {
				blade.SetOffenseMode(bladeOffenseInt == 1);
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
