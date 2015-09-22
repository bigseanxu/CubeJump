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

	public uint maxSpiderCount = 10;
	public float spiderInterval = 2f;
	public Vector2 spiderScale = new Vector2(5f, 15f);
	public float spiderRange = 20;
	public float spiderXOffset;
	public Vector2 spiderYOffset;
	public Vector2 spiderZOffset;
	public int beginSpiderCount = 2;
	public float beginSpiderXOffset;
	public float beginSpiderRange = 20;
	public Vector2 beginSpiderYOffset = new Vector2(12f, 12f);
	public Vector2 beginSpiderZOffset;


	public uint maxBatCount = 10;
	public float batInterval = 2f;
	public Vector2 batScale = new Vector2(8f, 12f);
	public float batRange = 20;
	public float barXOffset;
	public Vector2 batYOffset;
	public Vector2 batZOffset;
	public int beginBatCount = 2;
	public float beginBatRange = 20;
	public Vector2 beginBatYOffset = new Vector2(12f, 12f);
	public Vector2 beginBatZOffset;

	public uint maxGhostCount = 10;
	public float ghostInterval = 2f;
	public Vector2 ghostScale = new Vector2(0.1f, 0.3f);
	public float ghostRange = 20;
	public float ghostXOffset;
	public Vector2 ghostYOffset;
	public Vector2 ghostZOffset;
	public int beginGhostCount = 2;
	public float beginGhostRange = 20;
	public Vector2 beginGhostYOffset = new Vector2(12f, 12f);
	public Vector2 beginGhostZOffset;

	GameObjectPool spiderPool; 
	GameObjectPool batPool;
	GameObjectPool ghostPool; 

	public enum SceneType {
		Hell
	};

	public SceneType sceneType = SceneType.Hell;
	 
	void Start () {
		spiderPool = new GameObjectPool(prefabSpider.gameObject, maxSpiderCount,
		                                   (gameObject) => {}, false);
		batPool = new GameObjectPool(prefabBat.gameObject, maxBatCount,
		                                   (gameObject) => {}, false);
		ghostPool = new GameObjectPool(prefabGhost.gameObject, maxGhostCount,
		                                   (gameObject) => {}, false);
		GenerateBatBeforeGame ();
		GenerateGhostBeforeGame ();
		GenerateSpiderBeforeGame ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void StartGenerate() {
		StartCoroutine (Generate());
	}
	
	IEnumerator Generate() {
		yield return new WaitForSeconds(0.5f);
		StartCoroutine(GenerateSpider ());
		StartCoroutine(GenerateBat ());
		StartCoroutine(GenerateGhost ());

	}
	

	IEnumerator GenerateBat() {
		if (batPool.numActive < maxBatCount) {
			Vector3 position;
			float scale ;
			// 1. generate a random coordinate
			while (true) {
				scale = Random.Range (batScale.x, batScale.y);
				position = Vector3.zero;
			
				Vector3 randomPosition = new Vector3 (Random.Range (-batRange, batRange), Random.Range (batYOffset.x, batYOffset.y), Random.Range (batZOffset.x, batZOffset.y));
				Vector3 newPosition = randomPosition + Quaternion.Euler(- 270, 0, 0) * transform.worldToLocalMatrix.MultiplyPoint (generatorReference.position);
				bool result = CheckFlowCollision (transform.localToWorldMatrix * newPosition);
				if (!result) {
					position = newPosition;
					break;
				}
			}
			Transform newFish = batPool.Spawn (Vector3.zero, Quaternion.identity).transform;
			newFish.SetParent (Bats);
			newFish.localScale = Vector3.one * scale;
			newFish.localRotation = Quaternion.Euler (0, 0, 0);
			newFish.localPosition = position; 
			newFish.GetComponent<Bat> ().pool = batPool;
		}
		yield return new WaitForSeconds (batInterval);
		
		yield return StartCoroutine (GenerateBat());
	}

	void GenerateBatBeforeGame() {
		for (int i = 0; i < beginBatCount; i++) {
			Vector3 position;
			float scale ;
			// 1. generate a random coordinate
			while (true) {
				scale = Random.Range (batScale.x, batScale.y);
				Vector3 randomPosition = new Vector3 (Random.Range (-beginBatRange, beginBatRange), Random.Range (beginBatYOffset.x, beginBatYOffset.y), Random.Range (beginBatZOffset.x, beginBatZOffset.y));
				Vector3 newPosition = randomPosition + Quaternion.Euler(- 270, 0, 0) * transform.worldToLocalMatrix.MultiplyPoint (generatorReference.position);
				bool result = CheckFlowCollision (transform.localToWorldMatrix * newPosition);
				if (!result) {
					position = newPosition;
					break;
				}
			}
			Transform newFish = batPool.Spawn (Vector3.zero, Quaternion.identity).transform;
			newFish.SetParent (Bats);
			newFish.localScale = Vector3.one * scale;
			newFish.localRotation = Quaternion.Euler (0, 0, 0);
			newFish.localPosition = position; 
			newFish.GetComponent<Bat> ().pool = batPool;
		}
	}

	IEnumerator GenerateSpider() {
		if (spiderPool.numActive < maxSpiderCount) {
			Vector3 position;
			float scale ;
			// 1. generate a random coordinate
			while (true) {
				float xOffset = Random.Range (-spiderRange, spiderRange) + spiderXOffset;
				float yOffset = Random.Range (spiderYOffset.x, spiderYOffset.y);
				float zOffset = Random.Range (spiderZOffset.x, spiderZOffset.y);
				scale = Random.Range (spiderScale.x, spiderScale.y);
				Vector3 randomPosition = new Vector3 (xOffset, yOffset, zOffset);
				Vector3 newPosition = randomPosition + Quaternion.Euler(- 270, 0, 0) * transform.worldToLocalMatrix.MultiplyPoint (generatorReference.position);
				bool result = CheckFlowCollision (transform.localToWorldMatrix * newPosition);
				if (!result) {
					position = newPosition;
					break;
				}
			}
			Transform newPlant = spiderPool.Spawn (Vector3.zero, Quaternion.identity).transform;
			newPlant.SetParent (Spiders);
			newPlant.localScale = Vector3.one * scale;
			newPlant.localRotation = Quaternion.Euler (0, 0, 0);
			newPlant.localPosition = position;
			newPlant.GetComponent<Spider> ().OnInit();
			newPlant.GetComponent<Spider> ().pool = spiderPool;
		}
		yield return new WaitForSeconds (spiderInterval);

		yield return StartCoroutine (GenerateSpider());
	}

	void  GenerateSpiderBeforeGame() {
		for (int i = 0; i < beginSpiderCount; i++) {
			Vector3 position;
			float scale ;
			// 1. generate a random coordinate
			while (true) {
				float xOffset = Random.Range (-beginSpiderRange, beginSpiderRange) + beginSpiderXOffset;
				float yOffset = Random.Range (beginSpiderYOffset.x, beginSpiderYOffset.y);
				float zOffset = Random.Range (beginSpiderZOffset.x, beginSpiderZOffset.y);
				scale = Random.Range (spiderScale.x, spiderScale.y);
				Vector3 randomPosition = new Vector3 (xOffset, yOffset, zOffset);
				Vector3 newPosition = randomPosition + Quaternion.Euler(- 270, 0, 0) * transform.worldToLocalMatrix.MultiplyPoint (generatorReference.position);
				bool result = CheckFlowCollision (transform.localToWorldMatrix * newPosition);
				if (!result) {
					position = newPosition;
					break;
				}
			}
			Transform newPlant = spiderPool.Spawn (Vector3.zero, Quaternion.identity).transform;
			newPlant.SetParent (Spiders);
			newPlant.localScale = Vector3.one * scale;
			newPlant.localRotation = Quaternion.Euler (0, 0, 0);
			newPlant.localPosition = position;
			newPlant.GetComponent<Spider> ().OnInit();
			newPlant.GetComponent<Spider> ().pool = spiderPool;
		}
	}

	IEnumerator GenerateGhost() {
		if (ghostPool.numActive < maxGhostCount) {
			Vector3 position;
			float scale ;
			// 1. generate a random coordinate
			while (true) {
				float xOffset = Random.Range (-ghostRange, ghostRange) + ghostXOffset;
				float yOffset = Random.Range (ghostYOffset.x, ghostYOffset.y);
				float zOffset = Random.Range (ghostZOffset.x, ghostZOffset.y);
				Vector3 randomPosition = new Vector3 (xOffset, yOffset, zOffset);
				Vector3 newPosition = randomPosition + Quaternion.Euler(- 270, 0, 0) * transform.worldToLocalMatrix.MultiplyPoint (generatorReference.position);

				scale = Random.Range (ghostScale.x, ghostScale.y);
				bool result = CheckFlowCollision (transform.localToWorldMatrix * newPosition);
				if (!result) {
					position = newPosition;
					break;
				}
			}
			Transform newFish = ghostPool.Spawn (Vector3.zero, Quaternion.identity).transform;
			newFish.SetParent (Ghosts);
			newFish.localScale = Vector3.one * scale;
			newFish.localRotation = Quaternion.Euler (0, 0, 0);
			newFish.localPosition = position; 
			newFish.GetComponent<Ghost> ().pool = ghostPool;
		}
		yield return new WaitForSeconds (ghostInterval);
	
		yield return StartCoroutine (GenerateGhost());
	}

	void GenerateGhostBeforeGame() {
		for (int i = 0; i < beginGhostCount; i++) {
			Vector3 position;
			float scale ;
			// 1. generate a random coordinate
			while (true) {
				float xOffset = Random.Range (-beginGhostRange, beginGhostRange) + ghostXOffset;
				float yOffset = Random.Range (beginGhostYOffset.x, beginGhostYOffset.y);
				float zOffset = Random.Range (beginGhostZOffset.x, beginGhostZOffset.y);
				Vector3 randomPosition = new Vector3 (xOffset, yOffset, zOffset);
				Vector3 newPosition = randomPosition + Quaternion.Euler(- 270, 0, 0) * transform.worldToLocalMatrix.MultiplyPoint (generatorReference.position);
				bool result = CheckFlowCollision (transform.localToWorldMatrix * newPosition);
				if (!result) {
					position = newPosition;
					break;
				}
			}
			scale = Random.Range (ghostScale.x, ghostScale.y);
			
			Transform newFish = ghostPool.Spawn (Vector3.zero, Quaternion.identity).transform;
			newFish.SetParent (Ghosts);
			newFish.localScale = Vector3.one * scale;
			newFish.localRotation = Quaternion.Euler (0, 0, 0);
			newFish.localPosition = position; 
			newFish.GetComponent<Ghost> ().pool = ghostPool;
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
}
