using UnityEngine;
using System.Collections;

public class SceneGenerator05 : MonoBehaviour {
	public Transform pillarGenerator;

	public Transform prefabPlant;
	public Transform Plants;



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


			GeneratePlant(pillar.position);

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



}
