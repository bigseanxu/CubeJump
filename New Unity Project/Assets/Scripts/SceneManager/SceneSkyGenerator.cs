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
	public float cloudRange = 20;
	public float cloudXOffset;
	public Vector2 cloudYOffset = new Vector2(-5f, 5f);
	public Vector2 cloudZOffset;

	public float planeInterval = 2f;
	public Vector2 planeScale = new Vector2(10f, 20f);
	public float planeRange = 20;
	public float planeXOffset;
	public Vector2 planeYOffset = new Vector2(10f, 20f);
	public Vector2 planeZOffset;

	public float ballonInterval = 2f;
	public Vector2 ballonScale = new Vector2(8f, 12f);
	public float ballonRange = 20;
	public float ballonXOffset;
	public Vector2 ballonYOffset = new Vector2(10f, 20f);
	public Vector2 ballonZOffset;

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
		float scale = 10f;

		int i = Random.Range (0, 5);
		Vector3 position = Vector3.zero;
		
		float xOffset = Random.Range (-cloudRange, cloudRange) + cloudXOffset;
		float yOffset = Random.Range (cloudYOffset.x, cloudYOffset.y);
		float zOffset = Random.Range (cloudZOffset.x, cloudZOffset.y);
		Vector3 randomPosition = new Vector3(xOffset, yOffset, zOffset);
		Vector3 newPosition = randomPosition + transform.worldToLocalMatrix.MultiplyPoint(generatorReference.position);

		Transform newFish = (Transform)GameObject.Instantiate (prefabCloud[i]);
		newFish.SetParent (Clouds);
		newFish.localScale = Vector3.one * scale;
		newFish.localRotation = Quaternion.Euler (0, 0, 0);
		newFish.localPosition = newPosition; 
		
		yield return new WaitForSeconds (cloudInterval);
		yield return StartCoroutine (GenerateCloud());
	}

	IEnumerator GenerateBallon() {
		float xOffset = Random.Range (-ballonRange, ballonRange) + ballonXOffset;
		float yOffset = Random.Range (ballonYOffset.x, ballonYOffset.y);
		float zOffset = Random.Range (cloudZOffset.x, ballonZOffset.y);
		Vector3 randomPosition = new Vector3(xOffset, yOffset, zOffset);
		Vector3 newPosition = randomPosition + transform.worldToLocalMatrix.MultiplyPoint(generatorReference.position);


		float scale = Random.Range (ballonScale.x, ballonScale.y);
		Transform newPlant = (Transform)GameObject.Instantiate (prefabBalloon);
		newPlant.SetParent (Balloons);
		newPlant.localScale = Vector3.one*scale;
		newPlant.localRotation = Quaternion.Euler (0, 0, 0);
		newPlant.localPosition = newPosition;
		
		yield return new WaitForSeconds (ballonInterval);
		yield return StartCoroutine (GenerateBallon());
	}

	IEnumerator GeneratePlane() {
		float xOffset = Random.Range (-planeRange, planeRange) + planeXOffset;
		float yOffset = Random.Range (planeYOffset.x, planeYOffset.y);
		float zOffset = Random.Range (planeZOffset.x, planeZOffset.y);
		float scale = Random.Range (planeScale.x, planeScale.y);
		Vector3 position = Vector3.zero;
		
		Vector3 randomPosition = new Vector3(xOffset, yOffset, zOffset);
		Vector3 newPosition = randomPosition + transform.worldToLocalMatrix.MultiplyPoint(generatorReference.position);
		
		Transform newFish = (Transform)GameObject.Instantiate (prefabPlane);
		newFish.SetParent (Planes);
		newFish.localScale = Vector3.one * scale;
		newFish.localRotation = Quaternion.Euler (270, 0, 0);
		newFish.localPosition = newPosition; 
		
		yield return new WaitForSeconds (planeInterval);
		yield return StartCoroutine (GeneratePlane());
	}

}
