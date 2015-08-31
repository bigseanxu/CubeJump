using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PillarGenerator : MonoBehaviour {
	public Transform[] pillarPrefabs;
	public Transform pillarGroup;
	public Transform sceneGenerator;
	public Transform diamondGenerator;

	//public float minSmallToSmall; 
	//public float minSmallToMedium;

	public float minDistance = 7;
	public float maxDistance = 9;

	public Transform startPillar2;
	public SprayParticles sprayParticles;
	public Vector3 gravity = new Vector3 (0, - 50, 0);


	List<Transform> pillars = new List<Transform> ();
	Vector3 lastPillarPosition = new Vector3(9.2f, 0, -4.6f);
	bool lastPillarLeft = true;



	// Use this for initialization
	void Start () {
		Physics.gravity = gravity;
		GeneratePillar ();
		GeneratePillar ();
		GeneratePillar ();
	}
	
	// Update is called once per frame 
	void Update () {

	}

	public void GeneratePillar() {
		Transform prefab = pillarPrefabs [Random.Range (0, pillarPrefabs.Length)];
		Transform newPillar = (Transform)GameObject.Instantiate (prefab, Vector3.zero, Quaternion.identity);
		newPillar.SetParent (pillarGroup);
		newPillar.localScale = Vector3.one;
		newPillar.rotation = Quaternion.identity;


		float distance = Random.Range (minDistance, maxDistance);
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

	}



	public List<Transform> GetPillars() {
		return pillars;
	}

	public Transform GetLastPillar() {
		return pillars [pillars.Count - 1];
	}
}
