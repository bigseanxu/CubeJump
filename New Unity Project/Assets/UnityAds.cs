using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using System;

public class UnityAds : MonoBehaviour {

	public Transform adsButton;
	public string GameID = "68793";
	public int gameCountToShowAds = 5;
	public int diamondGain = 20;
	public int timeToShowAdsNext = 7200 ;//秒
	DateTime dt;
	DateTime dtNow;
	TimeSpan ts;
	// Use this for initialization
	void Start () {
		Advertisement.Initialize (GameID);
		string s = PlayerPrefs.GetString ("AdsButtonPress", "2010-10-1 10:01:30");
		dt = Convert.ToDateTime (s);
		dtNow=DateTime.Now;
		ts = dtNow.Subtract (dt);
//		print (ts.TotalSeconds);
		if ((int)ts.TotalSeconds > timeToShowAdsNext) {
			adsButton.gameObject.SetActive (true);
		} else {
			adsButton.gameObject.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {


	}

	public void OnAdsBtnClick(){
		ShowAds ();
		adsButton.gameObject.SetActive (false);
		PlayerPrefs.SetInt ("Diamonds", Game.diamond);
		DateTime dtStart = DateTime.Now;
		PlayerPrefs.SetString ("AdsButtonPress", dtStart.ToString ());
	}

	public void ShowAds() {
		if(Advertisement.IsReady()){
			Advertisement.Show();
		}
	}

	public bool checkAdsReady() {
		return Advertisement.IsReady ();
	}


}
