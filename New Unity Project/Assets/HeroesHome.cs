using UnityEngine;
using System.Collections;

public class HeroesHome : MonoBehaviour {
	public enum HeroName {
		Cube,
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
	public Transform CubeHero;
	HeroName name;

	// Use this for initialization
	void Start () {
		name = (HeroName)Game.heroName;
		GetHero (name);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject GetHero(HeroName name) {
		GameObject heroPrefab;
		GameObject hero;

		switch (name) {
		case HeroName.Cube:
			heroPrefab = (GameObject) Resources.Load("Heroes/Cube");
			break;
		case HeroName.Alpaca:
			heroPrefab = (GameObject) Resources.Load("Heroes/Alpaca");
			break;
		case HeroName.Cartoon:
			heroPrefab = (GameObject) Resources.Load("Heroes/Cartoon");
			break;
		case HeroName.Chicken:
			heroPrefab = (GameObject) Resources.Load("Heroes/Chicken");
			break;
		case HeroName.Crab:
			heroPrefab = (GameObject) Resources.Load("Heroes/Crab");
			break;
		case HeroName.Deer:
			heroPrefab = (GameObject) Resources.Load("Heroes/Deer");
			break;
		case HeroName.Dinosaur:
			heroPrefab = (GameObject) Resources.Load("Heroes/Dinosaur");
			break;
		case HeroName.Dog:
			heroPrefab = (GameObject) Resources.Load("Heroes/Dog");
			break;
		case HeroName.Dragon:
			heroPrefab = (GameObject) Resources.Load("Heroes/Dragon");
			break;
		case HeroName.Duck:
			heroPrefab = (GameObject) Resources.Load("Heroes/Duck");
			break;
		case HeroName.Elephant:
			heroPrefab = (GameObject) Resources.Load("Heroes/Elephant");
			break;
		case HeroName.Fish:
			heroPrefab = (GameObject) Resources.Load("Particle/Fish");
			break;
		case HeroName.Flamingos:
			heroPrefab = (GameObject) Resources.Load("Heroes/Flamingos");
			break;
		case HeroName.Giraffe:
			heroPrefab = (GameObject) Resources.Load("Heroes/Giraffe");
			break;
		case HeroName.Lion:
			heroPrefab = (GameObject) Resources.Load("Heroes/Lion");
			break;
		case HeroName.Mushroom:
			heroPrefab = (GameObject) Resources.Load("Heroes/Mushroom");
			break;
		case HeroName.Penguin:
			heroPrefab = (GameObject) Resources.Load("Heroes/Penguin");
			break;
		case HeroName.Snail:
			heroPrefab = (GameObject) Resources.Load("Heroes/Snail");
			break;
		case HeroName.Spider:
			heroPrefab = (GameObject) Resources.Load("Heroes/Spider");
			break;
		case HeroName.Unicorn:
			heroPrefab = (GameObject) Resources.Load("Heroes/Unicorn");
			break;
		case HeroName.Whale:
			heroPrefab = (GameObject) Resources.Load("Heroes/Whale");
			break;
		default:
			heroPrefab = null;
			break;
		}

		hero = Instantiate (heroPrefab);
		hero.transform.SetParent (transform);
		hero.transform.localPosition = Vector3.zero;

		return hero;
	}
}
