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
	public Transform SoundBtn1;
	public Transform SoundBtn2;
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
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnShopButtonPress() {
		Shop.gameObject.SetActive (true);
		StartPage.GetComponent<Animator> ().Play ("Begin disappear");
	}
	public void OnShopOverButtonPress() {
		Shop.gameObject.SetActive (true);
		StartPage.GetComponent<Animator> ().Play ("Begin disappear");
		GameOver.GetComponent<Animator> ().Play ("Game over disappear");
	}

	public void OnShopCloseButtonPress() {
		Shop.GetComponent<Animator> ().Play ("shopOut");
		StartPage.gameObject.SetActive (true);
		StartPage.GetComponent<Animator> ().Play ("Begin appear");
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

	}

	public void OnContinueButtonPress() {
		StopPage.GetComponent<Animator> ().Play ("Stop disappear");
		StopBtn.gameObject.SetActive (true);
		
	}

	public void OnAdsButtonPress() {
		LoadGameOver ();
	}
	public void OnHomeButtonPress() {
		StopPage.GetComponent<Animator> ().Play ("Stop disappear");
		StartPage.gameObject.SetActive (true);
		StartPage.GetComponent<Animator> ().Play ("Begin appear");
		SoundBtn1.GetComponent<Toggle> ().isOn =(a==0)? true:false;
		ReLoad ();
	}
	public void OnHomeOverButtonPress() {
		GameOver.GetComponent<Animator> ().Play ("Game over disappear");
		StopPage.GetComponent<Animator> ().Play ("Stop disappear");
		StartPage.gameObject.SetActive (true);
		StartPage.GetComponent<Animator> ().Play ("Begin appear");
		SoundBtn1.GetComponent<Toggle> ().isOn =(a==0)? true:false;
		ReLoad ();
	}

	public void LoadGameOver(){
		Game.state = Game.State.GameOver;
		GameOver.gameObject.SetActive (true);
		GameOver.GetComponent<Animator> ().Play ("Game over appear");
		GameOn.GetComponent<Animator> ().Play ("GameDisappear");
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

}
