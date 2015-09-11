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
	public float flowRange = 20;
	public float flowXOffset;
	public Vector2 flowYOffset;
	public Vector2 flowZOffset;
	
	public float plantInterval = 2f;
	public float plantRange = 20;
	public float plantXOffset;
	public Vector2 plantYOffset;
	public Vector2 plantZOffset;

	public float fishInterval = 2f;
	public Vector2 fishScale = new Vector2(0.1f, 0.3f);
	public float fishRange = 20;
	public float fishXOffset;
	public Vector2 fishYOffset = new Vector2(-10f, -5f);
	public Vector2 fishZOffset;

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
		Vector3 position;
		// 1. generate a random coordinate
		while (true) {
			float xOffset = Random.Range (-flowRange, flowRange) + flowXOffset;
			float yOffset = Random.Range (flowYOffset.x, flowYOffset.y);
			float zOffset = Random.Range (flowZOffset.x, flowZOffset.y);
			Vector3 randomPosition = new Vector3(xOffset, yOffset, zOffset);
			Vector3 newPosition = randomPosition + transform.worldToLocalMatrix.MultiplyPoint(generatorReference.position);
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
		newFlow.localPosition = position; 

		yield return new WaitForSeconds (flowInterval);
		yield return StartCoroutine (GenerateFlow());
	}

	IEnumerator GeneratePlant() {
		float xOffset = Random.Range (-plantRange, plantRange) + plantXOffset;
		float yOffset = Random.Range (plantYOffset.x, plantYOffset.y);
		float zOffset = Random.Range (plantZOffset.x, plantZOffset.y);
		Vector3 randomPosition = new Vector3(xOffset, yOffset, zOffset);
		Vector3 newPosition = randomPosition + transform.worldToLocalMatrix.MultiplyPoint(generatorReference.position);
		
		Transform newPlant = (Transform)GameObject.Instantiate (prefabPlant);
		newPlant.SetParent (plants);
		newPlant.localScale = Vector3.one;
		newPlant.localRotation = Quaternion.Euler (0, 0, 0);
		newPlant.localPosition = newPosition;
		
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
		float xOffset = Random.Range (-fishRange, fishRange) + fishXOffset;
		float yOffset = Random.Range (fishYOffset.x, fishYOffset.y);
		float zOffset = Random.Range (fishZOffset.x, fishZOffset.y);

		float scale = Random.Range (fishScale.x, fishScale.y);
	
		Vector3 randomPosition = new Vector3(xOffset, yOffset, zOffset);
		Vector3 newPosition = randomPosition + transform.worldToLocalMatrix.MultiplyPoint(generatorReference.position);

		Transform newFish = (Transform)GameObject.Instantiate (prefabFish);
		newFish.SetParent (fish);
		newFish.localScale = Vector3.one * scale;
		newFish.localRotation = Quaternion.Euler (0, 0, 0);
		newFish.localPosition = newPosition; 
		
		yield return new WaitForSeconds (fishInterval);
		yield return StartCoroutine (GenerateFish());
	}
}
