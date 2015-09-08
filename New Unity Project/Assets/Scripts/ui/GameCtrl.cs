using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameCtrl : MonoBehaviour {

	public Transform Shop;
	public Transform StartPage;
	public Transform StopPage;
	public Transform StopBtn;
	public Transform GameOver;
	public Transform GameOn;
	public Transform Loading;
	public Transform Share;
	public Transform CountDown;
	public Transform SoundBtn1;
	public Transform SoundBtn2;
	public Transform camareOnlyHero;
	bool shareAppear=false;
	int a;
	// Use this for initialization
	void Start () {
		Loading.gameObject.SetActive (true);
		Loading.GetComponent<Animator>().Play("loadingOut");
		Game.ResetState ();
		a=PlayerPrefs.GetInt("Sound",1);
		if(a==0)
			SoundBtn1.GetComponent<Toggle> ().isOn = true;
		else
			SoundBtn1.GetComponent<Toggle> ().isOn = false;
		Game.diamond=PlayerPrefs.GetInt ("Diamonds");
		if (Game.replay) {
			Game.StartGame ();
			Game.replay=false;
		}
		Input.ResetInputAxes();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnShopButtonPress() {
		Shop.gameObject.SetActive (true);
		Shop.GetComponent<Animator> ().Play ("shopIn");
		Game.state =Game.State.Shopping;
		StartPage.GetComponent<Animator> ().Play ("Begin disappear");
	}
	public void OnShopOverButtonPress() {
		Shop.gameObject.SetActive (true);
		Shop.GetComponent<Animator> ().Play ("shopIn");
		StartPage.GetComponent<Animator> ().Play ("Begin disappear");
		GameOver.GetComponent<Animator> ().Play ("Game over disappear");
	}

	public void OnShopCloseButtonPress() {
		Shop.GetComponent<Animator> ().Play ("shopOut");
		if (Game.state == Game.State.GameOver) {
			GameOver.gameObject.SetActive (true);
			GameOver.GetComponent<Animator> ().Play ("Game over appear");
		} else {
			Game.state =Game.State.BeforeGame;
			StartPage.gameObject.SetActive (true);
			StartPage.GetComponent<Animator> ().Play ("Begin appear");
		}

	}

	public void OnSoundButton1Press() {
		PlayerPrefs.SetInt ("Sound", SoundBtn1.GetComponent<Toggle> ().isOn ? 0 : 1);
		a=PlayerPrefs.GetInt("Sound");
		SoundBtn2.GetComponent<Toggle> ().isOn =(a==0)? true:false;
	}
	public void OnSoundButton2Press() {
		PlayerPrefs.SetInt ("Sound", SoundBtn2.GetComponent<Toggle> ().isOn ? 0 : 1);
		a=PlayerPrefs.GetInt("Sound");
		SoundBtn1.GetComponent<Toggle> ().isOn =(a==0)? true:false;
	}

	public void OnPauseButtonPress() {
		StopPage.gameObject.SetActive (true);
		StopPage.GetComponent<Animator> ().Play ("Stop appear");
		SoundBtn2.GetComponent<Toggle> ().isOn =(a==0)? true:false;
		StopBtn.gameObject.SetActive (false);
		camareOnlyHero.gameObject.SetActive (false);
		Game.pause = true;
		Time.timeScale = 0;
	}

	public void OnContinueButtonPress() {
		StopPage.GetComponent<Animator> ().Play ("Stop disappear");
		CountDown.gameObject.SetActive (true);
		CountDown.GetComponent<Animator> ().Play ("CountDown");
		//StopBtn.gameObject.SetActive (true);
		//Time.timeScale = 1;
		//StartCoroutine (PauseFlag ());
	}

	public void OnShareMenuButtonPress() {
		print ("OnShareMenuButtonPress");
		if(!shareAppear)
			Share.GetComponent<Animator> ().Play ("Share appear");
		if(shareAppear)
			Share.GetComponent<Animator> ().Play ("Share disappear");
		shareAppear = !shareAppear;
	}


	public void OnAdsButtonPress() {
		//LoadGameOver ();
	}
	public void OnHomeButtonPress() {
		Time.timeScale = 1;
	//	StopPage.GetComponent<Animator> ().Play ("Stop disappear");
	//	StartPage.gameObject.SetActive (true);
	//	StartPage.GetComponent<Animator> ().Play ("Begin appear");
	//	SoundBtn1.GetComponent<Toggle> ().isOn =(a==0)? true:false;
		ReLoad ();
	}
	public void OnHomeOverButtonPress() {
		Time.timeScale = 1;
	//	GameOver.GetComponent<Animator> ().Play ("Game over disappear");
	//	StopPage.GetComponent<Animator> ().Play ("Stop disappear");
	//	StartPage.gameObject.SetActive (true);
	//	StartPage.GetComponent<Animator> ().Play ("Begin appear");
	//	SoundBtn1.GetComponent<Toggle> ().isOn =(a==0)? true:false;
		ReLoad ();
	}

	public void OnRestartButtonPress() {
		Time.timeScale = 1;
	//	GameOver.GetComponent<Animator> ().Play ("Game over disappear");
	//	StopPage.GetComponent<Animator> ().Play ("Stop disappear");
	//	StartPage.gameObject.SetActive (true);
	//	StartPage.GetComponent<Animator> ().Play ("Begin appear");
	//	SoundBtn1.GetComponent<Toggle> ().isOn =(a==0)? true:false;
		ReLoad ();
		Game.replay = true;
	}


	public void LoadGameOver(){
		Game.state = Game.State.GameOver;
		GameOver.gameObject.SetActive (true);
		GameOver.GetComponent<Animator> ().Play ("Game over appear");
		GameOn.GetComponent<Animator> ().Play ("GameDisappear");
		PlayerPrefs.SetInt ("Diamonds", Game.diamond);
	}

	public void ReLoad(){
		Loading.gameObject.SetActive (true);
		StartCoroutine (ReLoadLevel());
	}



	IEnumerator ReLoadLevel(){
		yield return new WaitForSeconds (1.2f);
		Application.LoadLevel("ccc");
		Game.ResetState ();
		print ("loadlevel");
	}

	public void StartGame() {
		Game.SetState (Game.State.Gaming);
	}



}
