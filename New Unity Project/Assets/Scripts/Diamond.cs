using UnityEngine;
using System.Collections;

public class Diamond : MonoBehaviour {
	public float time;
	public ParticleSystem particles;
	public float rotateSpeed = 180;
	public Transform UIAudio;
	// Use this for initialization
	void Start() {

	}

	void Update () {
		Quaternion currentRotation = transform.rotation;
		Vector3 currentEulerAngles = currentRotation.eulerAngles;
		Vector3 newEulerAngles = currentEulerAngles;
		newEulerAngles.y = currentEulerAngles.y + rotateSpeed * Time.deltaTime;
		transform.rotation = Quaternion.Euler (newEulerAngles);
	}
	
	// Update is called once per frame

	void Move() {

		//LeanTween.rotate (gameObject, Vector3.forward , time).setLoopClamp ();
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.name == "CubeHero") {
			particles.transform.position = transform.position;
			particles.time = 0;
			particles.Play();
			Game.diamond++;
			GameObject.Destroy(transform.parent.gameObject);
		}
	}
}
