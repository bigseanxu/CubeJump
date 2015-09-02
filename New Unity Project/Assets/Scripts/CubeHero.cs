﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CubeHero : MonoBehaviour {
	public float size;
	public Transform ctrl;
	public Transform currentPillar;
	public Transform pillarGenerator;
	public Transform cameraReference;
	public float jumpDistanceBeforeGame = 3;
	public float jumpTimeBeforeGame = 0.6f;
	LTDescr jumpBeforeGameTween;
	bool isFaceLeft = true;
	bool live=true;
	public float fForceUp = 1500;
	public float fForceForward = 300;
	
	enum CubeState
	{ 
		BeforeGame,
		Ready,//可以跳跃
		Jumping,//跳跃中
		Fall,//已落地
		ReadyToRotate,//准备旋转
		Rotating,//旋转中
		Dead
	}
	CubeState state = CubeState.BeforeGame;
	// Use this for initialization
	void Start () {
		currentPillar = GameObject.Find ("StartPillar").transform;
		pillarGenerator = GameObject.Find ("PillarGenerator").transform;
		cameraReference = GameObject.Find ("CameraReference").transform;
		cameraReference.GetComponent<CameraReference> ().cubeHero = transform;

		Vector3 pos = transform.position;
		pos.y += jumpDistanceBeforeGame;
		jumpBeforeGameTween = LeanTween.move (gameObject, pos, jumpTimeBeforeGame).setLoopPingPong(-1).setEase(LeanTweenType.easeOutQuad).setOnComplete(JumpBeforeGameCallBack).setOnCompleteOnRepeat(true);
//		print ("cubehero start " + transform.localPosition);
		GetComponent<Rigidbody> ().useGravity = false;
		GetComponent<Rigidbody> ().freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (Game.state == Game.State.BeforeGame) {
			if (Input.GetMouseButtonUp (0) && !EventSystem.current.IsPointerOverGameObject ()) {
				Game.SetState(Game.State.Gaming);
				print("getmousebuttonup");
			}
		}	

		if (Input.GetMouseButtonUp (0)) {
			if(!Game.pause)
			Jump();
		}
		if (transform.position.y < -40) {
			if(live){
				ctrl.GetComponent<GameCtrl> ().LoadGameOver ();
				live=false;
			}
		}
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.name == "ColliderBox") {
			collider.gameObject.SetActive(false);
			LandSuccess (collider.transform.parent);
		} else if (collider.gameObject.name == "Water") {

		}
	}

	void OnCollisionEnter(Collision collision){
		int a = transform.GetChild (0).childCount;
		if (state== CubeState.Jumping&& collision.gameObject.tag=="Pillar") {
			
			transform.GetChild (0).GetComponent<MeshRenderer> ().enabled = false;
			transform.GetChild (0).GetComponent<TrailRenderer> ().enabled = false;
			if(a==0)
				return;
			if(a>1)
				transform.GetChild (0).GetChild (0).gameObject.SetActive (false);
			transform.GetComponent<BoxCollider>().isTrigger=true;
			transform.GetChild (0).GetChild (a-1).gameObject.SetActive (true);
			
		}
	}

	void Jump() {
		if (state != CubeState.Ready) {
			print ("state is not ready, so we cannot jump");
			return;
		}

		print ("jump");
		// give a force to jump
		Vector3 forceForward;
		Vector3 forceUp = new Vector3 (0, fForceUp, 0);
		if (isFaceLeft) {
			forceForward = Vector3.forward * fForceForward;
		} else {
			forceForward = - Vector3.right * fForceForward;
		}

		Vector3 finalForce = Quaternion.Inverse (transform.rotation) * (forceForward + forceUp);
		gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		gameObject.GetComponent<Rigidbody> ().AddForce (forceForward + forceUp);

		if (isFaceLeft) {
			LeanTween.rotate (gameObject, new Vector3(-90, 0, 0), 0.2f);
		} else {
			LeanTween.rotate (gameObject, new Vector3(-90, -90, 0), 0.2f);
        }
        // freeze rotation
		GetComponent<Rigidbody> ().freezeRotation = true;
		state = CubeState.Jumping;

	}

	void LandSuccess (Transform pillar) {
		state = CubeState.Fall;
		live = true;
		print ("LandSuccess");
		isFaceLeft = !isFaceLeft;
		currentPillar = pillar;
		pillarGenerator.GetComponent<PillarGenerator> ().GeneratePillar ();
		transform.position = currentPillar.GetComponent<Pillar> ().GetCubePosition ();

		Rigidbody rigid = GetComponent<Rigidbody> ();
		rigid.velocity = Vector3.zero;
		//transform.rotation =isFaceLeft?Quaternion.Euler(-90, 0, 0):Quaternion.Euler(-90, -90, 0);
		rigid.freezeRotation = false;

		GetScore ();
		currentPillar.GetComponent<Pillar> ().NextPillar.GetComponent<Pillar> ().Show ();
		StartCoroutine (TurnAround());
	}

	void ShowNextPillar() {
	
	}

	IEnumerator TurnAround() {
		Vector3 forceUp = new Vector3 (0, 500, 0);
		gameObject.GetComponent<Rigidbody> ().AddForce (forceUp);
		if (isFaceLeft) {
			LeanTween.rotate (gameObject, new Vector3(-90, 0, 0), 0.5f);
		} else {
			LeanTween.rotate (gameObject, new Vector3(-90, -90, 0), 0.5f);
		}
		yield return new WaitForSeconds(0.5f);
		currentPillar.GetComponent<Pillar> ().FallingDown ();
		state = CubeState.Ready;
    } 

	public Transform GetCurrPillar() {
		return currentPillar;
	}

	bool isLand = true;
	void JumpBeforeGameCallBack() {
		isLand = !isLand;
		if (isLand) {
			if (Game.state == Game.State.Gaming) {
				state = CubeState.Ready;
				GetComponent<Rigidbody> ().useGravity = true;
				jumpBeforeGameTween.cancel();
			}
		}
	}

	void GetScore() {
		Game.score++;
	}
}
