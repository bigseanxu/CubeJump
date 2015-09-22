using UnityEngine;
using System.Collections;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;

public class GameGoogle : MonoBehaviour {
	private InterstitialAd interstitial;
	public float interval = 100;
	static private float time = 0;
	bool isUIOnclick = false;
	public string adUnitIdEditor ="ca-app-pub-7896569660771969/8907844135";
	public string adUnitIdANDROID ="ca-app-pub-7896569660771969/4598629737";
	public string adUnitIdIPHONE = "ca-app-pub-7896569660771969/8907844135";
	// Use this for initialization
	void Start () {
		RequestInterstitial();
	}
	
	// Update is called once per frame
	void Update () {
		//print (Game.status );
		time += Time.deltaTime;
		//if里写明弹窗条件
		if (Game.state == Game.State.GameOver) {
			ShowInterstitial();

		}
	}
	private AdRequest createAdRequest()
	{
		return new AdRequest.Builder()
			.AddTestDevice(AdRequest.TestDeviceSimulator)
				.AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
				.AddKeyword("game")
				.SetGender(Gender.Male)
				.SetBirthday(new DateTime(1985, 1, 1))
				.TagForChildDirectedTreatment(false)
				.AddExtra("color_bg", "9B30FF")
				.Build();
		
	}
	private void RequestInterstitial()
	{
		#if UNITY_EDITOR
		string adUnitId = "ca-app-pub-7896569660771969/8907844135";
		#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-7896569660771969/4598629737";
		#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-7896569660771969/8907844135";
		#else
		string adUnitId = "unexpected_platform";
		#endif
		
		// Create an interstitial.
		interstitial = new InterstitialAd(adUnitId);
		// Register for ad events.
		interstitial.AdLoaded += HandleInterstitialLoaded;
		interstitial.AdFailedToLoad += HandleInterstitialFailedToLoad;
		interstitial.AdOpened += HandleInterstitialOpened;
		interstitial.AdClosing += HandleInterstitialClosing;
		interstitial.AdClosed += HandleInterstitialClosed;
		interstitial.AdLeftApplication += HandleInterstitialLeftApplication;
		// Load an interstitial ad.
		interstitial.LoadAd(createAdRequest());
	}
	public void HandleInterstitialLoaded(object sender, EventArgs args)
	{
		print("HandleInterstitialLoaded event received.");
	}
	
	public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		print("HandleInterstitialFailedToLoad event received with message: " + args.Message);
	}
	
	public void HandleInterstitialOpened(object sender, EventArgs args)
	{
		print("HandleInterstitialOpened event received");
	}
	
	void HandleInterstitialClosing(object sender, EventArgs args)
	{
		print("HandleInterstitialClosing event received");
	}
	
	public void HandleInterstitialClosed(object sender, EventArgs args)
	{
		print("HandleInterstitialClosed event received");
	}
	
	public void HandleInterstitialLeftApplication(object sender, EventArgs args)
	{
		print("HandleInterstitialLeftApplication event received");
	}

	public void ShowInterstitial()
	{
#if !UNITY_EDITOR
		if (interstitial.IsLoaded())
		{
			//print (time+" "+interval+" "+Game.status);
			if (time > interval) {
				interstitial.Show();
				time = 0;
			}	
		}
		else
		{
			print("Interstitial is not ready yet.");
		}
#endif
	}
}
