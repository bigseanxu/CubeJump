using UnityEngine;
using System.Collections;

public class HeroesHome : MonoBehaviour {
	public enum HeroName {
		Alpaca,
		Cartoon,
		Chicken,
		Crab,
		Deer,
		Dinosaur,
		Dog,
		Dragon,
		Duck,
		Elephant,
		Fish,
		Flamingos,
		Giraffe,
		Lion,
		Mushroom,
		Penguin,
		Snail,
		Spider,
		Unicorn,
		Whale
	}

	// Use this for initialization
	void Start () {
		GetHero (HeroName.Alpaca);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject GetHero(HeroName name) {
		GameObject heroPrefab;
		GameObject hero;
		switch (name) {
		case HeroName.Alpaca:
			heroPrefab = (GameObject) Resources.Load("Particle/Alpaca");
			break;
		default:
			heroPrefab = null;
			break;
		}

		hero = Instantiate (heroPrefab);

		hero.transform.SetParent (transform);
		return hero;
	}
}
