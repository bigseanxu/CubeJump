using UnityEngine;
using System.Collections;

public class LineZ : MonoBehaviour {
	public float minSpeed;
	public float maxSpeed;
	public float distance;
	public Texture [] lightMaps;
	public GameObjectPool pool;
	float speed;
	// Use this for initialization
	void OnEnable () {
		GetComponent<MeshRenderer>().material.mainTexture = lightMaps[Random.Range(0, lightMaps.Length - 1)];
		speed = Random.Range (minSpeed, maxSpeed);
		StartCoroutine(Move ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Move() {
		yield return new WaitForSeconds (0.01f);
		Vector3 position = transform.position;
		position.x += distance;
		LeanTween.move (gameObject, position, distance / speed).setOnComplete(Des);
	}
	void Des(){
		pool.Destroy (gameObject);
	}
}
