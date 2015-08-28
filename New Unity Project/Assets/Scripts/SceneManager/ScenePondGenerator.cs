using UnityEngine;
using System.Collections;
using System.Timers;
using System.Collections.Generic;

public class ScenePondGenerator : BaseGenerator {
	public Transform pillarGenerator;
	public Transform prefabFlow;
	public Transform prefabPlant;
	public Transform prefabCrocodile;
	public Transform prefabButterfly;
	public Transform flows;
	public Transform plants;
	public Transform Crocodiles;
	public Transform Butterflies;
	public Transform prefabFrog;
	public Transform prefabLotus;
	public Transform frog;
	public Transform lotus;
	public Transform generatorReference;

	public enum SceneType {
		Pond
	};

	public SceneType sceneType = SceneType.Pond;
	 
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
			GeneratePlant();
			int a=Random.Range(0,10);
			if(a>3)
				GenerateFrog();
			if(a>3)
				GenerateLotus();
			if(a>6)
				GenerateCo();
			if(a>6)
				GenerateButterfly();
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
		newPlant.localRotation = Quaternion.Euler (270, 0, 0);
		newPlant.position = generatorReference.position + new Vector3 (xOffset, 0, zOffset);
	}
	

	public void GenerateFrog() {
		float xOffset = 20;
		float zOffset = 20;
		float scale = Random.Range (8f, 12f);
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
		Transform newFrog = (Transform)GameObject.Instantiate (prefabFrog);
		newFrog.SetParent (frog);
		newFrog.localScale = Vector3.one*scale;
		newFrog.localRotation = Quaternion.Euler (270, 0, 0);
		newFrog.position = position; 
	}

	public void GenerateLotus() {
		float xOffset = 20;
		float zOffset = 20;
		Vector3 position = Vector3.zero;
		float scale = Random.Range (8f, 12f);
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
		Transform newLotus = (Transform)GameObject.Instantiate (prefabLotus);
		newLotus.SetParent (lotus);
		newLotus.localScale = Vector3.one*scale;
		newLotus.localRotation = Quaternion.Euler (270, 0, 0);
		newLotus.position = position; 
	}

	public void GenerateCo() {
		float xOffset = 20;
		float zOffset = 20;
		Vector3 position = Vector3.zero;
		float scale = Random.Range (8f, 12f);
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
		Transform newLotus = (Transform)GameObject.Instantiate (prefabCrocodile);
		newLotus.SetParent (Crocodiles);
		newLotus.localScale = Vector3.one*scale;
		newLotus.localRotation = Quaternion.Euler (270, 0, 0);
		newLotus.position = position; 
	}

	public void GenerateButterfly() {
		float xOffset = 20;
		float zOffset = 20;
		float scale = Random.Range (8f, 12f);
		Vector3 position = Vector3.zero;
		
		Vector3 randomPosition = new Vector3(Random.Range(-xOffset, xOffset), 12, Random.Range(-zOffset - 40, zOffset - 40));
		Vector3 newPosition = randomPosition + generatorReference.position;
		
		Transform newFish = (Transform)GameObject.Instantiate (prefabButterfly);
		newFish.SetParent (Butterflies);
		newFish.localScale = Vector3.one * scale;
		newFish.localRotation = Quaternion.Euler (270, 0, 0);
		newFish.position = newPosition; 
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
