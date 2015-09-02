using UnityEngine;
using System.Collections;

public class Spider : MonoBehaviour {
	public float minSpeed;
	public float maxSpeed;
	public float distance;
	float speed;
	// Use this for initialization
	void Start () {
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
	}
	void CheckOutOfCamera(){
		Vector2 vec = Camera.main.WorldToScreenPoint(transform.position);
		//print (vec.y);
		if (vec.y > 1500) {
			Destroy(gameObject);
		}
	}
}
