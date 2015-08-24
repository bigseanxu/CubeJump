using UnityEngine;
using System.Collections;
using System.Timers;


public class SceneGenerator01 : MonoBehaviour {
	public Transform pillarGenerator;
	public Transform prefabFlow;
	public Transform prefabPlant;
	public Transform prefabCrocodile;
	public Transform flows;
	public Transform plants;
	public Transform Crocodiles;
	public Transform prefabFrog;
	public Transform prefabLotus;
	public Transform frog;
	public Transform lotus;


	public enum SceneType {
		Water
	};

	public SceneType sceneType = SceneType.Water;
	 
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Generate(Transform pillar) {
		if (sceneType == SceneType.Water) { 
			if (!pillar.GetComponent<Pillar>().isLeft) {
				GenerateFlow(pillar.position);
				GenerateFish(pillar.position);
			}
			GeneratePlant(pillar.position);
			if(Random.Range(0f,2f)>1f)
				GenerateFrog(pillar.position);
			else
				GenerateLotus(pillar.position);

		}
	}



	public void GenerateFlow(Vector3 position) {
		float xOffset = Random.Range (16, -16);
		float zOffset = Random.Range (-16, -11);
		Transform newFlow = (Transform)GameObject.Instantiate (prefabFlow, Vector3.zero, Quaternion.identity);
		newFlow.SetParent (flows);
		newFlow.localScale = Vector3.one;
		newFlow.rotation = Quaternion.Euler (-90, 0, 0);
		newFlow.position = position + new Vector3 (xOffset, 0, zOffset); 
	}

	public void GeneratePlant(Vector3 position) {
		float xOffset = Random.Range (-6, -1);
		float zOffset = Random.Range (-6, -1);

		Transform newPlant = (Transform)GameObject.Instantiate (prefabPlant, Vector3.zero, Quaternion.identity);
		newPlant.SetParent (plants);
		newPlant.localScale = Vector3.one;
		newPlant.rotation = Quaternion.Euler (-90, 0, 0);
		newPlant.position = position + new Vector3 (xOffset, 0, zOffset);
	}

	public void GenerateFish(Vector3 position) {
		float xOffset = Random.Range (-10, -16);
		float zOffset = Random.Range (-20, -36);
		int a= Random.Range (1, 5);
		if (a > 3) {
			Transform newFish = (Transform)GameObject.Instantiate (prefabCrocodile, Vector3.zero, Quaternion.identity);
			newFish.SetParent (Crocodiles);
			newFish.localScale = new Vector3 (10, 10, 10);
			newFish.rotation = Quaternion.Euler (-90, 0, 0);
			newFish.position = position + new Vector3 (xOffset, -3, zOffset);
		}
	}

	public void GenerateFrog(Vector3 position) {
		float xOffset = Random.Range (-10, -16);
		float zOffset = Random.Range (-20, -36);
		Transform newFrog = (Transform)GameObject.Instantiate (prefabFrog, Vector3.zero, Quaternion.identity);
		newFrog.SetParent (frog);
		newFrog.localScale =new Vector3(10,10,10);
		newFrog.rotation = Quaternion.Euler (-90, 0, 0);
		newFrog.position = position + new Vector3 (xOffset, 0, zOffset); 
	}

	public void GenerateLotus(Vector3 position) {
		float xOffset = Random.Range (-6, -1);
		float zOffset = Random.Range (-1, 6);
		Transform newFish = (Transform)GameObject.Instantiate (prefabLotus, Vector3.zero, Quaternion.identity);
		newFish.SetParent (lotus);
		newFish.localScale =new Vector3(10,10,10);
		newFish.rotation = Quaternion.Euler (-90, 0, 0);
		newFish.position = position + new Vector3 (xOffset, 0, zOffset); 
	}
}
