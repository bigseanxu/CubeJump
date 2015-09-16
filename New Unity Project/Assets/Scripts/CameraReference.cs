using UnityEngine;
using System.Collections;

public class CameraReference : MonoBehaviour {
	public Transform startPillar;
	public Transform startPillar2;
	public Transform cubeHero;

	Vector3 startRef;
	Vector3 startRef2;
	Vector3 reference;
	// Use this for initialization
	void Start () {
		startRef = (startPillar.position + startPillar2.position) / 2.0f;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = GetReferencePosition ();
	}

	public Vector3 GetReferencePosition() {
		Vector3 ret;

		CubeHero hero = cubeHero.GetComponent<CubeHero>();
		if (hero.currentPillar == startPillar) {
			ret = startRef;
		} else {
			ret = (hero.currentPillar.transform.position + hero.currentPillar.GetComponent<Pillar> ().NextPillar.transform.position) / 2.0f;
		}
		ret.y = 0;
		return ret;
	}
}
