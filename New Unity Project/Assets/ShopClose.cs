using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopClose : MonoBehaviour {

	// Use this for initialization
	public Transform Shopping;
	
	// Update is called once per frame
	void Update () {
		if (Shopping.GetComponent<Shop> ().velocity > 0) {
			transform.GetComponent<Button> ().interactable = false;
		} else {
			transform.GetComponent<Button> ().interactable = true;
		}
	}
}
