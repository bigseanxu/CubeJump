using UnityEngine;
using System.Collections;

public class DragonFly : MonoBehaviour {
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
	
	}

	void Move() {
		Vector3 position = transform.position;
		position.z += distance;
		LeanTween.move (gameObject, position, distance / speed).setOnComplete(Des);
	}
	void Des(){
		Destroy (gameObject);
	}
}
