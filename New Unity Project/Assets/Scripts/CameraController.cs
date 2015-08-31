using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public Transform cubeHero;
	public Transform cameraReference;

	Vector3 lastReferencePosition;
	Vector3 newPosition;
	// Use this for initialization
	void Start () {
		lastReferencePosition = cameraReference.GetComponent<CameraReference> ().GetReferencePosition ();
		newPosition = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		Vector3 newReferencePosition = cameraReference.GetComponent<CameraReference> ().GetReferencePosition ();
		if (newReferencePosition != lastReferencePosition) {
			Vector3 deltaPosition = newReferencePosition - lastReferencePosition;
			newPosition = transform.position + deltaPosition;
			lastReferencePosition = newReferencePosition;
		}
	}
	 
	void Update () {
		transform.position = Vector3.Lerp (transform.position, newPosition, Time.deltaTime * 2);
	}
}
