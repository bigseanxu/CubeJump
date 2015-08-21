using UnityEngine;
using System.Collections;

public class Pillar : MonoBehaviour {
	public float tall;
	public bool isLeft;
	public Transform sceneGenerator;
	// Use this for initialization
	void Start () {
		HingeJoint hinge = GetComponent<HingeJoint> ();
		JointMotor motor = hinge.motor;
		motor.force = 1;

		motor.freeSpin = false;
		if (isLeft) {
			hinge.axis = Vector3.back;
			motor.targetVelocity = 3;
		} else {
			hinge.axis = Vector3.left;
			motor.targetVelocity = 3;
		}
		hinge.motor = motor;
		hinge.useMotor = false;
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
		hinge.useMotor = true;
//		if (isLeft) {
//			LeanTween.rotateLocal (gameObject, new Vector3 (0, 0, 90), 5.5f).setEase (LeanTweenType.easeInCubic);
//		} else {
//			LeanTween.rotateLocal (gameObject, new Vector3 (90, 0, 0), 5.5f).setEase (LeanTweenType.easeInCubic);
//		}
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
}
