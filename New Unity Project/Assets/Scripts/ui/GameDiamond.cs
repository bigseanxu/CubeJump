using UnityEngine;
using System.Collections;

public class GameDiamond : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<ShowNumberInCanvas> ().SetNumber (Game.diamond);
	}
}
