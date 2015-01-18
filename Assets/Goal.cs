using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Goal : ScriptableObject {
	// has location, action (pretty much just determine which animation to show... unless there's a chance of failure?), priority (??), effect (on resources)
	private Vector3 location;
	// public Action action;
	private int priority;
	private Dictionary<string, float> effect; 
	// just an object that contains a value for each resource changed, like { hunger: -10, tiredness: 10 }, etc
	// this doesn't really guarantee a particular interface... but whatever for now, this is how i'd do it in js anyways...

	// all of these must be set when the object is created, and cannot be changed later

	public void Init(Vector3 newLocation, int newPriority, Dictionary<string, float> newEffect) {
		location = newLocation;
		priority = newPriority;
		effect = newEffect;
	}

	public Vector3 GetLocation() {
		Debug.Log(location);
		return location;
	}

	public int GetPriority() {
		return priority;
	}

	// this class is just for storing data, and won't apply it's effect
	public Dictionary<string, float> GetEffect() {
		return effect;
	}
}
