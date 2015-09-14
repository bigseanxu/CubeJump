using UnityEngine;
using System.Collections;

public class Spider : MonoBehaviour {
	public float minSpeed;
	public float maxSpeed;
	public float distance;
	public Transform line;
	public GameObjectPool pool;
	float lineScale;
	float speed;
	// Use this for initialization
	void OnEnable () {
		speed = Random.Range (minSpeed, maxSpeed);
		lineScale = 1.0f / transform.localScale.x; 
		Move ();

		//line.GetComponent<LineRenderer> ().SetWidth (lineScale * 0.06f, lineScale * 0.06f);
	}
	
	// Update is called once per frame
	void Update () {
		CheckOutOfCamera ();
		line.GetComponent<LineRenderer> ().SetPosition (1, new Vector3(0, 20 - transform.localPosition.y * transform.localScale.y, 0));
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
			pool.Destroy (gameObject);
		}
	}

}
