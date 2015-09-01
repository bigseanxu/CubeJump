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

	public float flowInterval = 2f;
	public float plantInterval = 2f;
	public float frogInterval = 2f;
	public float lotusInterval = 2f;
	public float coInterval = 2f;
	public float butterflyInterval = 2f;

	public Vector2 frogScale = new Vector2(8f, 12f);
	public Vector2 lotusScale = new Vector2(8f, 12f);
	public Vector2 coScale = new Vector2(8f, 12f);
	public Vector2 butterflyScale = new Vector2(8f, 12f);

	public Vector2 butterflyY = new Vector2(12f, 12f);

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

		StartCoroutine(GenerateFlow());
		StartCoroutine(GeneratePlant());
		StartCoroutine(GenerateFrog());
        StartCoroutine(GenerateLotus());
        StartCoroutine(GenerateCo());
        StartCoroutine(GenerateButterfly());
	
		yield return null;
	}



	IEnumerator GenerateFlow() {
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

		yield return new WaitForSeconds (flowInterval);
		yield return StartCoroutine (GenerateFlow());
	}

	IEnumerator GeneratePlant() {
		float xOffset = Random.Range (-20, 0);
		float zOffset = Random.Range (0, 20);
		
		Transform newPlant = (Transform)GameObject.Instantiate (prefabPlant);
		newPlant.SetParent (plants);
		newPlant.localScale = Vector3.one;
		newPlant.localRotation = Quaternion.Euler (270, 0, 0);
		newPlant.position = generatorReference.position + new Vector3 (xOffset, 0, zOffset);

		yield return new WaitForSeconds (plantInterval);
		yield return StartCoroutine (GeneratePlant());
	}
	

	IEnumerator GenerateFrog() {
		float xOffset = 20;
		float zOffset = 20;
		float scale = Random.Range (frogScale.x, frogScale.y);
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

		yield return new WaitForSeconds (frogInterval);
		yield return StartCoroutine (GenerateFrog());
	}

	IEnumerator GenerateLotus() {
		float xOffset = 20;
		float zOffset = 20;
		Vector3 position = Vector3.zero;
		float scale = Random.Range (lotusScale.x, lotusScale.y);
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

		yield return new WaitForSeconds (lotusInterval);
		yield return StartCoroutine (GenerateLotus());
	}

	IEnumerator GenerateCo() {
		float xOffset = 20;
		float zOffset = 20;
		Vector3 position = Vector3.zero;
		float scale = Random.Range (coScale.x, coScale.y);
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

		yield return new WaitForSeconds (coInterval);
		yield return StartCoroutine (GenerateCo());
	}

	IEnumerator GenerateButterfly() {
		float xOffset = 20;
		float zOffset = 20;
		float scale = Random.Range (butterflyScale.x, butterflyScale.y);
		float yOffset = Random.Range (butterflyY.x, butterflyY.y);
		Vector3 position = Vector3.zero;
		
		Vector3 randomPosition = new Vector3(Random.Range(-xOffset, xOffset), yOffset, Random.Range(-zOffset - 40, zOffset - 40));
		Vector3 newPosition = randomPosition + generatorReference.position;
		
		Transform newFish = (Transform)GameObject.Instantiate (prefabButterfly);
		newFish.SetParent (Butterflies);
		newFish.localScale = Vector3.one * scale;
		newFish.localRotation = Quaternion.Euler (270, 0, 0);
		newFish.position = newPosition; 

		yield return new WaitForSeconds (butterflyInterval);
		yield return StartCoroutine (GenerateButterfly());
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
