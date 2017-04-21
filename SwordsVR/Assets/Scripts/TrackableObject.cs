using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Holojam.Tools;

public class TrackableObject : SynchronizableTrackable {

  public string label = "Obj";

  public EnergyBlade blade;

  public bool bladeOffensive;


  public override string Label {
    get { return label; }
  }

  public override bool Host {
    get { return  !BuildManager.IsMasterClient(); }
  }

  public override bool AutoHost {
    get { return false; }
  }


  protected override void Sync() {
    if (Sending) {
      // Set the outgoing data
      //MyInt = 8;
      //MyColor = color;

      RawPosition = transform.position;
      RawRotation = transform.rotation;

      if (blade != null)
       bladeOffensive = blade.IsOffensive();


      Debug.Log("SynchronizableTemplate: sending data on " + Brand);
    }

    // If this synchronizable is listening for data on the Label
    else {

      transform.position = TrackedPosition;
      transform.rotation = TrackedRotation;

      if (blade != null)
        blade.SetOffenseMode(bladeOffensive);
      
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
