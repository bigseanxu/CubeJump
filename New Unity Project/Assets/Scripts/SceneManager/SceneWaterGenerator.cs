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

	public uint maxFlowCount = 10;
	public float flowInterval = 2f;
	public float flowRange = 20;
	public float flowXOffset;
	public Vector2 flowYOffset;
	public Vector2 flowZOffset;
	public int beginFlowCount = 2;
	public float beginFlowRange = 20;
	public Vector2 beginFlowYOffset = new Vector2(12f, 12f);
	public Vector2 beginFlowZOffset;

	public uint maxPlantCount = 1;
	public float plantInterval = 2f;
	public float plantRange = 20;
	public float plantXOffset;
	public Vector2 plantYOffset;
	public Vector2 plantZOffset;
	public int beginPlantCount = 2;
	public float beginPlantXOffset;
	public float beginPlantRange = 20;
	public Vector2 beginPlantYOffset = new Vector2(12f, 12f);
	public Vector2 beginPlantZOffset;


	public uint maxFishCount = 10;
	public float fishInterval = 2f;
	public Vector2 fishScale = new Vector2(0.1f, 0.3f);
	public float fishRange = 20;
	public float fishXOffset;
	public Vector2 fishYOffset = new Vector2(-10f, -5f);
	public Vector2 fishZOffset;
	public int beginFishCount = 2;
	public float beginFishRange = 20;
	public Vector2 beginFishYOffset = new Vector2(12f, 12f);
	public Vector2 beginFishZOffset;


	GameObjectPool flowPool; 
	GameObjectPool plantPool;
	GameObjectPool fishPool; 

	public enum SceneType {
		Water
	};

	public SceneType sceneType = SceneType.Water;
	 
	void Start () {
		flowPool = new GameObjectPool(prefabFlow.gameObject, maxFlowCount,
		                              (gameObject) => {}, false);
		plantPool = new GameObjectPool(prefabPlant.gameObject, maxPlantCount,
		                               (gameObject) => {}, false);
		fishPool = new GameObjectPool(prefabFish.gameObject, maxFishCount,
		                              (gameObject) => {}, false);

		GenerateFlowBeforGame ();
		GenerateFishBeforeGame ();
		GeneratePlantBeforeGame ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public override void StartGenerate() {
		StartCoroutine (Generate());
	}

	IEnumerator Generate() {
		yield return new WaitForSeconds (0.5f);
		StartCoroutine(GenerateFlow());
		StartCoroutine(GenerateFish());
		StartCoroutine(GeneratePlant());

	}

	IEnumerator GenerateFlow() {
		if (flowPool.numActive < maxFlowCount) {
			Vector3 position;
			// 1. generate a random coordinate
			while (true) {
				float xOffset = Random.Range (-flowRange, flowRange) + flowXOffset;
				float yOffset = Random.Range (flowYOffset.x, flowYOffset.y);
				float zOffset = Random.Range (flowZOffset.x, flowZOffset.y);
				Vector3 randomPosition = new Vector3 (xOffset, yOffset, zOffset);
				print ("GenerateFlow " + randomPosition);
				Vector3 newPosition = randomPosition + Quaternion.Euler(- 270, 0, 0) * transform.worldToLocalMatrix.MultiplyPoint (generatorReference.position);
				bool result = CheckFlowCollision (transform.localToWorldMatrix * newPosition);
				if (!result) {
					position = newPosition;
					break;
				}
			}
			Transform newFlow = flowPool.Spawn (Vector3.zero, Quaternion.identity).transform;
			newFlow.SetParent (flows);
			newFlow.localScale = Vector3.one;
			newFlow.localRotation = Quaternion.Euler (0, 0, 0);
			newFlow.localPosition = position; 
			newFlow.GetComponent<Flow> ().pool = flowPool;

		}
		yield return new WaitForSeconds (flowInterval);
		
		yield return StartCoroutine (GenerateFlow());
	}

	void GenerateFlowBeforGame() {
		for (int i = 0; i < beginFlowCount; i++) {
			Vector3 position;
			// 1. generate a random coordinate
			while (true) {
				float xOffset = Random.Range (-beginFlowRange, beginFlowRange) + flowXOffset;
				float yOffset = Random.Range (beginFlowYOffset.x, beginFlowYOffset.y);
				float zOffset = Random.Range (beginFlowZOffset.x, beginFlowZOffset.y);
				Vector3 randomPosition = new Vector3 (xOffset, yOffset, zOffset);
				Vector3 newPosition = randomPosition + Quaternion.Euler(- 270, 0, 0) * transform.worldToLocalMatrix.MultiplyPoint (generatorReference.position);
				bool result = CheckFlowCollision (transform.localToWorldMatrix * newPosition);
				if (!result) {
					position = newPosition;
					break;
				}
			}
			Transform newFlow = flowPool.Spawn (Vector3.zero, Quaternion.identity).transform;
			newFlow.SetParent (flows);
			newFlow.localScale = Vector3.one;
			newFlow.localRotation = Quaternion.Euler (0, 0, 0);
			newFlow.localPosition = position; 
			newFlow.GetComponent<Flow> ().pool = flowPool;
		}
	}

	IEnumerator GeneratePlant() {
		if (plantPool.numActive < maxPlantCount) {
			float xOffset = Random.Range (-plantRange, plantRange) + plantXOffset;
			float yOffset = Random.Range (plantYOffset.x, plantYOffset.y);
			float zOffset = Random.Range (plantZOffset.x, plantZOffset.y);
			Vector3 randomPosition = new Vector3 (xOffset, yOffset, zOffset);
			Vector3 newPosition = randomPosition + Quaternion.Euler(- 270, 0, 0) * transform.worldToLocalMatrix.MultiplyPoint (generatorReference.position);
		
			Transform newPlant = plantPool.Spawn (Vector3.zero, Quaternion.identity).transform;
			newPlant.SetParent (plants);
			newPlant.localScale = Vector3.one;
			newPlant.localRotation = Quaternion.Euler (0, 0, 0);
			newPlant.localPosition = newPosition;
			newPlant.GetComponent<Plant> ().pool = plantPool;
//			print ("new position = " + newPosition);
//			print ("ref pos = " + Quaternion.Euler(- 270, 0, 0) * transform.worldToLocalMatrix.MultiplyPoint (generatorReference.position));
		}
		yield return new WaitForSeconds (plantInterval);
		
		yield return StartCoroutine (GeneratePlant());
	}

	void GeneratePlantBeforeGame() {
		for (int i = 0; i < beginPlantCount; i++){
			float xOffset = Random.Range (-beginPlantRange, beginPlantRange) + beginPlantXOffset;
			float yOffset = Random.Range (beginPlantYOffset.x, beginPlantYOffset.y);
			float zOffset = Random.Range (beginPlantZOffset.x, beginPlantZOffset.y);
			Vector3 randomPosition = new Vector3 (xOffset, yOffset, zOffset);
			Vector3 newPosition = randomPosition + Quaternion.Euler(- 270, 0, 0) * transform.worldToLocalMatrix.MultiplyPoint (generatorReference.position);

			Transform newPlant = plantPool.Spawn (Vector3.zero, Quaternion.identity).transform;
			newPlant.SetParent (plants);
			newPlant.localScale = Vector3.one;
			newPlant.localRotation = Quaternion.Euler (0, 0, 0);
			newPlant.localPosition = newPosition;
			newPlant.GetComponent<Plant> ().pool = plantPool;
		}
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
		if (fishPool.numActive < maxFishCount) {
			Vector3 position;
			float scale;
			// 1. generate a random coordinate
			while (true) {
				float xOffset = Random.Range (-fishRange, fishRange) + fishXOffset;
				float yOffset = Random.Range (fishYOffset.x, fishYOffset.y);
				float zOffset = Random.Range (fishZOffset.x, fishZOffset.y);

				scale = Random.Range (fishScale.x, fishScale.y);
		
				Vector3 randomPosition = new Vector3 (xOffset, yOffset, zOffset);
				Vector3 newPosition = randomPosition + Quaternion.Euler(- 270, 0, 0) * transform.worldToLocalMatrix.MultiplyPoint (generatorReference.position);

				bool result = CheckFlowCollision (transform.localToWorldMatrix * newPosition);
				if (!result) {
					position = newPosition;
					break;
				}
			}
			Transform newFish = fishPool.Spawn (Vector3.zero, Quaternion.identity).transform;
			newFish.SetParent (fish);
			newFish.localScale = Vector3.one * scale;
			newFish.localRotation = Quaternion.Euler (0, 0, 0);
			newFish.localPosition = position; 
			newFish.GetComponent<Fish> ().pool = fishPool;
		}
		yield return new WaitForSeconds (fishInterval);
		
		yield return StartCoroutine (GenerateFish());
	}

	void GenerateFishBeforeGame() {
		for (int i = 0; i < beginFishCount; i++) {
			Vector3 position;
			float scale;
			// 1. generate a random coordinate
			while (true) {
				float xOffset = Random.Range (-beginFishRange, beginFishRange) + fishXOffset;
				float yOffset = Random.Range (beginFishYOffset.x, beginFishYOffset.y);
				float zOffset = Random.Range (beginFishZOffset.x, beginFishZOffset.y);
				
				scale = Random.Range (fishScale.x, fishScale.y);
				
				Vector3 randomPosition = new Vector3 (xOffset, yOffset, zOffset);
	            Vector3 newPosition = randomPosition + Quaternion.Euler(- 270, 0, 0) * transform.worldToLocalMatrix.MultiplyPoint (generatorReference.position);

				
				bool result = CheckFlowCollision (transform.localToWorldMatrix * newPosition);
				if (!result) {
					position = newPosition;
					break;
				}
			}

			Transform newFish = fishPool.Spawn (Vector3.zero, Quaternion.identity).transform;
			newFish.SetParent (fish);
			newFish.localScale = Vector3.one * scale;
			newFish.localRotation = Quaternion.Euler (0, 0, 0);
			newFish.localPosition = position; 
			newFish.GetComponent<Fish> ().pool = fishPool;
		}
	}
}
