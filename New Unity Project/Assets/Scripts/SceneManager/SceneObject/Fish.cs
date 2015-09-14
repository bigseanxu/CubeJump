using UnityEngine;
using System.Collections;

public class Fish : MonoBehaviour {
	public float minSpeed;
	public float maxSpeed;
	public float distance;
	public GameObjectPool pool;
	float speed;
	// Use this for initialization
	void OnEnable () {
		speed = Random.Range (minSpeed, maxSpeed);
		StartCoroutine(Move ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	IEnumerator Move() {
		yield return new WaitForSeconds (0.01f);
		Vector3 position = transform.position;
		position.z += distance;
		LeanTween.move (gameObject, position, distance / speed).setOnComplete(Des);
	}
	void Des(){
		pool.Destroy (gameObject);
	}
}
