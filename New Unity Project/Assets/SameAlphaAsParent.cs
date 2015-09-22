using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SameAlphaAsParent : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float parentAlpha = transform.parent.GetComponent<Image> ().color.a;
		Color color = GetComponent<Image> ().color;
		color.a = parentAlpha;
		GetComponent<Image> ().color = color;
	}
}
