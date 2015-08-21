using UnityEngine;
using System.Collections;

public class SceneGenerator : MonoBehaviour {
	public Transform pillarGenerator;
	public Transform prefabFlow;
	public Transform prefabPlant;
	public Transform prefabFish;
	public Transform flows;
	public Transform plants;
	public Transform fish;


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
			if (!pillar.GetComponent<Pillar> ().isLeft) {
				GenerateFlow (pillar.position);
				GenerateFish (pillar.position);
			}
			GeneratePlant (pillar.position);
		}
	}

	public void GenerateFlow(Vector3 position) {
		Transform newFlow = (Transform)GameObject.Instantiate (prefabFlow, Vector3.zero, Quaternion.identity);
		newFlow.SetParent (flows);
		newFlow.localScale = Vector3.one;
		newFlow.rotation = Quaternion.Euler (-90, 0, 0);
		newFlow.position = position + new Vector3 (-2, 0, -15); 
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
		Transform newFish = (Transform)GameObject.Instantiate (prefabFish, Vector3.zero, Quaternion.identity);
		newFish.SetParent (fish);
		newFish.localScale = Vector3.one;
		newFish.rotation = Quaternion.Euler (-90, 0, 0);
		newFish.position = position + new Vector3 (-2, -3, -15); 
	}




}
