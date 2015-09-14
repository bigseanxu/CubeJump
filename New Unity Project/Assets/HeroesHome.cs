using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	public List<string> HeroNameString = new List<string> ();

	public Transform cubeHero;
	HeroName name;

	public Dictionary<string,HeroName> dic=new Dictionary<string, HeroName>();


	// Use this for initialization
	public void Start () {
		Game.Init ();
		//SetDic ();
		name = (HeroName)Game.heroName;
//		print ("heroname = " + name);
		GetHero (name);
	}

	public void SetDic(int a){
		if (dic.Keys.Count < 20) {
			dic.Add ("Random", (HeroName)a);
			dic.Add ("Alpaca", HeroName.Alpaca);
			dic.Add ("Cartoon", HeroName.Cartoon);
			dic.Add ("Chicken", HeroName.Chicken);
			dic.Add ("Crab", HeroName.Crab);
			dic.Add ("Deer", HeroName.Deer);
			dic.Add ("Dinosaur", HeroName.Dinosaur);
			dic.Add ("Dog", HeroName.Dog);
			dic.Add ("Dragon", HeroName.Dragon);
			dic.Add ("Duck", HeroName.Duck);
			dic.Add ("Elephant", HeroName.Elephant);
			dic.Add ("Fish", HeroName.Fish);
			dic.Add ("Flamingos", HeroName.Flamingos);
			dic.Add ("Giraffe", HeroName.Giraffe);
			dic.Add ("Lion", HeroName.Lion);
			dic.Add ("Mushroom", HeroName.Mushroom);
			dic.Add ("Penguin", HeroName.Penguin);
			dic.Add ("Snail", HeroName.Snail);
			dic.Add ("Spider", HeroName.Spider);
			dic.Add ("Unicorn", HeroName.Unicorn);
			dic.Add ("Whale", HeroName.Whale);
		}
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
			heroPrefab = (GameObject) Resources.Load("Heroes/Fish");
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
		hero.transform.SetParent (cubeHero);
		hero.transform.localRotation = Quaternion.identity;
		hero.transform.localPosition = Vector3.zero;

		return hero;
	}
}
