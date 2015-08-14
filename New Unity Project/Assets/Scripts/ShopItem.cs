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



	}

}
