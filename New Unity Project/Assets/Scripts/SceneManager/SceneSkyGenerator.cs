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
		if (Game.state == Game.State.Gaming) { 
			GenerateCloud();
			//GeneratePlant();
			int a=Random.Range(0,10);

			if(a>4)
				GeneratePlane();
			if(a>5)
				GenerateBallon();

		}
		
		yield return new WaitForSeconds (1);
		yield return StartCoroutine (Generate());
	}
	public void GenerateCloud() {
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
	}

	public void GenerateBallon() {
		float xOffset = Random.Range (-20, -10);
		float zOffset = Random.Range (10, 20);
		float scale = Random.Range (10, 20);
		Transform newPlant = (Transform)GameObject.Instantiate (prefabBalloon);
		newPlant.SetParent (Balloons);
		newPlant.localScale = Vector3.one*scale;
		newPlant.localRotation = Quaternion.Euler (0, 0, 0);
		newPlant.position = generatorReference.position + new Vector3 (xOffset, -20, zOffset);
	}

	public void GeneratePlane() {
		float xOffset = 20;
		float zOffset = 20;
		float scale = Random.Range (8f, 12f);
		Vector3 position = Vector3.zero;
		
		Vector3 randomPosition = new Vector3(Random.Range(-xOffset, xOffset), Random.Range(10f,15f), Random.Range(-zOffset - 40, zOffset - 40));
		Vector3 newPosition = randomPosition + generatorReference.position;
		
		Transform newFish = (Transform)GameObject.Instantiate (prefabPlane);
		newFish.SetParent (Planes);
		newFish.localScale = Vector3.one * scale;
		newFish.localRotation = Quaternion.Euler (270, 0, 0);
		newFish.position = newPosition; 
	}

}
