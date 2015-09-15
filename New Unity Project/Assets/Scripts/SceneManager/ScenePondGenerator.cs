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

	public uint maxPlantCount = 10;
	public float plantInterval = 2f;
	public float plantRange = 20;
	public float plantXOffset;
	public Vector2 plantYOffset;
	public Vector2 plantZOffset;
	public int beginPlantCount = 2;
	public float beginPlantRange = 20;
	public Vector2 beginPlantYOffset = new Vector2(12f, 12f);
	public Vector2 beginPlantZOffset;

	public uint maxFrogCount = 10;
	public float frogInterval = 2f;
	public Vector2 frogScale = new Vector2(8f, 12f);
	public float frogRange = 20;
	public float frogXOffset;
	public Vector2 frogYOffset;
	public Vector2 frogZOffset;
	public int beginFrogCount = 2;
	public float beginFrogRange = 20;
	public Vector2 beginFrogYOffset = new Vector2(12f, 12f);
	public Vector2 beginFrogZOffset;

	public uint maxLotusCount = 10;
	public float lotusInterval = 2f;
	public Vector2 lotusScale = new Vector2(8f, 12f);
	public float lotusRange = 20;
	public float lotusXOffset;
	public Vector2 lotusYOffset;
	public Vector2 lotusZOffset;
	public int beginLotusCount = 2;
	public float beginLotusRange = 20;
	public Vector2 beginLotusYOffset = new Vector2(12f, 12f);
	public Vector2 beginLotusZOffset;

	public uint maxCoCount = 10;
	public float coInterval = 2f;
	public Vector2 coScale = new Vector2(8f, 12f);
	public float coRange = 20;
	public float coXOffset;
	public Vector2 coYOffset;
	public Vector2 coZOffset;
	public int beginCoCount = 2;
	public float beginCoRange = 20;
	public Vector2 beginCoYOffset = new Vector2(12f, 12f);
	public Vector2 beginCoZOffset;

	GameObjectPool flowPool; 
	GameObjectPool plantPool;
	GameObjectPool frogPool; 
	GameObjectPool lotusPool; 
	GameObjectPool coPool; 

	public enum SceneType {
		Pond
	};

	public SceneType sceneType = SceneType.Pond;
	 
	void Start () {
		flowPool = new GameObjectPool(prefabFlow.gameObject, maxFlowCount,
		                                (gameObject) => {}, false);
		plantPool = new GameObjectPool(prefabPlant.gameObject, maxPlantCount,
		                                (gameObject) => {}, false);
		frogPool = new GameObjectPool(prefabFrog.gameObject, maxFrogCount,
		                                (gameObject) => {}, false);
		lotusPool = new GameObjectPool(prefabLotus.gameObject, maxLotusCount,
		                                (gameObject) => {}, false);
		coPool = new GameObjectPool(prefabCrocodile.gameObject, maxCoCount,
		                               (gameObject) => {}, false);
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
		StartCoroutine(GeneratePlant());
		StartCoroutine(GenerateFrog());
        StartCoroutine(GenerateLotus());
        StartCoroutine(GenerateCo());
	

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
				Vector3 newPosition = randomPosition + transform.worldToLocalMatrix.MultiplyPoint (generatorReference.position);
				bool result = CheckFlowCollision (newPosition);
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

	IEnumerator GeneratePlant() {
		if (plantPool.numActive < maxPlantCount) {
			float xOffset = Random.Range (-plantRange, plantRange) + plantXOffset;
			float yOffset = Random.Range (plantYOffset.x, plantYOffset.y);
			float zOffset = Random.Range (plantZOffset.x, plantZOffset.y);
			Vector3 randomPosition = new Vector3 (xOffset, yOffset, zOffset);
			Vector3 newPosition = randomPosition + transform.worldToLocalMatrix.MultiplyPoint (generatorReference.position);

			Transform newPlant = plantPool.Spawn (Vector3.zero, Quaternion.identity).transform;
			newPlant.SetParent (plants);
			newPlant.localScale = Vector3.one;
			newPlant.localRotation = Quaternion.Euler (270, 0, 0);
			newPlant.localPosition = newPosition;
			newPlant.GetComponent<Plant> ().pool = plantPool;
		}
			yield return new WaitForSeconds (plantInterval);
		
		yield return StartCoroutine (GeneratePlant());
	}
	

	IEnumerator GenerateFrog() {
		if (frogPool.numActive < maxFrogCount) {
			float scale = Random.Range (frogScale.x, frogScale.y);
			Vector3 position;
			// 1. generate a random coordinate
			while (true) {
				float xOffset = Random.Range (-frogRange, frogRange) + frogXOffset;
				float yOffset = Random.Range (frogYOffset.x, frogYOffset.y);
				float zOffset = Random.Range (frogZOffset.x, frogZOffset.y);
				Vector3 randomPosition = new Vector3 (xOffset, yOffset, zOffset);
				Vector3 newPosition = randomPosition + transform.worldToLocalMatrix.MultiplyPoint (generatorReference.position);
				bool result = CheckFlowCollision (newPosition);
				if (!result) {
					position = newPosition;
					break;
				}
			}
			Transform newFrog = frogPool.Spawn (Vector3.zero, Quaternion.identity).transform;
			newFrog.SetParent (frog);
			newFrog.localScale = Vector3.one * scale;
			newFrog.localRotation = Quaternion.Euler (270, 0, 0);
			newFrog.localPosition = position; 
			newFrog.GetComponent<Frog> ().pool = frogPool;
		}
		yield return new WaitForSeconds (frogInterval);
		
		yield return StartCoroutine (GenerateFrog());
	}

	IEnumerator GenerateLotus() {
		if (lotusPool.numActive < maxLotusCount) {
			Vector3 position = Vector3.zero;
			float scale = Random.Range (lotusScale.x, lotusScale.y);
			// 1. generate a random coordinate
			while (true) {
				float xOffset = Random.Range (-lotusRange, lotusRange) + lotusXOffset;
				float yOffset = Random.Range (lotusYOffset.x, lotusYOffset.y);
				float zOffset = Random.Range (lotusZOffset.x, lotusZOffset.y);
				Vector3 randomPosition = new Vector3 (xOffset, yOffset, zOffset);
				Vector3 newPosition = randomPosition + transform.worldToLocalMatrix.MultiplyPoint (generatorReference.position);
				bool result = CheckFlowCollision (newPosition);
				if (!result) {
					position = newPosition;
					break;
				}
			}
			Transform newLotus = lotusPool.Spawn (Vector3.zero, Quaternion.identity).transform;
			newLotus.SetParent (lotus);
			newLotus.localScale = Vector3.one * scale;
			newLotus.localRotation = Quaternion.Euler (270, 0, 0);
			newLotus.localPosition = position; 
			newLotus.GetComponent<Lotus> ().pool = lotusPool;
		}
			yield return new WaitForSeconds (lotusInterval);
		
		yield return StartCoroutine (GenerateLotus());
	}

	IEnumerator GenerateCo() {
		if (coPool.numActive < maxCoCount) {
			Vector3 position;
			float scale = Random.Range (coScale.x, coScale.y);
			// 1. generate a random coordinate
			while (true) {
				float xOffset = Random.Range (-coRange, coRange) + coXOffset;
				float yOffset = Random.Range (coYOffset.x, coYOffset.y);
				float zOffset = Random.Range (coZOffset.x, coZOffset.y);
				Vector3 randomPosition = new Vector3 (xOffset, yOffset, zOffset);
				Vector3 newPosition = randomPosition + transform.worldToLocalMatrix.MultiplyPoint (generatorReference.position);
				bool result = CheckFlowCollision (newPosition);
				if (!result) {
					position = newPosition;
					break;
				}
			}
			Transform newLotus = coPool.Spawn (Vector3.zero, Quaternion.identity).transform;
			newLotus.SetParent (Crocodiles);
			newLotus.localScale = Vector3.one * scale;
			newLotus.localRotation = Quaternion.Euler (270, 0, 0);
			newLotus.localPosition = position; 
			newLotus.GetComponent<Crocodile> ().pool = coPool;
		}
		yield return new WaitForSeconds (coInterval);
		
		yield return StartCoroutine (GenerateCo());
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
