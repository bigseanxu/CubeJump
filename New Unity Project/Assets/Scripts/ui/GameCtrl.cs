using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameCtrl : MonoBehaviour {

	public Transform Shop;
	public Transform Shopping;
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
	public Camera MainCamera;
	public Transform UIAudio;
	bool shareAppear=false;
	int a;
	int cheat;
	// Use this for initialization
	void Start () {
		cheat=0;
		StartCoroutine(CheatCheck());
		PlayerPrefs.SetInt ("Dinosaur", 1);
		PlayerPrefs.SetInt ("Random", 1);
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
		MainCamera.GetComponent<AudioListener>().enabled=(a==0)? false:true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnShopButtonPress() {
		UIAudio.GetComponent<AudioList> ().NormalButton.Play ();
		Shop.gameObject.SetActive (true);
		Shop.GetComponent<Animator> ().Play ("shopIn");
		Game.state =Game.State.Shopping;
		StartPage.GetComponent<Animator> ().Play ("Begin disappear");
		Game.isShopLoaded = true;

	}
	public void OnShopOverButtonPress() {
		UIAudio.GetComponent<AudioList> ().NormalButton.Play ();
		Shop.gameObject.SetActive (true);
		Shop.GetComponent<Animator> ().Play ("shopIn");
		StartPage.GetComponent<Animator> ().Play ("Begin disappear");
		GameOver.GetComponent<Animator> ().Play ("Game over disappear");
		Game.isShopLoaded = true;
	}

	public void OnShopCloseButtonPress() {
		UIAudio.GetComponent<AudioList> ().NormalButton.Play ();
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
		UIAudio.GetComponent<AudioList> ().NormalButton.Play ();
		PlayerPrefs.SetInt ("Sound", SoundBtn1.GetComponent<Toggle> ().isOn ? 0 : 1);
		a=PlayerPrefs.GetInt("Sound");
		SoundBtn2.GetComponent<Toggle> ().isOn =(a==0)? true:false;
		MainCamera.GetComponent<AudioListener>().enabled=(a==0)? false:true;
	}
	public void OnSoundButton2Press() {
		UIAudio.GetComponent<AudioList> ().NormalButton.Play ();
		PlayerPrefs.SetInt ("Sound", SoundBtn2.GetComponent<Toggle> ().isOn ? 0 : 1);
		a=PlayerPrefs.GetInt("Sound");
		SoundBtn1.GetComponent<Toggle> ().isOn =(a==0)? true:false;
		MainCamera.GetComponent<AudioListener>().enabled=(a==0)? false:true;
	}

	public void OnPauseButtonPress() {
		UIAudio.GetComponent<AudioList> ().NormalButton.Play ();
		StopPage.gameObject.SetActive (true);
		StopPage.GetComponent<Animator> ().Play ("Stop appear");
		SoundBtn2.GetComponent<Toggle> ().isOn =(a==0)? true:false;
		StopBtn.gameObject.SetActive (false);
		camareOnlyHero.gameObject.SetActive (false);
		Game.state = Game.State.Pause;
		Time.timeScale = 0;
	}

	public void OnContinueButtonPress() {
		UIAudio.GetComponent<AudioList> ().CountDown.Play ();
		StopPage.GetComponent<Animator> ().Play ("Stop disappear");
		CountDown.gameObject.SetActive (true);
		CountDown.GetComponent<Animator> ().Play ("CountDown");
		//StopBtn.gameObject.SetActive (true);
		//Time.timeScale = 1;
		//StartCoroutine (PauseFlag ());
	}

	public void OnShareMenuButtonPress() {
		UIAudio.GetComponent<AudioList> ().ShareButton.Play ();
		print ("OnShareMenuButtonPress");
		if(!shareAppear)
			Share.GetComponent<Animator> ().Play ("Share appear");
		if(shareAppear)
			Share.GetComponent<Animator> ().Play ("Share disappear");
		shareAppear = !shareAppear;
	}


	public void OnAdsButtonPress() {
		UIAudio.GetComponent<AudioList> ().NormalButton.Play ();
		Game.diamond +=20;
		PlayerPrefs.SetInt ("Diamonds", Game.diamond);
	}
	public void OnHomeButtonPress() {
		UIAudio.GetComponent<AudioList> ().NormalButton.Play ();
		Time.timeScale = 1;
	//	StopPage.GetComponent<Animator> ().Play ("Stop disappear");
	//	StartPage.gameObject.SetActive (true);
	//	StartPage.GetComponent<Animator> ().Play ("Begin appear");
	//	SoundBtn1.GetComponent<Toggle> ().isOn =(a==0)? true:false;
		ReLoad ();
	}
	public void OnHomeOverButtonPress() {
		UIAudio.GetComponent<AudioList> ().NormalButton.Play ();
		Time.timeScale = 1;
	//	GameOver.GetComponent<Animator> ().Play ("Game over disappear");
	//	StopPage.GetComponent<Animator> ().Play ("Stop disappear");
	//	StartPage.gameObject.SetActive (true);
	//	StartPage.GetComponent<Animator> ().Play ("Begin appear");
	//	SoundBtn1.GetComponent<Toggle> ().isOn =(a==0)? true:false;
		ReLoad ();
	}

	public void OnRestartButtonPress() {
		UIAudio.GetComponent<AudioList> ().TapToStart.Play ();
		Time.timeScale = 1;
	//	GameOver.GetComponent<Animator> ().Play ("Game over disappear");
	//	StopPage.GetComponent<Animator> ().Play ("Stop disappear");
	//	StartPage.gameObject.SetActive (true);
	//	StartPage.GetComponent<Animator> ().Play ("Begin appear");
	//	SoundBtn1.GetComponent<Toggle> ().isOn =(a==0)? true:false;
		ReLoad ();
		Game.replay = true;
	}

	public void OnFacebookPress(){
		UIAudio.GetComponent<AudioList> ().NormalButton.Play ();
	}

	public void OnTwitterPress(){
		UIAudio.GetComponent<AudioList> ().NormalButton.Play ();
	}
	public void OnRatePress(){
		UIAudio.GetComponent<AudioList> ().NormalButton.Play ();
	}
	public void OnGameCenterPress(){
		UIAudio.GetComponent<AudioList> ().NormalButton.Play ();
	}
	public void OnTapToPlayPress(){
		UIAudio.GetComponent<AudioList> ().TapToStart.Play ();
		Game.StartGame ();
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

	public void DiamondCheat(){
		cheat++;
		print (cheat);
		if(cheat>=20){
			PlayerPrefs.SetInt ("Diamonds", 8888);
		}
	}
	
	IEnumerator CheatCheck(){
		yield return new WaitForSeconds(5);
		if(cheat>0){
			cheat=0;
		}
		StartCoroutine(CheatCheck());
	}

}
