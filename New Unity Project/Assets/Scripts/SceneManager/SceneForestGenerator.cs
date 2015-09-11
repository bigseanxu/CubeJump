using UnityEngine;
using System.Collections;
using System.Timers;
using System.Collections.Generic;

public class SceneForestGenerator : BaseGenerator {
	public Transform pillarGenerator;
	public Transform[] prefabCloud;
	public Transform Clouds;
	public Transform prefabPlant;
	public Transform Plants;
	public Transform prefabFly;
	public Transform Flys;
	public Transform prefabButterfly;
	public Transform Butterflies;
	public Transform generatorReference;

	public uint maxButterflyCount = 10;
	public float butterflyInterval = 2f;
	public Vector2 butterflyScale = new Vector2 (8f, 12f);
	public float butterflyRange = 20;
	public Vector2 butterflyYOffset = new Vector2(12f, 12f);
	public Vector2 butterflyZOffset;

	public uint maxCloudCount = 10;
	public float cloudInterval = 2f;
	public float cloudRange = 20;
	public Vector2 cloudYOffset = new Vector2(-5f, 5f);
	public Vector2 cloudZOffset;

	public uint maxDragonCount = 10;
	public float dragonflyInterval = 2f;
	public Vector2 dragonflyScale = new Vector2(8f, 12f);
	public float dragonflyRange = 20;
	public Vector2 dragonflyYOffset = new Vector2(-5f, 5f);
	public Vector2 dragonflyZOffset;


	GameObjectPool butterflyPool; 
	GameObjectPool [] cloudPools = new GameObjectPool[6]; 
	GameObjectPool dragonflyPool; 


	public enum SceneType {
		Forest
	};

	public SceneType sceneType = SceneType.Forest;
	 
	void Start () {
		butterflyPool = new GameObjectPool(prefabButterfly.gameObject, maxButterflyCount,
		                                    (gameObject) => {}, false);

		for (int i = 0; i < 6; i++) {
			cloudPools[i] = new GameObjectPool(prefabCloud[i].gameObject, maxCloudCount,
			                                   (gameObject) => {}, false);	
		}

		dragonflyPool = new GameObjectPool(prefabFly.gameObject, maxDragonCount,
		                                   (gameObject) => {}, false);	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void StartGenerate() {
		StartCoroutine (Generate());
	}

	IEnumerator Generate() {
		yield return new WaitForSeconds(0.5f);
		StartCoroutine(GenerateCloud());
		StartCoroutine(GenerateFly());
		StartCoroutine(GenerateButterfly());

	}

	IEnumerator GenerateCloud() {
		float xOffset = 20;
		float zOffset = 20;
		float scale = 10f;
		int i = Random.Range (0, prefabCloud.Length);
		Vector3 position = Vector3.zero;
		
		Vector3 randomPosition = new Vector3(Random.Range(-cloudRange, cloudRange), Random.Range(cloudYOffset.x, cloudYOffset.y), Random.Range(cloudZOffset.x, cloudZOffset.y));
		Vector3 newPosition = randomPosition + transform.worldToLocalMatrix.MultiplyPoint(generatorReference.position);
		
		Transform newFish = cloudPools[i].Spawn (Vector3.zero, Quaternion.identity).transform;
		newFish.SetParent (Clouds);
		newFish.localScale = Vector3.one * scale;
		newFish.localRotation = Quaternion.Euler (0, 0, 0);
		newFish.position = newPosition; 

		yield return new WaitForSeconds (cloudInterval);
		yield return StartCoroutine (GenerateCloud());
	}

	IEnumerator GenerateFly() {
		float xOffset = 20;
		float zOffset = 20;
		float scale = Random.Range (dragonflyScale.x, dragonflyScale.y);
		Vector3 position = Vector3.zero;
		
		Vector3 randomPosition = new Vector3(Random.Range(-dragonflyRange, dragonflyRange), Random.Range(dragonflyYOffset.x, dragonflyYOffset.y), Random.Range(dragonflyZOffset.x, dragonflyZOffset.y));
		Vector3 newPosition = randomPosition + transform.worldToLocalMatrix.MultiplyPoint(generatorReference.position);
		
		Transform newFish = dragonflyPool.Spawn (Vector3.zero, Quaternion.identity).transform;
		newFish.SetParent (Flys);
		newFish.localScale = Vector3.one * scale;
		newFish.localRotation = Quaternion.Euler (270, 0, 0);
		newFish.position = newPosition; 
//		
		yield return new WaitForSeconds (dragonflyInterval);
		yield return StartCoroutine (GenerateFly());
	}

	IEnumerator GenerateButterfly() {
		float scale = Random.Range (butterflyScale.x, butterflyScale.y);
		float yOffset = Random.Range (butterflyYOffset.x, butterflyYOffset.y);
		Vector3 position = Vector3.zero;
		
		Vector3 randomPosition = new Vector3(Random.Range(-butterflyRange, butterflyRange), yOffset, Random.Range(butterflyZOffset.x, butterflyZOffset.y));
		Vector3 newPosition = randomPosition + transform.worldToLocalMatrix.MultiplyPoint(generatorReference.position);
		
		Transform newButterfly = butterflyPool.Spawn (Vector3.zero, Quaternion.identity).transform;
		newButterfly.SetParent (Butterflies);
		newButterfly.localScale = Vector3.one * scale;
		newButterfly.localRotation = Quaternion.Euler (285, 90, 270);
		newButterfly.localPosition = newPosition; 

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
