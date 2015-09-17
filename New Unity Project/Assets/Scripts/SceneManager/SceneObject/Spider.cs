using UnityEngine;
using System.Collections;

public class Spider : MonoBehaviour {
	public Vector2 lineSizeRange = new Vector2 (5, 10);
	public float minSpeed;
	public float maxSpeed;
	public float distance;
	public Transform line;
	public GameObjectPool pool;
	float lineScale;
	float speed;
	public float lineSize;
	public float startZ;
	LTDescr tween;
	// Use this for initialization
	void OnEnable () {

	}

	public void OnInit() {
		lineSize = Random.Range (lineSizeRange.x, lineSizeRange.y);
		startZ = transform.localPosition.z;
		speed = Random.Range (minSpeed, maxSpeed);
		lineScale = 1.0f / transform.localScale.x; 
		StartCoroutine(Move ());
		line.localScale = new Vector3 (lineScale, lineScale, lineScale);
	}

	// Update is called once per frame
	void Update () {
		CheckOutOfCamera ();
		line.GetComponent<LineRenderer> ().SetPosition (1, new Vector3(0, 0, lineSize - (transform.localPosition.z - startZ)));
	}
	IEnumerator Move() {
		yield return new WaitForSeconds (0.01f);
		Vector3 position = transform.position;
		position.y += distance;
		tween = LeanTween.move (gameObject, position, distance / speed).setLoopPingPong ();
	}
	void CheckOutOfCamera(){
		Vector2 vec = Camera.main.WorldToScreenPoint(transform.position);
		//print (vec.y);
		if (vec.y > 550) {
			if (tween != null) {
				tween.cancel();
			}
			pool.Destroy (gameObject);
		}
	}

}
