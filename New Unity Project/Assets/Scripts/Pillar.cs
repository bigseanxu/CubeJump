using UnityEngine;
using System.Collections;

public class Pillar : MonoBehaviour {
	public float tall;
	public bool isLeft;
	public Transform sceneGenerator;
	public SprayParticles sprayParticles;


	// Use this for initialization
	void Start () {

		HingeJoint hinge = GetComponent<HingeJoint> ();
		JointSpring spring = hinge.spring;
		spring.spring = 40;
		spring.damper = 10;
		spring.targetPosition = -180;

		hinge.spring = spring;
		hinge.useSpring = false;

		if (isLeft) {
			hinge.axis = Vector3.back;
		} else {
			hinge.axis = Vector3.left;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AnimationCallback() {
//		HingeJoint hinge = GetComponent<HingeJoint> ();
//		hinge.useMotor = true;
	}

	public void FallingDown () {
		HingeJoint hinge = GetComponent<HingeJoint> ();
		hinge.useSpring = true;
		GetComponent<Rigidbody> ().isKinematic = false;
	}

	public Vector3 GetCubePosition() {
		Vector3 ret;
		ret = transform.position;
		ret.y = ret.y + GetComponent<BoxCollider> ().bounds.max.y + 0.6f;
		return ret;
	}

	private Transform lastPillar;
	public Transform LastPillar {
		get { return lastPillar; }
		set { lastPillar = value; }
	}

	private Transform nextPillar;
	public Transform NextPillar {
		get { return nextPillar; }
		set { nextPillar = value; }
	}

	public void Show () {
		gameObject.SetActive (true);
		GetComponent<Animator> ().Play ("Show");
		PlaySprayParticle ();
	}

	void PlaySprayParticle() {

		sprayParticles.Stop ();
		float duration = 2;
		if (gameObject.name == "5x5(Clone)") {
			duration = 0.8f;
		} else if (gameObject.name == "10x10(Clone)") {
			duration = 1.6f;
		} else if (gameObject.name == "15x15(Clone)") {
			duration = 2.4f;
		} else {
			print ("error pillar PlaySprayParticle");
		}
		sprayParticles.SetPositon (transform.position);
		sprayParticles.Play ();
	}
}
