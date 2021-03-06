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
	bool waterOnce=true;
	bool isFall=true;
	public float fForceUp = 1500;
	public float fForceForward = 300;
	public Transform UIAudio;
	public ParticleSystem waterSpray;

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
		if (Input.GetKeyUp ("k")) {
			Game.diamond += 1;
		} else if (Input.GetKeyUp ("l")) {
			Game.diamond += 10;
		} else if (Input.GetKeyUp (";")) {
			Game.diamond += 100;
		} else if (Input.GetKeyUp ("i")) {
			Game.diamond -= 1;
		} else if (Input.GetKeyUp ("o")) {
			Game.diamond -= 10;
		} else if (Input.GetKeyUp ("p")){
			Game.diamond -= 100;
		}

		if (Game.state == Game.State.BeforeGame) {

#if UNITY_EDITOR
			if (Input.GetMouseButtonUp (0) && (EventSystem.current.currentSelectedGameObject == null && !EventSystem.current.IsPointerOverGameObject ())) {
#else
			if (Input.GetMouseButtonUp (0) && !EventSystem.current.IsPointerOverGameObject (Input.GetTouch (0).fingerId) && EventSystem.current.currentSelectedGameObject == null) {
#endif
				Game.SetState(Game.State.Gaming);
				
			}
		}	

#if UNITY_EDITOR
		if (Input.GetMouseButtonUp (0) && !EventSystem.current.IsPointerOverGameObject ()) {
#else
		if (Input.GetMouseButtonUp (0) && !EventSystem.current.IsPointerOverGameObject (Input.GetTouch (0).fingerId)) {
#endif
			if(Game.state == Game.State.Gaming)
				Jump();
				isFall=true;
		}
		if (transform.position.y < -5) {
			if (Game.sceneType <= 1 && waterOnce &&transform.GetChild (0).GetComponent<MeshRenderer> ().enabled && !Game.replay) {
				waterOnce = false;
				UIAudio.GetComponent<AudioList> ().HeroFallInWater.Play ();
			}
			if (Game.sceneType > 1 && waterOnce &&transform.GetChild (0).GetComponent<MeshRenderer> ().enabled &&  !Game.replay) {
				waterOnce = false;
				UIAudio.GetComponent<AudioList> ().Fall.Play ();
			}
		} else {
				waterOnce=true;
		}
		if (transform.position.y < -40) {
			if(live){
				UIAudio.GetComponent<AudioList> ().GameOver.Play ();
				UIAudio.GetComponent<AudioList> ().Theme.Stop();
				ctrl.GetComponent<GameCtrl> ().LoadGameOver ();
				live=false;
			}
		}
	}

	void OnTriggerEnter(Collider collider) {
				print("curr"+currentPillar.position.x);
				print("hero"+transform.position.x);
		if (collider.gameObject.name == "ColliderBox" && state== CubeState.Jumping) {
			isFall=false;
			collider.gameObject.SetActive(false);
			LandSuccess (collider.transform.parent);
			print ( " land success ");
		} else if (collider.gameObject.name == "water") {
				print("cube is in water");
				waterSpray.transform.position = transform.position;
				waterSpray.Play();

		} else if (collider.gameObject.name.StartsWith("Diamond") ) {
			// print ("cube is in diamond");
			UIAudio.GetComponent<AudioList> ().Diamond.Play ();

		}else if(collider.gameObject.name == "ColliderFall" && state== CubeState.Jumping){
				collider.gameObject.SetActive(false);
				//StartCoroutine(FallDown());
				//GetComponent<Rigidbody>().isKinematic=true;
					
				//GetComponent<Rigidbody>().isKinematic=false;
					
				Rigidbody rigid = GetComponent<Rigidbody> ();
				rigid.freezeRotation = false;
				float force = 200;
				if(isFaceLeft) {
					if(currentPillar.GetComponent<Pillar> ().NextPillar.position.z>transform.position.z){
						gameObject.GetComponent<Rigidbody> ().AddForceAtPosition((Vector3.down+Vector3.back * 3)*force,transform.localToWorldMatrix.MultiplyPoint(new Vector3(0,2,0)));
					}else{
						gameObject.GetComponent<Rigidbody> ().AddForceAtPosition((Vector3.down+Vector3.forward * 3)*force,transform.localToWorldMatrix.MultiplyPoint(new Vector3(0,-2,0)));
					}
					//gameObject.GetComponent<Rigidbody> ().AddForce(Vector3.up*600);
				}else{
					if(currentPillar.GetComponent<Pillar> ().NextPillar.position.x<transform.position.x){
					
						gameObject.GetComponent<Rigidbody> ().AddForceAtPosition((Vector3.down+Vector3.right * 3)*force,transform.localToWorldMatrix.MultiplyPoint(new Vector3(0,2,0)));
					//gameObject.GetComponent<Rigidbody> ().AddForce(Vector3.up*600);
					}else{
						gameObject.GetComponent<Rigidbody> ().AddForceAtPosition((Vector3.down+Vector3.left * 3)*force,transform.localToWorldMatrix.MultiplyPoint(new Vector3(0,-2,0)));
					}
				}
				state=CubeState.Dead;
				print ( " land not success");
		}

	}

	
	



	void OnCollisionEnter(Collision collision){
		int a = transform.GetChild (0).childCount;
		if (state== CubeState.Jumping&& collision.gameObject.tag=="Pillar") {
				UIAudio.GetComponent<AudioList> ().HeroBroken.Play ();
			transform.GetChild (0).GetComponent<MeshRenderer> ().enabled = false;

			if (transform.GetChild(0).GetComponent<TrailRenderer> () != null) {
				transform.GetChild (0).GetComponent<TrailRenderer> ().enabled = false;
			}

			if(a==0)
				return;
			if(a>1)
				transform.GetChild (0).GetChild (0).gameObject.SetActive (false);
			transform.GetComponent<BoxCollider>().isTrigger=true;
			transform.GetChild (0).GetChild (a-1).gameObject.SetActive (true);
			
		}
		if (collision.gameObject.name == "water") {
				UIAudio.GetComponent<AudioList> ().HeroFallInWater.Play ();
		}
//			print("collision is " + collision.gameObject.name);
	}

	void Jump() {
		if (state != CubeState.Ready) {
			print ("state is not ready, so we cannot jump");
			return;
		}

//		print ("jump");
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
		currentPillar.GetComponent<Pillar> ().NextPillar.GetComponent<Pillar> ().NextPillar.GetComponent<Pillar> ().Show ();
		if(currentPillar.GetComponent<Pillar> ().LastPillar!=null){
			if(currentPillar.GetComponent<Pillar> ().LastPillar.GetComponent<Pillar> ().LastPillar!=null)
				currentPillar.GetComponent<Pillar> ().LastPillar.GetComponent<Pillar> ().LastPillar.GetComponent<Pillar> ().Disappear();
		}
		UIAudio.GetComponent<AudioList> ().Jump.Play ();
	}

	void LandSuccess (Transform pillar) {
		UIAudio.GetComponent<AudioList> ().HeroAlive.Play ();
		state = CubeState.Fall;
		live = true;
//		print ("LandSuccess");
		isFaceLeft = !isFaceLeft;
		currentPillar = pillar;
		pillarGenerator.GetComponent<PillarGenerator> ().GeneratePillar ();
		transform.position = currentPillar.GetComponent<Pillar> ().GetCubePosition ();

		Rigidbody rigid = GetComponent<Rigidbody> ();
		rigid.velocity = Vector3.zero;
		//transform.rotation =isFaceLeft?Quaternion.Euler(-90, 0, 0):Quaternion.Euler(-90, -90, 0);
		rigid.freezeRotation = false;

		GetScore ();
		StartCoroutine (TurnAround());
	}

	void ShowNextPillar() {
	
	}

	IEnumerator TurnAround() {
		Vector3 forceUp = new Vector3 (0, 500, 0);
		gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;	
		gameObject.GetComponent<Rigidbody> ().AddForce (forceUp);
		if (isFaceLeft) {
			LeanTween.rotate (gameObject, new Vector3(-90, 0, 0), 0.2f);
		} else {
			LeanTween.rotate (gameObject, new Vector3(-90, -90, 0), 0.2f);
		}
		yield return new WaitForSeconds(0.2f);
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
