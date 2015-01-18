using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PuppyAI : MonoBehaviour {

	private AstarAI astar;

	private Dictionary<string, float> resources;
	private float resourceMin = 0;
	private float resourceMax = 100;

	private Goal currentGoal;

	// Use this for initialization
	void Start () {
		astar = GetComponent<AstarAI>();

		UnityEngine.Random.seed = (int) System.DateTime.Now.Ticks;

		resources = new Dictionary<string, float>(){
		    {"food", resourceMax},
		    {"rest", resourceMax},
		    {"health", resourceMax}
		};

		currentGoal = ScriptableObject.CreateInstance("Goal") as Goal;
		SetRandomGoal();

		EventManager.OnWolfGoalReached += ApplyEffectsWrapper;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			SetRandomGoal();

			// set the goal's location as target for wuff
			astar.SetTargetPosition(currentGoal.GetLocation());
		}
	}

	void SetRandomGoal() {
		// should be from -50 to 50
		float x = (UnityEngine.Random.value * 100) - 50;
		float z = (UnityEngine.Random.value * 100) - 50;

		Dictionary<string, float> effects = new Dictionary<string, float>(){
		    {"food", 10},
		    {"rest", 0},
		    {"health", 10}
		};

		// create a goal
		currentGoal.Init(new Vector3(x, 0, z), 1, effects);
	}

	void ApplyEffectsWrapper() {
		ApplyEffects(currentGoal.GetEffect());
	}

	// the resource system will also apply to the player, so abstract these out later
	void ApplyEffects (Dictionary<string, float> effects) {
		// for each effect, check that the key also exists in resources
		foreach (var pair in effects) {
			if (resources.ContainsKey(pair.Key)) {
				// apply effect
				this.resources[pair.Key] += effects[pair.Key];

				// constrain to min and max values
				this.resources[pair.Key] = Mathf.Clamp(this.resources[pair.Key], this.resourceMin, this.resourceMax);

				// then check if the wolf is like, dead
				if (this.resources[pair.Key] == this.resourceMin) {
					// wolf is dead
					EventManager.TriggerWolfDead();
				}
			}
		}
	}
}
