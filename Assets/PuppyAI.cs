using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PuppyAI : MonoBehaviour {

	private AstarAI astar;
	private Dictionary<string, float> resources;
	private float resourceMin = 0;
	private float resourceMax = 100;

	void OnEnable()
    {
        EventManager.OnWolfGoalReached += ApplyEffectsWrapper;
    }
    

	// Use this for initialization
	void Start () {
		astar = GetComponent<AstarAI>();

		UnityEngine.Random.seed = (int) System.DateTime.Now.Ticks;

		resources = new Dictionary<string, float>(){
		    {"food", resourceMax},
		    {"rest", resourceMax},
		    {"health", resourceMax}
		};
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {

			// should be from -50 to 50
			float x = (UnityEngine.Random.value * 100) - 50;
			float z = (UnityEngine.Random.value * 100) - 50;

			// create a goal
			Goal goal = ScriptableObject.CreateInstance("Goal") as Goal;
			goal.Init(new Vector3(x, 0, z), 1, new Dictionary<string, float>());

			// set the goal's location as target for wuff
			astar.SetTargetPosition(goal.GetLocation());
		}
	}

	void ApplyEffectsWrapper() {
		Dictionary<string, float> test = new Dictionary<string, float>(){
		    {"food", 50},
		    {"rest", 50},
		    {"health", -50}
		};

		ApplyEffects(test);
	}

	// the resource system will also apply to the player, so abstract these out later
	void ApplyEffects (Dictionary<string, float> effects) {
		// for each effect, check that the key also exists in resources
		foreach (var pair in effects) {
			if (resources.ContainsKey(pair.Key)) {
				Debug.Log("has key!");
				// apply effect
				resources[pair.Key] += effects[pair.Key];

				// constrain to min and max values
				resources[pair.Key] = Mathf.Clamp(resources[pair.Key], resourceMin, resourceMax);

				// then check if the wolf is like, dead
				// maybe fire an event for each that reaches 0...?
				if (resources[pair.Key] == resourceMin) {
					// wolf is dead
					EventManager.TriggerWolfDead();
				}
			}
		}
	}
}
