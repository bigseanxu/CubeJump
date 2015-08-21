using UnityEngine;
using System.Collections;

public class SprayParticles : MonoBehaviour {
	public ParticleSystem sprayParticleF;
	public ParticleSystem sprayParticleB;
	public ParticleSystem sprayParticleL;
	public ParticleSystem sprayParticleR;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetPositon (Vector3 pos) {
		transform.position = pos;
	}

	public void Play() {
		sprayParticleF.time = 0;
		sprayParticleF.Play ();
		sprayParticleB.time = 0;
		sprayParticleB.Play ();
		sprayParticleL.time = 0;
		sprayParticleL.Play ();
		sprayParticleR.time = 0;
		sprayParticleR.Play ();
	}

	public void Stop() {
		sprayParticleF.Stop ();
		sprayParticleB.Stop ();
		sprayParticleL.Stop ();
		sprayParticleR.Stop ();
	}
}
