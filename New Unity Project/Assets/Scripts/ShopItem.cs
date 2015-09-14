using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopItem : MonoBehaviour {

	public int id ;
	public Sprite nameSprite;
	public string name;
	public int price;
	public bool isbought;
	public bool isRandom;
	public Transform Lock;
	Transform l;
	public bool check;
	public Texture tLock;
	public bool fish;
	public bool cartoon;
	public Transform tfish;
	public Transform tcartoon;
	public HeroesHome.HeroName temp;
	Texture tHave;
	Material mat;
	Vector3 vec;
	Vector3 vecPos;

	void Start(){
		transform.localScale = new Vector3 (0, 0, 0);
		name = gameObject.name;
		isbought = PlayerPrefs.GetInt (name, 0) == 1 ? true : false;
		//PlayerPrefs.SetInt ("Fish", 0);
		LockGenerator ();
		Lock = transform.GetChild (0).transform;
		Lock.gameObject.SetActive(isbought?false:true);
	}

	void LockGenerator(){
		Lock = transform.GetChild (0).transform;
		mat=GetComponent<MeshRenderer>().material;
		if(fish){
			mat=tfish.GetComponent<SkinnedMeshRenderer>().material;
		}
		if(cartoon){
			mat=tcartoon.GetComponent<SkinnedMeshRenderer>().material;
		}
		tHave = mat.mainTexture;
		mat.mainTexture = isbought ? tHave : tLock;
	}

	void Update(){
		if (check) {
			Lock = transform.GetChild (0).transform;
			Lock.gameObject.SetActive(isbought?false:true);
			mat.mainTexture = isbought ? tHave : tLock;
			check=false;
		}
		Lock.localScale = transform.localScale/50;
		vec = transform.position;
		vec.z -= 1;
		Lock.position = vec;
		PlayerPrefs.SetInt (name, isbought ? 1 : 0);
	}


}
