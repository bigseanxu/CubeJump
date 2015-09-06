using UnityEngine;
using System.Collections;

public class RainParticle : MonoBehaviour {
	public Transform cameraReference;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = cameraReference.position;
	}
}
