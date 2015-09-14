using UnityEngine;
using System.Collections;

public class Plant : MonoBehaviour {
	public float minSpeed;
	public float maxSpeed;
	public float distance;
	public GameObjectPool pool;
	float speed;
	// Use this for initialization
	void OnEnable () {

		speed = Random.Range (minSpeed, maxSpeed);
		Move ();
	}
	
	// Update is called once per frame
	void Update () {
		CheckOutOfCamera ();
	}
	void Move() {
		Vector3 position = transform.position;
		position.y += distance;
		LeanTween.move (gameObject, position, distance / speed).setLoopPingPong ();
		//CheckOutOfCamera ();
	}

	void CheckOutOfCamera(){
		Vector2 vec = Camera.main.WorldToScreenPoint(transform.position);
		//print (vec.y);
		if (vec.y >1500) {
			pool.Destroy (gameObject);
		}
	}
}
