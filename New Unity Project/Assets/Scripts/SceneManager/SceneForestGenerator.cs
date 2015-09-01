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

	public float cloudInterval = 2f;
	public float flyInterval = 2f;
	public float butterflyInterval = 2f;

	public Vector2 butterflyScale = new Vector2 (8f, 12f);
	public Vector2 butterflyY = new Vector2(12f, 12f);

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
		
		Vector3 randomPosition = new Vector3(Random.Range(-xOffset, xOffset), Random.Range(1f, 10f), Random.Range(-zOffset - 40, zOffset - 40));
		Vector3 newPosition = randomPosition + generatorReference.position;
		
		Transform newFish = (Transform)GameObject.Instantiate (prefabCloud[i]);
		newFish.SetParent (Clouds);
		newFish.localScale = Vector3.one * scale;
		newFish.localRotation = Quaternion.Euler (0, 0, 0);
		newFish.position = newPosition; 

		print ("aa");
		yield return new WaitForSeconds (cloudInterval);
		yield return StartCoroutine (GenerateCloud());
	}


	public void GeneratePlant() {
		float xOffset = Random.Range (-20, 0);
		float zOffset = Random.Range (0, 20);
		
		Transform newPlant = (Transform)GameObject.Instantiate (prefabPlant);
		newPlant.SetParent (Plants);
		newPlant.localScale = Vector3.one;
		newPlant.localRotation = Quaternion.Euler (270, 0, 0);
		newPlant.position = generatorReference.position + new Vector3 (xOffset, 0, zOffset);

	}

	IEnumerator GenerateFly() {
		float xOffset = 20;
		float zOffset = 20;
		float scale = 10;
		Vector3 position = Vector3.zero;
		
		Vector3 randomPosition = new Vector3(Random.Range(-xOffset, xOffset), Random.Range(10f, 15f), Random.Range(-zOffset - 40, zOffset - 40));
		Vector3 newPosition = randomPosition + generatorReference.position;
		
		Transform newFish = (Transform)GameObject.Instantiate (prefabFly);
		newFish.SetParent (Flys);
		newFish.localScale = Vector3.one * scale;
		newFish.localRotation = Quaternion.Euler (270, 0, 0);
		newFish.position = newPosition; 
		
		yield return new WaitForSeconds (flyInterval);
		yield return StartCoroutine (GenerateFly());
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
