using UnityEngine;
using System.Collections;

public class Pillar : MonoBehaviour {
	public float tall;
	public bool isLeft;
	public Transform sceneGenerator;
	public SprayParticles sprayParticles;
	public SprayParticles sprayParticles2;

	// Use this for initialization
	void Start () {

		HingeJoint hinge = GetComponent<HingeJoint> ();
		JointSpring spring = hinge.spring;
		spring.spring = 20;
		spring.damper = 12;
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
		Transform UIAudio = GameObject.FindGameObjectWithTag ("Audio").transform;
		UIAudio.GetComponent<AudioList> ().PillarAppear.Play ();
		if (Game.sceneType <= 1) {
			PlaySprayParticle ();
		}
	}


	public void Disappear(){
		LeanTween.rotate(gameObject,new Vector3(transform.rotation.eulerAngles.x,transform.rotation.eulerAngles.y,180),0.2f).setOnComplete(DisappearMethod);
		//GetComponent<Animator> ().enabled=true;
		//GetComponent<Animator> ().Play ("Disappear");
	}
	void DisappearMethod(){
	//	LeanTween.rotate(gameObject,Quaternion.Euler(0,0,180),0.2f).setOnComplete();
		GetComponent<Animator> ().enabled=true;
		GetComponent<Animator> ().Play ("Disappear");
	}


	void PlaySprayParticle() {
		if (isLeft) {
			sprayParticles.Stop ();
			float duration = 1.5f;
			
			sprayParticles.SetPositon (transform.position);
			sprayParticles.Play ();
		} else {
			sprayParticles2.Stop ();
			float duration = 1.5f;
			
			sprayParticles2.SetPositon (transform.position);
			sprayParticles2.Play ();
		}
	}
}
