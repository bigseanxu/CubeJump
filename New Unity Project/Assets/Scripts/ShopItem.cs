using UnityEngine;
using System.Collections;

public class ShopItem : MonoBehaviour {

	public int id ;
	public string name;
	public int price;
	public bool isbought;
	public bool isRandom;





	void Start(){
		name = gameObject.name;
		if(name.IndexOf('(')>0)
		name = name.Substring (0,gameObject.name.Length-7);
		isbought = PlayerPrefs.GetInt (name, 0) == 1 ? true : false;
	}

}
