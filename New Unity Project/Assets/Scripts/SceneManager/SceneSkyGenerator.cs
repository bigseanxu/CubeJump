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

	public uint maxCloudCount = 10;
	public float cloudInterval = 2f;
	public float cloudRange = 20;
	public float cloudXOffset;
	public Vector2 cloudYOffset = new Vector2(-5f, 5f);
	public Vector2 cloudZOffset;
	public int beginCloudCount = 2;
	public float beginCloudRange = 20;
	public Vector2 beginCloudYOffset = new Vector2(12f, 12f);
	public Vector2 beginCloudZOffset;

	public uint maxPlaneCount = 10;
	public float planeInterval = 2f;
	public Vector2 planeScale = new Vector2(10f, 20f);
	public float planeRange = 20;
	public float planeXOffset;
	public Vector2 planeYOffset = new Vector2(10f, 20f);
	public Vector2 planeZOffset;
	public int beginPlaneCount = 2;
	public float beginPlaneRange = 20;
	public Vector2 beginPlaneYOffset = new Vector2(12f, 12f);
	public Vector2 beginPlaneZOffset;

	public uint maxBallonCount = 10;
	public float ballonInterval = 2f;
	public Vector2 ballonScale = new Vector2(8f, 12f);
	public float ballonRange = 20;
	public float ballonXOffset;
	public Vector2 ballonYOffset = new Vector2(10f, 20f);
	public Vector2 ballonZOffset;
	public int beginBallonCount = 2;
	public float beginBallonRange = 20;
	public Vector2 beginBallonYOffset = new Vector2(12f, 12f);
	public Vector2 beginBallonZOffset;

	public GameObjectPool planePool; 
	public GameObjectPool [] cloudPools = new GameObjectPool[6]; 
	public GameObjectPool ballonPool; 

	public enum SceneType {
		Sky
	};

	public SceneType sceneType = SceneType.Sky;
	 
	void Start () {
		planePool = new GameObjectPool(prefabPlane.gameObject, maxPlaneCount,
		                                   (gameObject) => {}, false);
		for (int i = 0; i < 6; i++) {
			cloudPools[i] = new GameObjectPool(prefabCloud[i].gameObject, maxCloudCount,
			                                   (gameObject) => {}, false);	
		}
		
		ballonPool = new GameObjectPool(prefabBalloon.gameObject, maxBallonCount,
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
		StartCoroutine(GenerateCloud());
		StartCoroutine(GeneratePlane());
		StartCoroutine(GenerateBallon());
		GenerateCloudBeforeGame ();
		GeneratePlaneBeforeGame ();
		GenerateBallonBeforeGame ();

	}

	IEnumerator GenerateCloud() {
		float xOffset = 20;
		float zOffset = 20;
		float scale = 10f;
		int i = Random.Range (0, prefabCloud.Length);
		if (cloudPools [i].numActive < maxCloudCount) {
			Vector3 position = Vector3.zero;
			
			Vector3 randomPosition = new Vector3 (Random.Range (-cloudRange, cloudRange), Random.Range (cloudYOffset.x, cloudYOffset.y), Random.Range (cloudZOffset.x, cloudZOffset.y));
			Vector3 newPosition = randomPosition + Quaternion.Euler(- 270, 0, 0) * transform.worldToLocalMatrix.MultiplyPoint (generatorReference.position);
			
			Transform newFish = cloudPools [i].Spawn (Vector3.zero, Quaternion.identity).transform;
			newFish.SetParent (Clouds);
			newFish.localScale = Vector3.one * scale;
			newFish.localRotation = Quaternion.Euler (0, 0, 0);
			newFish.localPosition = newPosition; 
			newFish.GetComponent<Cloud> ().pool = cloudPools [i];
		}
		yield return new WaitForSeconds (cloudInterval);
		
		yield return StartCoroutine (GenerateCloud());
	}

	void GenerateCloudBeforeGame() {
		float xOffset = 20;
		float zOffset = 20;
		float scale = 10f;

		for (int j = 0; j < beginCloudCount; j++) {
			int i = Random.Range (0, prefabCloud.Length);
			Vector3 position = Vector3.zero;
			
			Vector3 randomPosition = new Vector3 (Random.Range (-beginCloudRange, beginCloudRange), Random.Range (beginCloudYOffset.x, beginCloudYOffset.y), Random.Range (beginCloudZOffset.x, beginCloudZOffset.y));
			Vector3 newPosition = randomPosition + Quaternion.Euler(- 270, 0, 0) * transform.worldToLocalMatrix.MultiplyPoint (generatorReference.position);
			
			Transform newFish = cloudPools [i].Spawn (Vector3.zero, Quaternion.identity).transform;
			newFish.SetParent (Clouds);
			newFish.localScale = Vector3.one * scale;
			newFish.localRotation = Quaternion.Euler (0, 0, 0);
			newFish.localPosition = newPosition; 
			newFish.GetComponent<Cloud> ().pool = cloudPools [i];
		}
	}

	IEnumerator GenerateBallon() {
		if (ballonPool.numActive < maxBallonCount) {
			float xOffset = Random.Range (-ballonRange, ballonRange) + ballonXOffset;
			float yOffset = Random.Range (ballonYOffset.x, ballonYOffset.y);
			float zOffset = Random.Range (cloudZOffset.x, ballonZOffset.y);
			Vector3 randomPosition = new Vector3 (xOffset, yOffset, zOffset);
			Vector3 newPosition = randomPosition + Quaternion.Euler(- 270, 0, 0) * transform.worldToLocalMatrix.MultiplyPoint (generatorReference.position);


			float scale = Random.Range (ballonScale.x, ballonScale.y);
			Transform newPlant = ballonPool.Spawn (Vector3.zero, Quaternion.identity).transform;
			newPlant.SetParent (Balloons);
			newPlant.localScale = Vector3.one * scale;
			newPlant.localRotation = Quaternion.Euler (0, 0, 0);
			newPlant.localPosition = newPosition;
			newPlant.GetComponent<Balloon> ().pool = ballonPool;
		}
		yield return new WaitForSeconds (ballonInterval);
		
		yield return StartCoroutine (GenerateBallon());
	}
	
	void GenerateBallonBeforeGame() {
		for (int i = 0; i < beginBallonCount; i++) {
			float xOffset = Random.Range (-beginBallonRange, beginBallonRange) + ballonXOffset;
			float yOffset = Random.Range (beginBallonYOffset.x, beginBallonYOffset.y);
			float zOffset = Random.Range (beginBallonZOffset.x, beginBallonZOffset.y);
			Vector3 randomPosition = new Vector3 (xOffset, yOffset, zOffset);
			Vector3 newPosition = randomPosition + Quaternion.Euler(- 270, 0, 0) * transform.worldToLocalMatrix.MultiplyPoint (generatorReference.position);
			
			
			float scale = Random.Range (ballonScale.x, ballonScale.y);
			Transform newPlant = ballonPool.Spawn (Vector3.zero, Quaternion.identity).transform;
			newPlant.SetParent (Balloons);
			newPlant.localScale = Vector3.one * scale;
			newPlant.localRotation = Quaternion.Euler (0, 0, 0);
			newPlant.localPosition = newPosition;
			newPlant.GetComponent<Balloon> ().pool = ballonPool;
		}
	}

	IEnumerator GeneratePlane() {
		if (planePool.numActive < maxPlaneCount) {
//			print (planePool.numActive);
			float xOffset = Random.Range (-planeRange, planeRange) + planeXOffset;
			float yOffset = Random.Range (planeYOffset.x, planeYOffset.y);
			float zOffset = Random.Range (planeZOffset.x, planeZOffset.y);
			float scale = Random.Range (planeScale.x, planeScale.y);
			Vector3 position = Vector3.zero;
		
			Vector3 randomPosition = new Vector3 (xOffset, yOffset, zOffset);
			Vector3 newPosition = randomPosition + Quaternion.Euler(- 270, 0, 0) * transform.worldToLocalMatrix.MultiplyPoint (generatorReference.position);
		
			Transform newFish = planePool.Spawn (Vector3.zero, Quaternion.identity).transform;
			newFish.SetParent (Planes);
			newFish.localScale = Vector3.one * scale;
			newFish.localRotation = Quaternion.Euler (0, 0, 0);
			newFish.localPosition = newPosition; 
			newFish.GetComponent<AirPlane> ().pool = planePool;
		}
			yield return new WaitForSeconds (planeInterval);
		
		yield return StartCoroutine (GeneratePlane ());

	}

	void GeneratePlaneBeforeGame() {
		for (int i = 0; i < beginPlaneCount; i++) {
			print (planePool.numActive);
			float xOffset = Random.Range (-beginPlaneRange, beginPlaneRange) + planeXOffset;
			float yOffset = Random.Range (beginPlaneYOffset.x, beginPlaneYOffset.y);
			float zOffset = Random.Range (beginPlaneZOffset.x, beginPlaneZOffset.y);
			float scale = Random.Range (planeScale.x, planeScale.y);
			Vector3 position = Vector3.zero;
			
			Vector3 randomPosition = new Vector3 (xOffset, yOffset, zOffset);
			Vector3 newPosition = randomPosition + Quaternion.Euler(- 270, 0, 0) * transform.worldToLocalMatrix.MultiplyPoint (generatorReference.position);
			
			Transform newFish = planePool.Spawn (Vector3.zero, Quaternion.identity).transform;
			newFish.SetParent (Planes);
			newFish.localScale = Vector3.one * scale;
			newFish.localRotation = Quaternion.Euler (0, 0, 0);
			newFish.localPosition = newPosition; 
			newFish.GetComponent<AirPlane> ().pool = planePool;
		}	
	}

}
