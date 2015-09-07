using UnityEngine;
using System.Collections;

public class GameDirector : MonoBehaviour {
	public Transform beginScreen;
	public Transform gameScreen;
	public Transform sceneManager;

	public Transform gameLights;
	public Transform shopLights;
	public Transform shopScreen;
	// Use this for initialization
	void Start () {
		if (Game.gameDirector == null) {
			Game.gameDirector = this;
		}
		sceneManager.GetComponent<SceneManager> ().Generate ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Game.isStateChanged) {
			if (Game.state == Game.State.Gaming) {
				GameStart();
				Game.isStateChanged = false;
			}
		}

//		if (shopScreen.gameObject.activeInHierarchy) {
//			gameLights.gameObject.SetActive (false);
//		} else {
//			gameLights.gameObject.SetActive (true);
//		}
	}

	public void GameStart() {
		beginScreen.GetComponent<Animator> ().Play ("Begin disappear");
		gameScreen.GetComponent<Animator> ().Play ("GameAppear");
	}

	public void EventHandler() {

	}
	
}
