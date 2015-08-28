using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneWaterGenerator : BaseGenerator {
	public Transform pillarGenerator;
	public Transform prefabFlow;
	public Transform prefabPlant;
	public Transform prefabFish;
	public Transform flows;
	public Transform plants;
	public Transform fish;
	public Transform generatorReference;
	
	public enum SceneType {
		Water
	};

	public SceneType sceneType = SceneType.Water;
	 
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
			GenerateFlow();
			GenerateFish();
			GeneratePlant();
		}

		yield return new WaitForSeconds (1);
		yield return StartCoroutine (Generate());
	}

	public void GenerateFlow() {
		float xOffset = 20;
		float zOffset = 20;
		Vector3 position = Vector3.zero;
		// 1. generate a random coordinate
		while (true) {
			Vector3 randomPosition = new Vector3(Random.Range(-xOffset, xOffset), 0, Random.Range(-zOffset - 40, zOffset - 40));
			Vector3 newPosition = randomPosition + generatorReference.position;
			bool result = CheckFlowCollision(newPosition);
			if (!result) {
				position = newPosition;
				break;
			}
		}
		Transform newFlow = (Transform)GameObject.Instantiate (prefabFlow);
		newFlow.SetParent (flows);
		newFlow.localScale = Vector3.one;
		newFlow.localRotation = Quaternion.Euler (0, 0, 0);
		newFlow.position = position; 
	}

	public void GeneratePlant() {
		float xOffset = Random.Range (-20, 0);
		float zOffset = Random.Range (0, 20);
		
		Transform newPlant = (Transform)GameObject.Instantiate (prefabPlant);
		newPlant.SetParent (plants);
		newPlant.localScale = Vector3.one;
		newPlant.localRotation = Quaternion.Euler (0, 0, 0);
		newPlant.position = generatorReference.position + new Vector3 (xOffset, 0, zOffset);
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

	public void GenerateFish() {
		float xOffset = 20;
		float zOffset = 20;
		float scale = Random.Range (0.5f, 1);
		Vector3 position = Vector3.zero;

		Vector3 randomPosition = new Vector3(Random.Range(-xOffset, xOffset), -10, Random.Range(-zOffset - 40, zOffset - 40));
		Vector3 newPosition = randomPosition + generatorReference.position;

		Transform newFish = (Transform)GameObject.Instantiate (prefabFish);
		newFish.SetParent (fish);
		newFish.localScale = Vector3.one * scale;
		newFish.localRotation = Quaternion.Euler (0, 0, 0);
		newFish.position = newPosition; 
	}
}
