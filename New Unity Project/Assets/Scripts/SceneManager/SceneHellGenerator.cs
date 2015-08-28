using UnityEngine;
using System.Collections;
using System.Timers;
using System.Collections.Generic;

public class SceneHellGenerator : BaseGenerator {
	public Transform pillarGenerator;
	public Transform prefabBat;
	public Transform Bats;
	public Transform prefabSpider;
	public Transform Spiders;
	public Transform prefabGhost;
	public Transform Ghosts;
	public Transform generatorReference;
	public enum SceneType {
		Hell
	};

	public SceneType sceneType = SceneType.Hell;
	 
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

				GenerateSpider ();
				int a = Random.Range (0, 10);
				if (a > 1)
					GenerateBat ();
				if (a > 2)
					GenerateGhost ();


		}
		yield return new WaitForSeconds (1);
		yield return StartCoroutine (Generate());
	}
	

	public void GenerateBat() {
		float xOffset = 20;
		float zOffset = 20;
		float scale = Random.Range (8f, 12f);
		Vector3 position = Vector3.zero;
		
		Vector3 randomPosition = new Vector3(Random.Range(-xOffset, xOffset), Random.Range(10f,15f), Random.Range(-zOffset - 40, zOffset - 40));
		Vector3 newPosition = randomPosition + generatorReference.position;
		
		Transform newFish = (Transform)GameObject.Instantiate (prefabBat);
		newFish.SetParent (Bats);
		newFish.localScale = Vector3.one * scale;
		newFish.localRotation = Quaternion.Euler (270, 0, 0);
		newFish.position = newPosition; 
	}

	public void GenerateSpider() {
		float xOffset = Random.Range (-20, -10);
		float zOffset = Random.Range (10, 20);
		float scale = Random.Range (0.1f,0.3f);
		Transform newPlant = (Transform)GameObject.Instantiate (prefabSpider);
		newPlant.SetParent (Spiders);
		newPlant.localScale = Vector3.one*scale;
		newPlant.localRotation = Quaternion.Euler (0, 0, 0);
		newPlant.position = generatorReference.position + new Vector3 (xOffset, -20, zOffset);
	}

	public void GenerateGhost() {
		float xOffset = 20;
		float zOffset = 20;
		float scale = Random.Range (5f, 15f);
		Vector3 position = Vector3.zero;
		
		Vector3 randomPosition = new Vector3(Random.Range(-xOffset, xOffset), Random.Range(-15f,15f), Random.Range(-zOffset - 40, zOffset - 40));
		Vector3 newPosition = randomPosition + generatorReference.position;
		
		Transform newFish = (Transform)GameObject.Instantiate (prefabGhost);
		newFish.SetParent (Ghosts);
		newFish.localScale = Vector3.one * scale;
		newFish.localRotation = Quaternion.Euler (270, 0, 0);
		newFish.position = newPosition; 
	}

}
