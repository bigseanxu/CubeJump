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



	public float butterflyInterval = 2f;
	public Vector2 butterflyScale = new Vector2 (8f, 12f);
	public float butterflyRange = 20;
	public Vector2 butterflyYOffset = new Vector2(12f, 12f);
	public Vector2 butterflyZOffset;

	public float cloudInterval = 2f;
	public float cloudRange = 20;
	public Vector2 cloudYOffset = new Vector2(-5f, 5f);
	public Vector2 cloudZOffset;

	public float dragonflyInterval = 2f;
	public Vector2 dragonflyScale = new Vector2(8f, 12f);
	public float dragonflyRange = 20;
	public Vector2 dragonflyYOffset = new Vector2(-5f, 5f);
	public Vector2 dragonflyZOffset;

	public enum SceneType {
		Forest
	};

	public SceneType sceneType = SceneType.Forest;
	 
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void StartGenerate() {
		StartCoroutine (Generate());
	}

	IEnumerator Generate() {
		StartCoroutine(GenerateCloud());
		StartCoroutine(GenerateFly());
		StartCoroutine(GenerateButterfly());
		yield return null;
	}

	IEnumerator GenerateCloud() {
		float xOffset = 20;
		float zOffset = 20;
		float scale = 10f;
		int i = Random.Range (0, 5);
		Vector3 position = Vector3.zero;
		
		Vector3 randomPosition = new Vector3(Random.Range(-cloudRange, cloudRange), Random.Range(cloudYOffset.x, cloudYOffset.y), Random.Range(cloudZOffset.x, cloudZOffset.y));
		Vector3 newPosition = randomPosition + transform.worldToLocalMatrix.MultiplyPoint(generatorReference.position);
		
		Transform newFish = (Transform)GameObject.Instantiate (prefabCloud[i]);
		newFish.SetParent (Clouds);
		newFish.localScale = Vector3.one * scale;
		newFish.localRotation = Quaternion.Euler (0, 0, 0);
		newFish.position = newPosition; 

		print ("aa");
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
		
		Transform newFish = (Transform)GameObject.Instantiate (prefabFly);
		newFish.SetParent (Flys);
		newFish.localScale = Vector3.one * scale;
		newFish.localRotation = Quaternion.Euler (270, 0, 0);
		newFish.position = newPosition; 
		
		yield return new WaitForSeconds (dragonflyInterval);
		yield return StartCoroutine (GenerateFly());
	}

	IEnumerator GenerateButterfly() {
		float scale = Random.Range (butterflyScale.x, butterflyScale.y);
		float yOffset = Random.Range (butterflyYOffset.x, butterflyYOffset.y);
		Vector3 position = Vector3.zero;
		
		Vector3 randomPosition = new Vector3(Random.Range(-butterflyRange, butterflyRange), yOffset, Random.Range(butterflyZOffset.x, butterflyZOffset.y));
		Vector3 newPosition = randomPosition + transform.worldToLocalMatrix.MultiplyPoint(generatorReference.position);
		
		Transform newFish = (Transform)GameObject.Instantiate (prefabButterfly);
		newFish.SetParent (Butterflies);
		newFish.localScale = Vector3.one * scale;
		newFish.localRotation = Quaternion.Euler (285, 90, 270);
		newFish.localPosition = newPosition; 

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
