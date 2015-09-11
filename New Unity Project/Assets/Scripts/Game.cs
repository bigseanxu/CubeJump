using UnityEngine;
using System.Collections;

public class Game
{
	public enum State {
		BeforeGame,
		Gaming,
		Pause,
		GameOver,
		Shopping
	};

	public static State state = State.BeforeGame;        // Game state
	public static bool isStateChanged = false;			 // 
	public static GameDirector gameDirector = null;		 // Game director to control the game state
	public static int score = 0;			
	public static int bestScore;	// Game score
	public static int diamond = 0;						 // Game diamond
	public static int heroName = 0;
	public static int heroItemID = 0;// Hero name of the cube
	public static int sceneType = 0;
	public static bool replay=false;
	public static string sHeroName = "";	
	public static bool isInit = false;
	public static bool isShopLoaded = false;

	public static void Init() {
		if (!isInit) {
			Application.targetFrameRate = 60;
			heroName = PlayerPrefs.GetInt ("HeroName", 1);
			heroItemID=PlayerPrefs.GetInt ("heroItemID", 0);
			diamond = PlayerPrefs.GetInt ("Diamonds", 0);
			bestScore = PlayerPrefs.GetInt ("bestScore", 0);
			isInit = true;
		}
	}

	public static void StartGame() {
		SetState (State.Gaming);
	}

	public static void SetState(State s) {
		if (state != s) {
			state = s;
			isStateChanged = true;
		}
	}

	public static void ResetState(){
		state = State.BeforeGame;   
		isStateChanged = false;	
		gameDirector = null;	
		//heroName = 0;	
		score = 0;	
		diamond = PlayerPrefs.GetInt ("Diamonds", 0);
		isInit = false;

	}

}

