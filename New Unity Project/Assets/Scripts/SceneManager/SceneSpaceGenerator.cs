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

	public uint maxHorizontalLineCount = 10;
	public float horizontalLineInterval = 2f;
	public Vector3 horizontalLineScaleA = new Vector3(0.8f, 1, 1.2f);
	public Vector3 horizontalLineScaleB = new Vector3(0.8f, 1, 1.2f);
	public Vector2 horizontalLineZOffset = new Vector2(-5f, 5f);
	public float horizontalLineXRange = 20;
	public Vector2 horizontalLineYOffset = new Vector2 (0, 0);
	public int beginHorizontalCount = 2;
	public float beginHorizontalRange = 20;
	public Vector2 beginHorizontalYOffset = new Vector2(12f, 12f);
	public Vector2 beginHorizontalZOffset;


	public uint maxVerticalLineCount = 10;
	public float verticalLineInterval = 2f;
	public Vector3 verticalLineScaleA = new Vector3(0.8f, 1, 1.2f);
	public Vector3 verticalLineScaleB = new Vector3(0.8f, 1, 1.2f);
	public Vector2 verticalLineZOffset = new Vector2(-5f, 5f);
	public float verticalLineYRange = 20;
	public Vector2 verticalLineXOffset = new Vector2 (0, 0);
	public int beginVerticalCount = 2;
	public float beginVerticalYRange = 20;
	public Vector2 beginVerticalZOffset = new Vector2(12f, 12f);
	public Vector2 beginVerticalXOffset;


	GameObjectPool horizontalLinePool; 
	GameObjectPool verticalLinePool; 
	
	public enum SceneType {
		Space
	};

	public SceneType sceneType = SceneType.Space;
	 
	void Start () {
		horizontalLinePool = new GameObjectPool(prefabLine.gameObject, maxHorizontalLineCount,
		                              (gameObject) => {}, false);
		verticalLinePool = new GameObjectPool(prefabLineLeft.gameObject, maxVerticalLineCount,
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
		StartCoroutine(GenerateHorizontalLine());
		StartCoroutine(GenerateVerticalLineLeft());
		GenerateHorizontalLineBeforeGame ();
		GenerateVerticalLineLeftBeforeGame ();
	}


	IEnumerator GenerateHorizontalLine() {

		Vector3 scale = new Vector3 (Random.Range (horizontalLineScaleA.x, horizontalLineScaleB.x),
		                             Random.Range (horizontalLineScaleA.y, horizontalLineScaleB.y),
		                             Random.Range (horizontalLineScaleA.z, horizontalLineScaleB.z));
		Vector3 position = Vector3.zero;
		// 1. generate a random coordinate
		while (true) {
			Vector3 randomPosition = new Vector3 (Random.Range (- horizontalLineXRange, horizontalLineXRange), Random.Range (horizontalLineYOffset.x, horizontalLineYOffset.y), Random.Range (horizontalLineZOffset.x, horizontalLineZOffset.y));
			Vector3 newPosition = randomPosition + Quaternion.Euler(- 270, 0, 0) * transform.worldToLocalMatrix.MultiplyPoint (generatorReference.position);
			bool result = false; // CheckFlowCollision(newPosition);
			if (!result) {
				position = newPosition;
				break;
			}
		}
		if (horizontalLinePool.numActive < maxHorizontalLineCount) {
			Transform newFlow = horizontalLinePool.Spawn (Vector3.zero, Quaternion.identity).transform;
			newFlow.SetParent (horizontalLines);
			newFlow.localScale = scale;
			newFlow.localRotation = Quaternion.Euler (0, 0, 0);
			newFlow.localPosition = position; 
			newFlow.GetComponent<LineX> ().pool = horizontalLinePool;
		}
			yield return new WaitForSeconds (horizontalLineInterval);

		yield return StartCoroutine (GenerateHorizontalLine());
	}

	void GenerateHorizontalLineBeforeGame() {
		for (int i = 0; i < beginHorizontalCount; i++) {
			Vector3 scale = new Vector3 (Random.Range (horizontalLineScaleA.x, horizontalLineScaleB.x),
			                             Random.Range (horizontalLineScaleA.y, horizontalLineScaleB.y),
			                             Random.Range (horizontalLineScaleA.z, horizontalLineScaleB.z));
			Vector3 position = Vector3.zero;
			// 1. generate a random coordinate
			while (true) {
				Vector3 randomPosition = new Vector3 (Random.Range (- beginHorizontalRange, beginHorizontalRange), Random.Range (beginHorizontalYOffset.x, beginHorizontalYOffset.y), Random.Range (beginHorizontalZOffset.x, beginHorizontalZOffset.y));
				Vector3 newPosition = randomPosition + Quaternion.Euler(- 270, 0, 0) * transform.worldToLocalMatrix.MultiplyPoint (generatorReference.position);
				bool result = false; // CheckFlowCollision(newPosition);
				if (!result) {
					position = newPosition;
					break;
				}
			}
			if (horizontalLinePool.numActive < maxHorizontalLineCount) {
				Transform newFlow = horizontalLinePool.Spawn (Vector3.zero, Quaternion.identity).transform;
				newFlow.SetParent (horizontalLines);
				newFlow.localScale = scale;
				newFlow.localRotation = Quaternion.Euler (0, 0, 0);
				newFlow.localPosition = position; 
				newFlow.GetComponent<LineX> ().pool = horizontalLinePool;
			}
		}
	}

	IEnumerator GenerateVerticalLineLeft() {
	
		Vector3 scale = new Vector3 (Random.Range (verticalLineScaleA.x, verticalLineScaleB.x),
		                             Random.Range (verticalLineScaleA.y, verticalLineScaleB.y),
		                             Random.Range (verticalLineScaleA.z, verticalLineScaleB.z));

		Vector3 position = Vector3.zero;
		// 1. generate a random coordinate
		while (true) {
			Vector3 randomPosition = new Vector3 (Random.Range (verticalLineXOffset.x, verticalLineXOffset.y), Random.Range (-verticalLineYRange, verticalLineYRange), Random.Range (verticalLineZOffset.x, verticalLineZOffset.y));

			Vector3 newPosition = randomPosition + Quaternion.Euler(- 270, 0, 0) * transform.worldToLocalMatrix.MultiplyPoint (generatorReference.position);
			bool result = false; // CheckFlowCollision(newPosition);
			if (!result) {
				position = newPosition;
				break;
			}
		}
		if (verticalLinePool.numActive < maxVerticalLineCount) {
			Transform newFlow = verticalLinePool.Spawn (Vector3.zero, Quaternion.identity).transform;
			newFlow.SetParent (verticalLines);
			newFlow.localScale = scale;
			newFlow.localRotation = Quaternion.Euler (0, 0, 0);
			newFlow.localPosition = position; 
			newFlow.GetComponent<LineZ> ().pool = verticalLinePool;
		}
			yield return new WaitForSeconds (verticalLineInterval);

		yield return StartCoroutine (GenerateVerticalLineLeft());
	}

	
	void GenerateVerticalLineLeftBeforeGame() {
		for (int i = 0; i < beginVerticalCount; i++) { 
			Vector3 scale = new Vector3 (Random.Range (verticalLineScaleA.x, verticalLineScaleB.x),
			                             Random.Range (verticalLineScaleA.y, verticalLineScaleB.y),
			                             Random.Range (verticalLineScaleA.z, verticalLineScaleB.z));
			
			Vector3 position = Vector3.zero;
			// 1. generate a random coordinate
			while (true) {
				Vector3 randomPosition = new Vector3 (Random.Range (beginVerticalXOffset.x, beginVerticalXOffset.y), Random.Range (-beginVerticalYRange, beginVerticalYRange), Random.Range (beginVerticalZOffset.x, beginVerticalZOffset.y));
				
				Vector3 newPosition = randomPosition + Quaternion.Euler(- 270, 0, 0) * transform.worldToLocalMatrix.MultiplyPoint (generatorReference.position);
				bool result = false; // CheckFlowCollision(newPosition);
				if (!result) {
					position = newPosition;
					break;
				}
			}
			if (verticalLinePool.numActive < maxVerticalLineCount) {
				Transform newFlow = verticalLinePool.Spawn (Vector3.zero, Quaternion.identity).transform;
				newFlow.SetParent (verticalLines);
				newFlow.localScale = scale;
				newFlow.localRotation = Quaternion.Euler (0, 0, 0);
				newFlow.localPosition = position; 
				newFlow.GetComponent<LineZ> ().pool = verticalLinePool;
			}
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
