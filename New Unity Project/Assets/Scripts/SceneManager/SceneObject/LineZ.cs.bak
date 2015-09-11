using UnityEngine;
using System.Collections;

public class LineZ : MonoBehaviour {
	public float minSpeed;
	public float maxSpeed;
	public float distance;
	public Texture [] lightMaps;
	float speed;
	// Use this for initialization
	void Start () {
		GetComponent<MeshRenderer>().material.mainTexture = lightMaps[Random.Range(0, lightMaps.Length - 1)];
		speed = Random.Range (minSpeed, maxSpeed);
		Move ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Move() {
		Vector3 position = transform.position;
		position.x += distance;
		LeanTween.move (gameObject, position, distance / speed).setOnComplete(Des);
	}
	void Des(){
		Destroy (gameObject);
	}
}
