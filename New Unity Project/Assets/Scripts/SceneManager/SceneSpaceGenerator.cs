using UnityEngine;
using System.Collections;
using System.Timers;
using System.Collections.Generic;

public class SceneSpaceGenerator : BaseGenerator {
	public Transform pillarGenerator;

	public Transform prefabLine;
	public Transform Lines;
	public Transform prefabLineLeft;
	public Transform generatorReference;


	public enum SceneType {
		Space
	};

	public SceneType sceneType = SceneType.Space;
	 
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void StartGenerate() {
		StartCoroutine (Generate());
	}
	
	IEnumerator Generate() {
		if (Game.state == Game.State.Gaming) { 
			GenerateLine();
			GenerateLineLeft() ;
		}
		
		yield return new WaitForSeconds (1);
		yield return StartCoroutine (Generate());
	}


	public void GenerateLine() {
		float xOffset = 20;
		float zOffset = 20;
		Vector3 position = Vector3.zero;
		// 1. generate a random coordinate
		while (true) {
			Vector3 randomPosition = new Vector3(Random.Range(-xOffset, xOffset), Random.Range(-10, 10), Random.Range(-zOffset - 40, zOffset - 40));
			Vector3 newPosition = randomPosition + generatorReference.position;
			bool result = CheckFlowCollision(newPosition);
			if (!result) {
				position = newPosition;
				break;
			}
		}
		Transform newFlow = (Transform)GameObject.Instantiate (prefabLine);
		newFlow.SetParent (Lines);
		newFlow.localScale = Vector3.one;
		newFlow.localRotation = Quaternion.Euler (90, 0, 0);
		newFlow.position = position; 
	}
	public void GenerateLineLeft() {
		float xOffset = 20;
		float zOffset = 20;
		Vector3 position = Vector3.zero;
		// 1. generate a random coordinate
		while (true) {
			Vector3 randomPosition = new Vector3(Random.Range(-xOffset, xOffset), Random.Range(-10, 10), Random.Range(-zOffset - 40, zOffset - 40));
			Vector3 newPosition =generatorReference.position-randomPosition+new Vector3(30,0,-30);
			bool result = CheckFlowCollision(newPosition);
			if (!result) {
				position = newPosition;
				break;
			}
		}
		Transform newFlow = (Transform)GameObject.Instantiate (prefabLineLeft);
		newFlow.SetParent (Lines);
		newFlow.localScale = Vector3.one;
		newFlow.localRotation = Quaternion.Euler (90, 0, 90);
		newFlow.position = position; 
	}


	bool CheckFlowCollision(Vector3 pos) {
		bool ret = false;
		List<Transform> pillars = pillarGenerator.GetComponent<PillarGenerator> ().GetPillars ();
		for (int i = 0; i < pillars.Count; i++) {
			if (Mathf.Abs (pillars [i].position.x - pos.x) < 2) {
				ret = true;
				break;
			}
		}
		
		return ret;
	}

}
