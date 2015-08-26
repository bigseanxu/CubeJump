using UnityEngine;
using System.Collections;

public class GameDirector : MonoBehaviour {
	public Transform beginScreen;
	public Transform gameScreen;
	// Use this for initialization
	void Start () {
		if (Game.gameDirector == null) {
			Game.gameDirector = this;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Game.isStateChanged) {
			if (Game.state == Game.State.Gaming) {
				GameStart();
			}
		}
	}

	public void GameStart() {
		beginScreen.GetComponent<Animator> ().Play ("Begin disappear");
		gameScreen.GetComponent<Animator> ().Play ("GameAppear");
	}



	public void EventHandler() {

	}
}
