using UnityEngine;
using System.Collections;

public class SceneGenerator03 : MonoBehaviour {
	public Transform pillarGenerator;
	public Transform[] prefabCloud;
	public Transform Clouds;
	public Transform prefabPlant;
	public Transform Plants;
	public Transform prefabBalloon;
	public Transform Balloons;


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
			GenerateCloud(pillar.position);
			GenerateBalloon(pillar.position);
			GeneratePlant(pillar.position);

		}
	}

	public void GenerateCloud(Vector3 position) {
		float yOffset = Random.Range (1f, -1f);
		int i = Random.Range (0, 5);
		Transform newCloud = (Transform)GameObject.Instantiate (prefabCloud[i], Vector3.zero, Quaternion.identity);
		newCloud.SetParent (Clouds);
		newCloud.localScale = new Vector3(10,10,10);
		newCloud.rotation = Quaternion.Euler (-90, 0, 0);
		newCloud.position = position + new Vector3 (-2, yOffset, -15); 
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

	public void GenerateBalloon(Vector3 position) {
		float xOffset = Random.Range (-6, -1);
		float zOffset = Random.Range (-6, -1);
		Transform newBalloon = (Transform)GameObject.Instantiate (prefabBalloon, Vector3.zero, Quaternion.identity);
		newBalloon.SetParent (Balloons);
		newBalloon.localScale = new Vector3(10,10,10);
		newBalloon.rotation = Quaternion.Euler (-90, 0, 0);
		newBalloon.position = position + new Vector3 (xOffset, -3, zOffset); 
	}

}
