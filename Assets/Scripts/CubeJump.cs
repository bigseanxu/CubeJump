using UnityEngine;
using System.Collections;

public class CubeJump : MonoBehaviour {
	public bool xisLeft=true;
	public static bool xisLeftC=true;
	public float xfar=5500;
	public float xheight=9000;
	public float xRotationAngle = 90;
	public float xMoveAndRotateTime = 0.2f;
	public float xMoveUpDistance = 1;
	bool xLeanTweenOnce=true; //这个是为了防止leantween在update里无限执行的bug
	Vector3 vec,vec2;
	CubeState cs=CubeState.ready;

	enum CubeState
	{ 
		ready=1,//可以跳跃
		jumping=2,//跳跃中
		fall=3,//已落地
		readyToRotate=4,//准备旋转
		rotating=5,//旋转中
		dead=6
	}
		// Use this for initialization
	void Start () {
		CubeState cs=CubeState.ready;
	}



	void FixedUpdate () {
		if (cs == CubeState.ready) {
			//Input.GetButtonDown ("Fire1")是鼠标左键,这里需要修改
			if (Input.GetButtonDown ("Fire1")) {
				gameObject.GetComponent<Rigidbody> ().isKinematic = true;
				gameObject.GetComponent<Rigidbody> ().isKinematic = false;
				if (xisLeft) {

					gameObject.GetComponent<Rigidbody> ().AddForce (Vector3.right * xfar);
					gameObject.GetComponent<Rigidbody> ().AddForce (Vector3.up * xheight);
					
				} else {

					gameObject.GetComponent<Rigidbody> ().AddForce (Vector3.forward * xfar);
					gameObject.GetComponent<Rigidbody> ().AddForce (Vector3.up * xheight);
					
				}
				xisLeft = !xisLeft;
				cs = CubeState.jumping;
			}
			
		} 
	
		else {

			PunchUp();
		}
		
	}
	void PunchUp(){
		xisLeftC = xisLeft;

		//判断是否跳跃完毕.这里不知道如何判断跳跃完成,所以就用gameObject.transform.position.y <= 1.02f判断.
		//但是这是有bug的,偶尔会连转2次,可能是由于update的原因.
		if (cs == CubeState.jumping && gameObject.transform.position.y < 1) {
			//这里判断方块是否跳准了,是否该死了
			if(1>1){
				cs = CubeState.dead;
				//触发死亡
				return;
			}
			cs = CubeState.fall;
			gameObject.GetComponent<Rigidbody> ().isKinematic = true;

		} 
		if (cs == CubeState.fall && xLeanTweenOnce == true) {
			cs=CubeState.readyToRotate;
			xLeanTweenOnce = false;
			vec2 = new Vector3 (transform.position.x, transform.position.y + xMoveUpDistance, transform.position.z);
			LeanTween.move (gameObject, vec2, xMoveAndRotateTime ).setLoopOnce ().setOnComplete (RotateCube);
		} 
	}


	//旋转方块
	void RotateCube(){
		if (cs == CubeState.readyToRotate) {
			cs = CubeState.rotating;
			if (xisLeft) {
				LeanTween.rotate (gameObject, Vector3.down*0, xMoveAndRotateTime ).setLoopOnce ().setOnComplete (DropCube);

			} else {
				LeanTween.rotate (gameObject, Vector3.down * xRotationAngle, xMoveAndRotateTime ).setLoopOnce ().setOnComplete (DropCube);

			}
		} else {

		}

	}

	//丢下方块
	void DropCube(){
		if (cs == CubeState.rotating) {
			vec2 = new Vector3 (transform.position.x, transform.position.y - xMoveUpDistance, transform.position.z);
			LeanTween.move (gameObject, vec2, xMoveAndRotateTime).setLoopOnce ().setOnComplete (ResetCube);
		} else {

		}
	}

	//为下一次跳跃做初始化
	void ResetCube(){
		gameObject.GetComponent<Rigidbody> ().isKinematic = false;

		cs = CubeState.ready;
		xLeanTweenOnce = true;

	}



}
