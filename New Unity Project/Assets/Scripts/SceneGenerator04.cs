using UnityEngine;
using System.Collections;

public class SceneGenerator04 : MonoBehaviour {
	public Transform pillarGenerator;

	public Transform prefabPlant;
	public Transform Plants;
	public Transform prefabBat;
	public Transform Bats;
	public Transform prefabSpider;
	public Transform Spiders;
	public Transform prefabGhost;
	public Transform Ghosts;

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

			GenerateBat(pillar.position);
			GeneratePlant(pillar.position);
			GenerateSpider(pillar.position);
			GenerateGhost(pillar.position);
		}
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

	public void GenerateBat(Vector3 position) {
		Transform newBat = (Transform)GameObject.Instantiate (prefabBat, Vector3.zero, Quaternion.identity);
		newBat.SetParent (Bats);
		newBat.localScale =  new Vector3(10,10,10);
		newBat.rotation = Quaternion.Euler (-90, 0, 0);
		newBat.position = position + new Vector3 (-2, 2, -15); 
	}

	public void GenerateSpider(Vector3 position) {
		float xOffset = Random.Range (-16, -1);
		float zOffset = Random.Range (-1, 8);

		Transform newSpider = (Transform)GameObject.Instantiate (prefabSpider, Vector3.zero, Quaternion.identity);
		newSpider.SetParent (Spiders);
		newSpider.localScale = new Vector3(0.3f,0.3f,0.3f);
		newSpider.rotation = Quaternion.Euler (-90, 0, 0);
		newSpider.position = position + new Vector3 (xOffset, 3, zOffset); 
	}

	public void GenerateGhost(Vector3 position) {
		float xOffset = Random.Range (16, -11);
		float zOffset = Random.Range (-16, -11);
		
		Transform newGhost = (Transform)GameObject.Instantiate (prefabGhost, Vector3.zero, Quaternion.identity);
		newGhost.SetParent (Ghosts);
		newGhost.localScale = new Vector3(10,10,10);
		newGhost.rotation = Quaternion.Euler (-90, 0, 0);
		newGhost.position = position + new Vector3 (xOffset, 4, zOffset); 
	}

}
