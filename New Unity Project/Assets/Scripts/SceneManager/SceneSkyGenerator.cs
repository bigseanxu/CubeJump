using UnityEngine;
using System.Collections;
using System.Timers;
using System.Collections.Generic;

public class SceneSkyGenerator : BaseGenerator {
	public Transform pillarGenerator;
	public Transform[] prefabCloud;
	public Transform Clouds;
	public Transform prefabPlane;
	public Transform Planes;
	public Transform prefabBalloon;
	public Transform Balloons;
	public Transform generatorReference;

	public float cloudInterval = 2f;
	public float planeInterval = 2f;
	public float ballonInterval = 2f;

	public Vector2 planeScale = new Vector2(10f, 20f);
	public Vector2 ballonScale = new Vector2(8f, 12f);

	public Vector2 planeY = new Vector2(10f, 20f);
	public Vector2 ballonY = new Vector2(8f, 12f);
	public Vector2 ballonX = new Vector2(-10f, 10f);
	public Vector2 ballonZ = new Vector2(-10f, 10f);

	public Vector2 cloudY = new Vector2(-5f, 5f);


	public enum SceneType {
		Sky
	};

	public SceneType sceneType = SceneType.Sky;
	 
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
		StartCoroutine(GeneratePlane());
		StartCoroutine(GenerateBallon());

		yield return null;
	}

	IEnumerator GenerateCloud() {
		float xOffset = 20;
		float zOffset = 20;
		float scale = 10f;

		int i = Random.Range (0, 5);
		Vector3 position = Vector3.zero;
		
		Vector3 randomPosition = new Vector3(Random.Range(-xOffset, xOffset), Random.Range(cloudY.x, cloudY.y), Random.Range(-zOffset - 40, zOffset - 40));
		Vector3 newPosition = randomPosition + generatorReference.position;
		
		Transform newFish = (Transform)GameObject.Instantiate (prefabCloud[i]);
		newFish.SetParent (Clouds);
		newFish.localScale = Vector3.one * scale;
		newFish.localRotation = Quaternion.Euler (0, 0, 0);
		newFish.position = newPosition; 
		
		yield return new WaitForSeconds (cloudInterval);
		yield return StartCoroutine (GenerateCloud());
	}

	IEnumerator GenerateBallon() {
		float xOffset = Random.Range (ballonX.x, ballonX.y);
		float zOffset = Random.Range (ballonZ.x, ballonZ.y);
		float scale = Random.Range (ballonScale.x, ballonScale.y);
		Transform newPlant = (Transform)GameObject.Instantiate (prefabBalloon);
		newPlant.SetParent (Balloons);
		newPlant.localScale = Vector3.one*scale;
		newPlant.localRotation = Quaternion.Euler (0, 0, 0);
		newPlant.position = generatorReference.position + new Vector3 (xOffset, Random.Range(ballonY.x, ballonY.y), zOffset);
		
		yield return new WaitForSeconds (ballonInterval);
		yield return StartCoroutine (GenerateBallon());
	}

	IEnumerator GeneratePlane() {
		float xOffset = 20;
		float zOffset = 20;
		float scale = Random.Range (planeScale.x, planeScale.y);
		Vector3 position = Vector3.zero;
		
		Vector3 randomPosition = new Vector3(Random.Range(-xOffset, xOffset), Random.Range(planeY.x, planeY.y), Random.Range(-zOffset - 40, zOffset - 40));
		Vector3 newPosition = randomPosition + generatorReference.position;
		
		Transform newFish = (Transform)GameObject.Instantiate (prefabPlane);
		newFish.SetParent (Planes);
		newFish.localScale = Vector3.one * scale;
		newFish.localRotation = Quaternion.Euler (270, 0, 0);
		newFish.position = newPosition; 
		
		yield return new WaitForSeconds (planeInterval);
		yield return StartCoroutine (GeneratePlane());
	}

}
