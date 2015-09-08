using UnityEngine;
using System.Collections;
using System.Timers;
using System.Collections.Generic;

public class SceneSpaceGenerator : BaseGenerator {
	public Transform pillarGenerator;

	public Transform prefabLine;
	public Transform verticalLines;
	public Transform horizontalLines;
	public Transform prefabLineLeft;
	public Transform generatorReference;
	
	public float horizontalLineInterval = 2f;
	public Vector3 horizontalLineScaleA = new Vector3(0.8f, 1, 1.2f);
	public Vector3 horizontalLineScaleB = new Vector3(0.8f, 1, 1.2f);
	public Vector2 horizontalLineZOffset = new Vector2(-5f, 5f);
	public float horizontalLineXRange = 20;
	public Vector2 horizontalLineYOffset = new Vector2 (0, 0);

	public float verticalLineInterval = 2f;
	public Vector3 verticalLineScaleA = new Vector3(0.8f, 1, 1.2f);
	public Vector3 verticalLineScaleB = new Vector3(0.8f, 1, 1.2f);
	public Vector2 verticalLineZOffset = new Vector2(-5f, 5f);
	public float verticalLineYRange = 20;
	public Vector2 verticalLineXOffset = new Vector2 (0, 0);



	
	public enum SceneType {
		Space
	};

	public SceneType sceneType = SceneType.Space;
	 
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void StartGenerate() {
		StartCoroutine (Generate());
	}
	
	IEnumerator Generate() {

		StartCoroutine(GenerateHorizontalLine());
		StartCoroutine(GenerateVerticalLineLeft());

		yield return null;
	}


	IEnumerator GenerateHorizontalLine() {
		print ("GenerateLine");
		Vector3 scale = new Vector3 (Random.Range (horizontalLineScaleA.x, horizontalLineScaleB.x),
		                             Random.Range (horizontalLineScaleA.y, horizontalLineScaleB.y),
		                             Random.Range (horizontalLineScaleA.z, horizontalLineScaleB.z));
		Vector3 position = Vector3.zero;
		// 1. generate a random coordinate
		while (true) {
			Vector3 randomPosition = new Vector3(Random.Range(- horizontalLineXRange, horizontalLineXRange), Random.Range(horizontalLineYOffset.x, horizontalLineYOffset.y), Random.Range(horizontalLineZOffset.x, horizontalLineZOffset.y));
			Vector3 newPosition = randomPosition + transform.worldToLocalMatrix.MultiplyPoint(generatorReference.position);
			bool result = false; // CheckFlowCollision(newPosition);
			if (!result) {
				position = newPosition;
				break;
			}
		}
		Transform newFlow = (Transform)GameObject.Instantiate (prefabLine);
		newFlow.SetParent (horizontalLines);
		newFlow.localScale = scale;
		newFlow.localRotation = Quaternion.Euler (-90, 0, 0);
		newFlow.localPosition = position; 

		yield return new WaitForSeconds (horizontalLineInterval);
		yield return StartCoroutine (GenerateHorizontalLine());
	}

	IEnumerator GenerateVerticalLineLeft() {
		Vector3 scale = new Vector3 (Random.Range (verticalLineScaleA.x, verticalLineScaleB.x),
		                             Random.Range (verticalLineScaleA.y, verticalLineScaleB.y),
		                             Random.Range (verticalLineScaleA.z, verticalLineScaleB.z));

		Vector3 position = Vector3.zero;
		// 1. generate a random coordinate
		while (true) {
			Vector3 randomPosition = new Vector3(Random.Range(verticalLineXOffset.x, verticalLineXOffset.y), Random.Range(-verticalLineYRange, verticalLineYRange), Random.Range(verticalLineZOffset.x, verticalLineZOffset.y));

			Vector3 newPosition = randomPosition + transform.worldToLocalMatrix.MultiplyPoint(generatorReference.position);
			bool result = false; // CheckFlowCollision(newPosition);
			if (!result) {
				position = newPosition;
				break;
			}
		}
		Transform newFlow = (Transform)GameObject.Instantiate (prefabLineLeft);
		newFlow.SetParent (verticalLines);
		newFlow.localScale = scale;
		newFlow.localRotation = Quaternion.Euler (-90, 0, 0);
		newFlow.localPosition = position; 

		yield return new WaitForSeconds (verticalLineInterval);
		yield return StartCoroutine (GenerateVerticalLineLeft());
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
