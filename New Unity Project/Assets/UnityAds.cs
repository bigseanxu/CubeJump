using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class UnityAds : MonoBehaviour {

	public string GameID = "68793";
	public int gameCountToShowAds = 5;
	public int diamondGain = 20;
	public int timeToShowAdsNext = 2 * 60 * 60;

	// Use this for initialization
	void Start () {
		Advertisement.Initialize (GameID);
	}
	
	// Update is called once per frame
	void Update () {

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
