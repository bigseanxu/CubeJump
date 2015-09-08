using UnityEngine;
using System.Collections;

public class GameBestScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Game.bestScore = PlayerPrefs.GetInt ("bestScore", 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (Game.score > Game.bestScore) {
			Game.bestScore=Game.score;
			PlayerPrefs.SetInt ("bestScore", Game.bestScore);
		}
		GetComponent<ShowNumberInCanvas> ().SetNumber (Game.bestScore);
	}
}
