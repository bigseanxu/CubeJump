using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CubeHero : MonoBehaviour {
	public float size;

	public Transform currentPillar;
	public Transform pillarGenerator;

	public Vector3 cameraReference;
	public float jumpDistanceBeforeGame = 3;
	public float jumpTimeBeforeGame = 0.6f;
	LTDescr jumpBeforeGameTween;
	bool isFaceLeft = true;


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
		Vector3 pos = transform.position;
		pos.y += jumpDistanceBeforeGame;
		jumpBeforeGameTween = LeanTween.move (gameObject, pos, jumpTimeBeforeGame).setLoopPingPong(-1).setEase(LeanTweenType.easeOutQuad).setOnComplete(JumpBeforeGameCallBack).setOnCompleteOnRepeat(true);
		GetComponent<Rigidbody> ().useGravity = false;
		//GetComponent<Rigidbody> ().freezeRotation = true;
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
			Jump();
		}
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.name == "ColliderBox") {
			LandSuccess (collider.transform.parent);
		}
	}

	void Jump() {
		if (state != CubeState.Ready) {
			print ("state is not ready, so we cannot jump");
			return;
		}

		print ("jump");
		Vector3 forceForward;
		Vector3 forceUp = new Vector3 (0, 1500, 0);
		if (isFaceLeft) {
			forceForward = Vector3.forward * 300;
		} else {
			forceForward = - Vector3.right * 300;
		}
		gameObject.GetComponent<Rigidbody> ().AddForce (forceForward + forceUp);
		state = CubeState.Jumping;
	}

	void LandSuccess (Transform pillar) {
		print ("LandSuccess");
		isFaceLeft = !isFaceLeft;
		currentPillar = pillar;
		pillarGenerator.GetComponent<PillarGenerator> ().GeneratePillar ();
		transform.position = currentPillar.GetComponent<Pillar> ().GetCubePosition ();

		pillar.GetComponent<Pillar> ().FallingDown ();
		Rigidbody rigid = GetComponent<Rigidbody> ();
		rigid.velocity = Vector3.zero;
		transform.rotation = Quaternion.identity;
		state = CubeState.Ready;
	}

	public Transform GetCurrPillar() {
		return currentPillar;
	}

	bool isLand = false;
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
}
