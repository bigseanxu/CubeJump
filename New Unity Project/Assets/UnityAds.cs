using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class UnityAds : MonoBehaviour {

	public string GameID = "68793";
	// Use this for initialization
	void Start () {
		Advertisement.Initialize (GameID);

	}
	
	// Update is called once per frame
	void Update () {
		ShowAds ();
	}

	public void ShowAds() {
		if(Advertisement.IsReady()){
			Advertisement.Show();
		}
	}
}
