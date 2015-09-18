using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.SocialPlatforms;
using System.Runtime.InteropServices;
using Umeng;
#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi;
#endif
public class GameCenter : MonoBehaviour {

	public string score_ID="Point";
	public string best="bestScore";
	public string appKey="55f24f2d67e58e4b880030bb";
	public string channelId="App Store";
	public string url="https://itunes.apple.com/cn/app/wind-dodge/id1031859249?mt=8";
	public string twitter="I scored {0} points. Can you beat me? #Cube's Adventure";
	#if UNITY_ANDROID
	bool isPlayGamesPlayformActivate = false;
	#endif
	// Use this for initialization
	private enum ShareType{
		Facebook,
		Twitter,
		Weibo
	};
	private ShareType type;
	#if UNITY_ANDROID
	private AndroidJavaObject m_activity; 
	#endif
	// Use this for initialization

	void Start () {
		#if UNITY_ANDROID
		if (!isPlayGamesPlayformActivate) {
			isPlayGamesPlayformActivate = true;
			PlayGamesPlatform.Activate();
			Debug.Log("PlayGamesPlatform.Activate()");
		}
		#endif
#if !UNITY_EDITOR
		Social.localUser.Authenticate (ProcessAuthentication);

		GA.StartWithAppKeyAndChannelId (appKey, channelId);
#endif
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[ DllImport("__Internal")]
	private static extern void _ReportAchievement( string achievementID, float progress );
	[ DllImport( "__Internal" )]
	private static extern int shareWithFacebook ( string title, string msg);
	[ DllImport( "__Internal" )]
	private static extern int shareWithTwitter ( string title, string msg);
	// This function gets called when Authenticate completes
	// Note that if the operation is successful, Social.localUser will contain data from the server. 
	public void ProcessAuthentication (bool success) {
		Debug.Log ("mr.x on ProcessAuthentication");
		if (success) {
			Debug.Log ("Authenticated, checking achievements");
			
			// Request loaded achievements, and register a callback for processing them
			Social.LoadAchievements (ProcessLoadedAchievements);
			Social.LoadAchievementDescriptions (descriptions => {
				if (descriptions.Length > 0) {
					Debug.Log ("Got " + descriptions.Length + " achievement descriptions");
					string achievementDescriptions = "Achievement Descriptions:\n";
					foreach (IAchievementDescription ad in descriptions) {
						achievementDescriptions += "\t" +
							ad.id + " " +
								ad.title + " " +
								ad.unachievedDescription + "\n";
					}
					Debug.Log (achievementDescriptions);
				}
				else
					Debug.Log ("Failed to load achievement descriptions");
			});
		}
		else
			Debug.Log ("Failed to authenticate");
	}	
	
	// This function gets called when the LoadAchievement call completes
	void ProcessLoadedAchievements (IAchievement[] achievements) {
		if (achievements.Length == 0)
			Debug.Log ("Error: no achievements found");
		else
			Debug.Log ("Got " + achievements.Length + " achievements");
		
		// You can also call into the functions like this
		/*Social.ReportProgress ("30smasher", 100.0f, result => {
			if (result)
				Debug.Log ("Successfully reported achievement progress");
			else
				Debug.Log ("Failed to report achievement");
		});*/
		ReportAchi();
	}

	public void OnFacebookShare() {
		//type = ShareType.Facebook;
		//shareWithFacebook ("title can be modified", "message can be modified");
		//CaptureAPicureAsync ();
		//Debug.Log ("facebook button is clicked.");
		int point = PlayerPrefs.GetInt (best);
		
		#if UNITY_IPHONE
		shareWithFacebook ("Facebook", ""+point);
		#elif UNITY_ANDROID
		using (AndroidJavaClass jc = new AndroidJavaClass ("com.unity3d.player.UnityPlayer")) {
			using (m_activity = jc.GetStatic<AndroidJavaObject> ("currentActivity")) {
				m_activity.Call("shareToFacebook", "Wind Dodge!  I scored " + point + " points. Can you beat me?");	
			}
		}
		#endif
	}
	
	public void OnTwitterShare() {
		//shareWithTwitter ("title can be modified", "message can be modified");
		//type = ShareType.Twitter;
		//CaptureAPicureAsync ();
		int point = PlayerPrefs.GetInt (best);
		#if UNITY_IPHONE
		shareWithTwitter ("Twitter", string.Format(twitter,point.ToString()));
		#elif UNITY_ANDROID
		using (AndroidJavaClass jc = new AndroidJavaClass ("com.unity3d.player.UnityPlayer")) {
			using (m_activity = jc.GetStatic<AndroidJavaObject> ("currentActivity")) {
				m_activity.Call("shareToTwitter", "Wind Dodge!  I scored " + point + " points. Can you beat me? #winddodge");	
			}
		}
		#endif
	}

	public void LoadLeaderboard() {
		#if UNITY_ANDROID
		PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkIt5fH8s4EEAIQBw");
		#elif UNITY_IPHONE
		Social.ShowLeaderboardUI ();

		ReportAchi();
		//Social.ShowAchievementsUI();
		#endif
	}
	public void LoadAchievements() {
		#if UNITY_ANDROID
		PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkIt5fH8s4EEAIQBw");
		#elif UNITY_IPHONE
		//Social.ShowLeaderboardUI ();
		Social.ShowAchievementsUI();
		#endif
		ReportAchi ();
	}
	
	public void ReportScore(long score) {
		string id=score_ID;
		#if UNITY_ANDROID
		id = "CgkIt5fH8s4EEAIQBw";
		#elif UNITY_IPHONE
		id = "Point";
		#endif
		Social.ReportScore (score, id, success => {
			Debug.Log(success ? "Reported score successfully" : "Failed to report score");
		});
	}
	public void ReportAchi(){

		if (true) {
			Social.ReportScore (PlayerPrefs.GetInt (best, 0), score_ID, success => {
				Debug.Log (success ? "Reported score successfully" : "Failed to report score");
			});
		}
		//print (PlayerPrefs.GetInt ("total", 0));
		//if里面要写上成就的上报条件
		//_ReportAchievement(id,100);

	}

	public void GiveScore(){
		Application.OpenURL (url);
	}
}
