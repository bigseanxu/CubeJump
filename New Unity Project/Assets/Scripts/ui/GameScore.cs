using UnityEngine;
using System.Collections;

public class GameScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Game.bestScore = PlayerPrefs.GetInt ("bestScore", 0);
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<ShowNumberInCanvas> ().SetNumber (Game.score);
	}
}
