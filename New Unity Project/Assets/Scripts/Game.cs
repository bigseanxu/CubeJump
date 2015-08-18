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
}

