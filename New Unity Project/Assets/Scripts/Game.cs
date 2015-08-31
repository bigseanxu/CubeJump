using UnityEngine;
using System.Collections;

public class Game
{
	public enum State {
		BeforeGame,
		Gaming,
		GameOver
	};

	public static State state = State.BeforeGame;        // Game state
	public static bool isStateChanged = false;			 // 
	public static GameDirector gameDirector = null;		 // Game director to control the game state
	public static int score = 0;						 // Game score
	public static int diamond = 0;						 // Game diamond
	public static int heroName = 0;						 // Hero name of the cube
	public static int sceneType = 0;
	public static bool replay=false;
	public static string sHeroName = "";	

	public static void Init() {
		heroName = PlayerPrefs.GetInt ("HeroName", 0);
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
		heroName = 0;	
		score = 0;	
		diamond = 0;
	}
}

