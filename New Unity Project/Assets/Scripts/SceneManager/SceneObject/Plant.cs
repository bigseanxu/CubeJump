using UnityEngine;
using System.Collections;

public class Plant : MonoBehaviour {
	public float minSpeed;
	public float maxSpeed;
	public float distance;
	public GameObjectPool pool;
	float speed;
	LTDescr tween;
	// Use this for initialization
	void OnEnable () {

		speed = Random.Range (minSpeed, maxSpeed);
		StartCoroutine (Move());
		Move ();
	}
	
	// Update is called once per frame
	void Update () {
		CheckOutOfCamera ();

	}

	IEnumerator Move() {
		yield return new WaitForSeconds(0.01f);
		Vector3 position = transform.localPosition;
		position.z += distance;
		tween = LeanTween.moveLocal (gameObject, position, distance / speed).setLoopPingPong ();

		//CheckOutOfCamera ();
	}

	void CheckOutOfCamera(){
		Vector2 vec = Camera.main.WorldToScreenPoint(transform.position);
		//print (vec.y);
		if (vec.y > Screen.height) {
			if (tween != null) {
				tween.cancel();
			}
			pool.Destroy (gameObject);
		}
	}
}
