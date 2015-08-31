using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//GetComponent<Rigidbody> ().AddForceAtPosition (new Vector3(0, 100, 0), new Vector3(0, 0, 1));
		//transform.localRotation = Quaternion.Euler (90, 0, 0);
		//GetComponent<HingeJoint> ().useSpring = true;
		StartCoroutine (force ());
	}
	
	// Update is called once per frame
	void Update () {
		//print (transform.localRotation);
	}

	IEnumerator force() {
		GetComponent<Rigidbody> ().AddForceAtPosition (new Vector3(0, 100, 0), new Vector3(0, 0, 0.5f));
		GetComponent<Rigidbody> ().AddTorque (0, 0, 10);
		yield return new WaitForSeconds (2);
		print (transform.localRotation.eulerAngles);
		GetComponent<HingeJoint> ().useSpring = true;
	}
}
