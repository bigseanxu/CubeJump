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

	public float flowInterval = 2f;
	public float plantInterval = 2f;
	public float fishInterval = 2f;

	public Vector2 fishScale = new Vector2(0.1f, 0.3f);
	public Vector2 fishY = new Vector2(-10f, -5f);

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

		StartCoroutine(GenerateFlow());
		StartCoroutine(GenerateFish());
		StartCoroutine(GeneratePlant());
	
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
		newPlant.localRotation = Quaternion.Euler (0, 0, 0);
		newPlant.position = generatorReference.position + new Vector3 (xOffset, 0, zOffset);
		
		yield return new WaitForSeconds (plantInterval);
		yield return StartCoroutine (GeneratePlant());
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

	IEnumerator GenerateFish() {
		float xOffset = 20;
		float zOffset = 20;
		float scale = Random.Range (fishScale.x, fishScale.y);
		Vector3 position = Vector3.zero;

		Vector3 randomPosition = new Vector3(Random.Range(-xOffset, xOffset), Random.Range(fishY.x, fishY.y), Random.Range(-zOffset - 40, zOffset - 40));
		Vector3 newPosition = randomPosition + generatorReference.position;

		Transform newFish = (Transform)GameObject.Instantiate (prefabFish);
		newFish.SetParent (fish);
		newFish.localScale = Vector3.one * scale;
		newFish.localRotation = Quaternion.Euler (0, 0, 0);
		newFish.position = newPosition; 
		
		yield return new WaitForSeconds (fishInterval);
		yield return StartCoroutine (GenerateFish());
	}
}
