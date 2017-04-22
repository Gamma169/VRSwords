// ActorAvatar.cs
// Created by Holojam Inc. on 05.07.16
// Example Actor extension

using UnityEngine;
using System.Collections;

public class TestCustomActor : Holojam.Tools.Actor {

	//const float BLINK_TIME = 0.03f;
	//readonly Vector2 BLINK_DELAY = new Vector2(1, 11);

	//public Transform head;

	//public bool isCurrentBuild;

	//public bool thisPlayerDamaged;
	//public bool thisPlayerOffensive;

	//public GameObject mask;

	//public Color motif = Holojam.Utility.Palette.Select(DEFAULT_COLOR);
	//public Transform animatedEyes;
	//public Material skinMaterial;

	//float blinkDelay = 0, lastBlink = 0;

	// Override the orientation accessor to match the rotation assignment below
	public override Quaternion Orientation {
		//get { return head != null ? head.rotation : Quaternion.identity; }
		get { return this.transform.rotation; }
	}

	protected override void UpdateTracking() {
		if (Tracked) {
			transform.position = TrackedPosition;
			transform.rotation = TrackedRotation;

			// Use a separate transform for rotation (a head) instead of the default (Actor transform)
			/*
			if (head != null) {
				head.localPosition = Vector3.zero;
				head.rotation = TrackedRotation;
			}
			else Debug.LogWarning("ActorAvatar: No head found for " + gameObject.name, this);
			*/
		}

		// Toggle mask--if this is a build actor, we don't want to render our mesh in
		// front of the camera
		/*
		if (mask != null) {
			mask.SetActive(!IsBuild);

		}
		*/

	}

	// Toggle the head object on fade in and fade out to hide the attached mesh

	protected override void FadeIn() {
		//head.gameObject.SetActive(true);
	}

	protected override void FadeOut() {
		//head.gameObject.SetActive(false);
	}

	void Start() {
		//isCurrentBuild = IsBuild;
	}


	// Blink effect

	void LateUpdate() {
		/*
		if (Application.isPlaying && animatedEyes != null
			&& Time.time > lastBlink + blinkDelay) {
		}
		*/
	}

}