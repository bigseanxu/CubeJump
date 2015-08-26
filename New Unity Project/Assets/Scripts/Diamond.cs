using UnityEngine;
using System.Collections;

public class Diamond : MonoBehaviour {
	public float time;
	public ParticleSystem particles;
	// Use this for initialization
	void Update () {
		Move ();
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
			GameObject.Destroy(gameObject);
		}
	}
}
