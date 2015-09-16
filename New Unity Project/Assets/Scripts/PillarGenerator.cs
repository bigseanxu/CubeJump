using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PillarGenerator : MonoBehaviour {
	public Transform[] pillarPrefabs;
	public Transform pillarGroup;
	public Transform sceneGenerator;
	public Transform diamondGenerator;
	public Transform UIAudio;

	//public float minSmallToSmall; 
	//public float minSmallToMedium;

	public float minDistance = 7;
	public float maxDistance = 9;
	public float minDistanceB = 7;
	public float maxDistanceB = 9;
	public Transform startPillar;
	public Transform startPillar2;
	public SprayParticles sprayParticles;
	public Vector3 gravity = new Vector3 (0, - 50, 0);


	List<Transform> pillars = new List<Transform> ();
	Vector3 lastPillarPosition = new Vector3(9.2f, 0, -3.8f);
	bool lastPillarLeft = true;



	// Use this for initialization
	void Start () {
		Physics.gravity = gravity;
		pillars.Add (startPillar);
		pillars.Add (startPillar2);
		Transform pillar3 = GeneratePillar (pillarPrefabs[1]);
		GeneratePillar ();
		GeneratePillar ();
		startPillar.GetComponent<Pillar> ().NextPillar = startPillar2;
		startPillar2.GetComponent<Pillar> ().NextPillar = pillar3;
	}
	
	// Update is called once per frame 
	void Update () {

	}

	public Transform GeneratePillar(Transform pillarPrefab = null) {
//		int rangeA = 0;
//		int rangeB = pillarPrefabs.Length;
//		if (pillars [pillars.Count - 1].name.Contains ("5x5")) {
//			rangeB = rangeB - 1;
//		} else if (pillars [pillars.Count - 1].name.Contains ("15x15")) {
//			rangeA = 1;
//		}
		Transform prefab;
		if (pillarPrefab == null) {
			prefab = pillarPrefabs [Random.Range (0, pillarPrefabs.Length)];
		} else {
			prefab = pillarPrefab;
		}
		Transform newPillar = (Transform)GameObject.Instantiate (prefab, Vector3.zero, Quaternion.identity);
		newPillar.SetParent (pillarGroup);
		// newPillar.localScale = Vector3.one;
		newPillar.rotation = Quaternion.identity;


		float distance = Random.Range(0f, 1f) < 0.5f ? Random.Range(minDistance, minDistanceB) : Random.Range(maxDistance, maxDistanceB);
		if (lastPillarLeft) {
			newPillar.transform.position = lastPillarPosition + new Vector3 (- distance, 0, 0);
			newPillar.GetComponent<HingeJoint>().connectedAnchor = newPillar.transform.position;
			newPillar.GetComponent<Pillar> ().isLeft = !lastPillarLeft;
		} else {
			newPillar.transform.position = lastPillarPosition + new Vector3 (0, 0, distance);
			newPillar.GetComponent<HingeJoint>().connectedAnchor = lastPillarPosition + new Vector3 (0, 0, distance);
			newPillar.GetComponent<Pillar> ().isLeft = !lastPillarLeft;
		}

		//		print (newPillar.position);
		lastPillarPosition = newPillar.position;
		lastPillarLeft = !lastPillarLeft;

		if (pillars.Count != 0) {
			Transform lastPillar = pillars [pillars.Count - 1];
			lastPillar.GetComponent<Pillar> ().NextPillar = newPillar;
			newPillar.GetComponent<Pillar> ().LastPillar = lastPillar;
		} else {
			startPillar2.GetComponent<Pillar>().NextPillar = newPillar;
			newPillar.GetComponent<Pillar> ().LastPillar = startPillar2;
		}
		newPillar.GetComponent<Pillar> ().sprayParticles = sprayParticles;
		newPillar.gameObject.SetActive (false);
		newPillar.GetComponent<Rigidbody> ().isKinematic = true;
		pillars.Add (newPillar);

		//sceneGenerator.GetComponent<SceneGenerator> ().Generate (newPillar);
		diamondGenerator.GetComponent<DiamondGenerator> ().Generate ();
//		PlaySprayParticle ();
		return newPillar;
	}



	public List<Transform> GetPillars() {
		return pillars;
	}

	public Transform GetLastPillar() {
		return pillars [pillars.Count - 1];
	}
}
