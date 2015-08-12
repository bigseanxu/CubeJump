using UnityEngine;
using System.Collections;

public class Diamond : MonoBehaviour {



	public float time;

	// Use this for initialization
	void Update () {

		Move ();
	}
	
	// Update is called once per frame

	void Move() {

		//LeanTween.rotate (gameObject, Vector3.forward , time).setLoopClamp ();
	}
}
