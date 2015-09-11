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
		if(name.IndexOf('(')>0)
		name = name.Substring (0,gameObject.name.Length-7);
		isbought = PlayerPrefs.GetInt (name, 0) == 1 ? true : false;
		//PlayerPrefs.SetInt ("Fish", 0);
		LockGenerator ();
	}

	void LockGenerator(){
		vec = transform.position;
		vec.y += 0.05f;
		vec.z -= 0.3f;
		Quaternion qua = Quaternion.Euler (90,180,0);//这里要调很久
		l=(Transform)GameObject.Instantiate(Lock,vec,qua);
		l.SetParent (transform);
		l.gameObject.SetActive(isbought?false:true);
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
			l.gameObject.SetActive(isbought?false:true);
			mat.mainTexture = isbought ? tHave : tLock;
			check=false;
		}
		l.localScale = transform.localScale/50;
		vec = transform.position;
		vec.z -= 1;
		l.position = vec;

	}

	public void OnLoad(){
		LeanTween.scale (gameObject, new Vector3 (3, 3, 3), 3f);
	}
}
