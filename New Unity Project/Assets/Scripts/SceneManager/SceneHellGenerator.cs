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
	
	public float batInterval = 2f;
	public float ghostInterval = 2f;
	public float spiderInterval = 2f;

	public Vector2 starScale = new Vector2(0.8f, 1.2f);
	public Vector2 batScale = new Vector2(8f, 12f);
	public Vector2 ghostScale = new Vector2(0.1f, 0.3f);
	public Vector2 spiderScale = new Vector2(5f, 15f);

	public Vector2 batY = new Vector2(8f, 12f);
	public Vector2 ghostY = new Vector2(0, 5f);
	public Vector2 spiderY = new Vector2(5f, 15f);
	public Vector2 spiderX = new Vector2(-10, 10f);
	public Vector2 spiderZ = new Vector2(-10f, 10f);


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

		StartCoroutine(GenerateSpider ());
		StartCoroutine(GenerateBat ());
		StartCoroutine(GenerateGhost ());
//		StartCoroutine(GenerateStars());
		yield return null;
	}
	

	IEnumerator GenerateBat() {
		float xOffset = 20;
		float zOffset = 20;
		float scale = Random.Range (batScale.x, batScale.y);
		Vector3 position = Vector3.zero;
		
		Vector3 randomPosition = new Vector3(Random.Range(-xOffset, xOffset), Random.Range(batY.x,batY.y), Random.Range(-zOffset - 40, zOffset - 40));
		Vector3 newPosition = randomPosition + generatorReference.position;
		
		Transform newFish = (Transform)GameObject.Instantiate (prefabBat);
		newFish.SetParent (Bats);
		newFish.localScale = Vector3.one * scale;
		newFish.localRotation = Quaternion.Euler (270, 0, 0);
		newFish.position = newPosition; 

		yield return new WaitForSeconds (batInterval);
		yield return StartCoroutine (GenerateBat());
	}

	IEnumerator GenerateSpider() {
		float xOffset = Random.Range (spiderX.x, spiderX.y);
		float zOffset = Random.Range (spiderZ.x, spiderZ.y);
		float scale = Random.Range (spiderScale.x, spiderScale.y);
		Transform newPlant = (Transform)GameObject.Instantiate (prefabSpider);
		newPlant.SetParent (Spiders);
		newPlant.localScale = Vector3.one*scale;
		newPlant.localRotation = Quaternion.Euler (0, 0, 0);
		newPlant.position = generatorReference.position + new Vector3 (xOffset, Random.Range(spiderY.x, spiderY.y), zOffset);

		yield return new WaitForSeconds (spiderInterval);
		yield return StartCoroutine (GenerateSpider());
	}

	IEnumerator GenerateGhost() {
		float xOffset = 20;
		float zOffset = 20;
		float scale = Random.Range (ghostScale.x, ghostScale.y);
		Vector3 position = Vector3.zero;
		
		Vector3 randomPosition = new Vector3(Random.Range(-xOffset, xOffset), Random.Range(ghostY.x, ghostY.y), Random.Range(-zOffset - 40, zOffset - 40));
		Vector3 newPosition = randomPosition + generatorReference.position;
		
		Transform newFish = (Transform)GameObject.Instantiate (prefabGhost);
		newFish.SetParent (Ghosts);
		newFish.localScale = Vector3.one * scale;
		newFish.localRotation = Quaternion.Euler (270, 0, 0);
		newFish.position = newPosition; 

		yield return new WaitForSeconds (ghostInterval);
		yield return StartCoroutine (GenerateGhost());
	}

}
