using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public Transform cubeHero;
	public Transform cameraReference;
	public Transform cameraOnlyHero;

	Vector3 lastReferencePosition;
	Vector3 newPosition;
	Vector3 cameraToReference;
	// Use this for initialization
	void Start () {
		lastReferencePosition = cameraReference.GetComponent<CameraReference> ().GetReferencePosition ();
		newPosition = transform.position;
		cameraToReference = transform.position - lastReferencePosition;
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		Vector3 newReferencePosition = cameraReference.GetComponent<CameraReference> ().GetReferencePosition ();
		if (newReferencePosition != lastReferencePosition) {
//			Vector3 deltaPosition = newReferencePosition - lastReferencePosition;
//			newPosition = transform.position + deltaPosition;
			newPosition = cameraToReference + newReferencePosition;
			lastReferencePosition = newReferencePosition;
		}
	}
	 
	void Update () {
		transform.position = Vector3.Lerp (transform.position, newPosition, Time.deltaTime * 2);
		if (Game.state != Game.State.BeforeGame) {
			cameraOnlyHero.gameObject.SetActive(false);
		} else {
			cameraOnlyHero.gameObject.SetActive(true);		
		}
	}
}
