using UnityEngine;
using System.Collections;

public class QuitShop : MonoBehaviour {

	public Transform shopscreen;
	public Transform over;
	public Transform StopBtn;
	public Transform camareOnlyHero;
	public Transform UIAudio;
	// Use this for initialization
	public void ExitShop(){
		gameObject.SetActive (false);
	}

	public void InShop(){
		shopscreen.GetComponent<Animator>().Play ("shopIn");
	}
	public void OnContinue(){
		Game.state = Game.State.Gaming;
		Time.timeScale = 1;
		StopBtn.gameObject.SetActive (true);
		camareOnlyHero.gameObject.SetActive (false);
	}

	public void GameOverFixDisappear(){
		over.gameObject.SetActive (false);
	}
	public void ShopItemChoose(){
		UIAudio.GetComponent<AudioList> ().ChooseHero.Play ();
	}

}
