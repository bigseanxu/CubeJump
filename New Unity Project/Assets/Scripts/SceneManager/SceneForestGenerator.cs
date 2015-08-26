using UnityEngine;
using System.Collections;

public class SceneForestGenerator : BaseGenerator {
	public Transform pillarGenerator;
	public Transform[] prefabCloud;
	public Transform Clouds;
	public Transform prefabPlant;
	public Transform Plants;
	public Transform prefabFly;
	public Transform Flys;
	public Transform prefabButterFly;
	public Transform ButterFlys;


	public enum SceneType {
		Water
	};

	public SceneType sceneType = SceneType.Water;
	 
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void StartGenerate() {
		
	}

	public void Generate(Transform pillar) {
		if (sceneType == SceneType.Water) { 
			GenerateCloud(pillar.position);
			GenerateFly(pillar.position);
			GeneratePlant(pillar.position);
			GenerateButterFly(pillar.position);
		}
	}

	public void GenerateCloud(Vector3 position) {
		float xOffset = Random.Range (-10, -16);
		float zOffset = Random.Range (-20, -36);
		int i = Random.Range (0, 5);
		Transform newCloud = (Transform)GameObject.Instantiate (prefabCloud[i], Vector3.zero, Quaternion.identity);
		newCloud.SetParent (Clouds);
		newCloud.localScale =new Vector3(10,10,10);
		newCloud.rotation = Quaternion.Euler (-90, 0, 0);
		newCloud.position = position + new Vector3 (xOffset, 0, zOffset); 
	}


	public void GeneratePlant(Vector3 position) {
		float xOffset = Random.Range (-6, -1);
		float zOffset = Random.Range (-6, -1);
		
		Transform newPlant = (Transform)GameObject.Instantiate (prefabPlant, Vector3.zero, Quaternion.identity);
		newPlant.SetParent (Plants);
		newPlant.localScale = Vector3.one;
		newPlant.rotation = Quaternion.Euler (-90, 0, 0);
		newPlant.position = position + new Vector3 (xOffset, 0, zOffset);
	}

	public void GenerateFly(Vector3 position) {
		float xOffset = Random.Range (-10, -16);
		float zOffset = Random.Range (-20, -36);
		Transform newFlow = (Transform)GameObject.Instantiate (prefabFly, Vector3.zero, Quaternion.identity);
		newFlow.SetParent (Flys);
		newFlow.localScale = new Vector3(10,10,10);
		newFlow.rotation = Quaternion.Euler (-90, 0, 0);
		newFlow.position = position + new Vector3 (xOffset, 2, zOffset); 
	}
	public void GenerateButterFly(Vector3 position) {
		float xOffset = Random.Range (-10, -16);
		float zOffset = Random.Range (-20, -36);
		Transform newButterFly = (Transform)GameObject.Instantiate (prefabButterFly, Vector3.zero, Quaternion.identity);
		newButterFly.SetParent (ButterFlys);
		newButterFly.localScale =new Vector3(10,10,10);
		newButterFly.rotation = Quaternion.Euler (-90, 0, 0);
		newButterFly.position = position + new Vector3 (xOffset, 2, zOffset); 
	}

}
