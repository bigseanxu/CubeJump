using UnityEngine;
using System.Collections;

public class DiamondGenerator : MonoBehaviour {
	public PillarGenerator pillarGenerator;
	public float chance = 0.5f;
	public Transform prefabDiamond;
	public ParticleSystem diamondParticle;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Generate() {
		float r = Random.Range (0f, 1f);
		if (r < chance) {
			Vector3 pillar = pillarGenerator.GetLastPillar().position;
			Vector3 pillar2 = pillarGenerator.GetLastPillar().GetComponent<Pillar>().LastPillar.position;
			Vector3 position = (pillar + pillar2) / 2.0f + Vector3.up * 8; 
//			print ("new diamond position = " + position + "   pillar " + pillar + "pillar2 = " + pillar2);

			Transform newDiamond = (Transform)GameObject.Instantiate (prefabDiamond);
			newDiamond.SetParent (transform);
			newDiamond.localScale = Vector3.one;
			newDiamond.localRotation = Quaternion.Euler (0, 0, 0);
			newDiamond.position = position; 
			newDiamond.GetComponent<Diamond> ().particles = diamondParticle;
		}
	}
}
