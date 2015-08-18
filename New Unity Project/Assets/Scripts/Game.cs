using UnityEngine;
using System.Collections;

public class Game
{
	public enum State {
		BeforeGame,
		Gaming,
		GameOver
	};

	public static State state = State.BeforeGame;
	public static bool isStateChanged = false;
	public static GameDirector gameDirector = null;

	public static void SetState(State s) {
		if (state != s) {
			state = s;
			isStateChanged = true;
		}
	}
}

