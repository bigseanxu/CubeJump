using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Shop : MonoBehaviour {

	public Transform Hero;
	public Transform Heroes;
	public Transform[] cubes;//预制物体
	public Transform name;
	public Transform diamond;
	public Transform Buy;
	public Transform Play;
	public Transform camera;
	public float itemDistance=0.3f;
	public float movingTime=0.17f;

	public Transform ShopPage;
	public Transform StartPage;

	public Transform gameScreen;
	public Transform ctrl;
	public Transform heroesForShow;

	Transform[] cube;//使用预制物体创建的副本
	int itemID;//当前选中物品的ID
	int diamondCount;
	bool canTweening;//是否已经完成上一次移动
	string itemName;
	Vector3 mVec=new Vector3(3,3,3);   //小
	Vector3 mVec2=new Vector3(4,4,4);   //中
	Vector3 mVec3=new Vector3(6f,6f,6f);   //最大
//	Color temp;
	bool flag ;
	void Start () {
		PlayerPrefs.SetInt("Diamonds",100);//测试用
		diamondCount=PlayerPrefs.GetInt("Diamonds");
		//temp=new Color(Color.gray.r-0.3f,Color.gray.g-0.3f,Color.gray.b-0.3f);
		itemID = 0;
		cube = new Transform[cubes.Length];   //初始化商品
		Vector3 vec = camera.position;
		vec.z+= 10;vec.y-= 0.2f;//测试数据
		Quaternion qua = Quaternion.Euler(290,180,40);//这个不要改
		for (int i=0; i<cubes.Length; i++) {
			cube[i]=(Transform)GameObject.Instantiate(cubes[i],vec,qua);
			cube[i].SetParent(heroesForShow);
			cube[i].localScale=mVec;
			//cube [i].GetComponent<Renderer> ().material.color =temp;
			if(i<cubes.Length-1){
				vec.x+= itemDistance;
			}
			if(i>1){
				LeanTween.scale(cube[0].gameObject,mVec3,movingTime);
				LeanTween.scale(cube[1].gameObject,mVec2,movingTime);
			}
			FixColor(i);
		}
		FixGameObject ();

		//Heroes = GameObject.Find ("Heroes").transform;
//			Heroes.GetComponent<HeroesHome> ().SetDic ();

	}
	//要修改输入条件
	void Update () {
		if (Input.GetKey ("right") && canTweening) {
			if(itemID<cube.Length-1){
				cube [itemID].GetComponent<Animator> ().enabled = false;
				Vector3 vec = camera.position;
				vec.z+= 10;vec.y-= 0.2f;
				cube [itemID].position=vec;
				itemID++;
				ScaleItems(false);//对商品的大小做缩放
			}
		}
		if (Input.GetKey ("left") && canTweening) {    
			if(itemID>0){
				cube [itemID].GetComponent<Animator> ().enabled = false;
				Vector3 vec = camera.position;
				vec.z+= 10;vec.y-= 0.2f;
				cube [itemID].position=vec;
				itemID--;
				ScaleItems(true);
			}
		}
	}
	//缩放商品+移动商品,也可以分成2个方法,效果一样.
	void ScaleItems(bool isLeft){
		canTweening=false;
		for(int i=0;i<cube.Length;i++){
			if(itemID==i)
				LeanTween.scale(cube[itemID].gameObject,mVec3,movingTime).setOnComplete(FixGameObject);
			if(itemID>0)
				LeanTween.scale(cube[itemID-1].gameObject,mVec2,movingTime);
			if(cube.Length>itemID+1)
				LeanTween.scale(cube[itemID+1].gameObject,mVec2,movingTime);
			if(itemID>1)
				LeanTween.scale(cube[itemID-2].gameObject,mVec,movingTime);
			if(cube.Length>itemID+2)
				LeanTween.scale(cube[itemID+2].gameObject,mVec,movingTime);
			FixColor(i);
			Vector3 vec=cube[i].position;
			if(isLeft)
				vec.x+=itemDistance;
			else
				vec.x-=itemDistance;
			LeanTween.move(cube[i].gameObject,vec,movingTime);
		}
	}
	//对原始方法进行修补,重设是否购买,避免在tween的时候狂按方向键的bug
	void FixGameObject(){
		flag = cube [itemID].GetComponent<ShopItem> ().isbought;
		int price = cube [itemID].GetComponent<ShopItem> ().price;
		Buy.gameObject.SetActive(flag?false:true);
		Play.gameObject.SetActive(flag?true:false);
		Buy.FindChild("Price").GetComponent<Text>().text= price.ToString ();
		itemName = cube [itemID].GetComponent<ShopItem> ().name;
		name.GetComponent<Text> ().text = itemName;
		PlayerPrefs.SetInt (itemName,flag ? 1 : 0);
		cube [itemID].GetComponent<ShopItem> ().check = true;
		canTweening = true;
		cube [itemID].GetComponent<Animator> ().Rebind ();
		cube [itemID].GetComponent<Animator> ().enabled = flag;
		if(flag)
		cube[itemID].GetComponent<Renderer>().material.color=Color.white;//这句话是为了更新购买后的缓存
	}

	void FixColor(int i){
		bool b=cube [i].GetComponent<ShopItem> ().isbought;
		if(itemID==i && b){
			cube[i].GetComponent<Renderer>().material.color=Color.white;
		}
		if(itemID==i && !b){
			cube[i].GetComponent<Renderer>().material.color=Color.grey;
		}
		if(itemID!=i && b){
			cube[i].GetComponent<Renderer>().material.color=Color.grey;
		}
		if(itemID!=i && !b){
			cube[i].GetComponent<Renderer>().material.color=Color.grey;
		}
	}

	public void OnBuyBtnClick(){
		if (canTweening) {
			int price=cube [itemID].GetComponent<ShopItem> ().price;
			if(diamondCount>=price){
				diamondCount-=price;
				PlayerPrefs.SetInt("Diamonds",diamondCount);
				Game.diamond=diamondCount;
				cube [itemID].GetComponent<ShopItem> ().isbought=true;
				FixGameObject();
				print ("购买成功");//测试
			}else
				print ("钱不够");
		}
	}

	public void OnPlayBtnClick(){
		/*
		if (cube [itemID].GetComponent<ShopItem> ().isRandom) {
			int a;
			do{
				a = Random.Range (0, cube.Length-1);
			}
			while(cube[a].GetComponent<ShopItem>().isbought);
			if(cube[a].GetComponent<ShopItem>().fish){
				Hero.gameObject.SetActive(false);
				tcartoon.gameObject.SetActive(false);
				tfish.gameObject.SetActive(true);
			}
			else if(cube[a].GetComponent<ShopItem>().cartoon){
				Hero.gameObject.SetActive(false);
				tcartoon.gameObject.SetActive(true);
				tfish.gameObject.SetActive(false);
			}else
			{
				tcartoon.gameObject.SetActive(false);
				tfish.gameObject.SetActive(false);
				Hero.GetChild (0).GetComponent<MeshFilter> ().mesh = cube [a].GetComponent<MeshFilter> ().mesh;
			}
		} else {
			if(cube[itemID].GetComponent<ShopItem>().isbought){
				if(cube[itemID].GetComponent<ShopItem>().fish){
					Hero.gameObject.SetActive(false);
					tcartoon.gameObject.SetActive(false);
					tfish.gameObject.SetActive(true);
				}
				else if(cube[itemID].GetComponent<ShopItem>().cartoon){
					Hero.gameObject.SetActive(false);
					tcartoon.gameObject.SetActive(true);
					tfish.gameObject.SetActive(false);
				}else{
					tcartoon.gameObject.SetActive(false);
					tfish.gameObject.SetActive(false);
					Hero.GetChild (0).GetComponent<MeshFilter> ().mesh = cube [itemID].GetComponent<MeshFilter> ().mesh;
				}
			}else{
				print("未购买");
				return;
			}

		}
		*/
		if (Hero.childCount > 0) {
			for(int i=0;i<Hero.childCount;i++){
				Destroy(Hero.GetChild(i).gameObject);
			}
		}		
		int a;
		do{
			a = Random.Range (1, cube.Length-1);
		}
		while(!cube[a].GetComponent<ShopItem>().isbought);
		Heroes.GetComponent<HeroesHome> ().SetDic (a);
		HeroesHome.HeroName name = Heroes.GetComponent<HeroesHome> ().dic[cube[itemID].GetComponent<ShopItem>().name];
		PlayerPrefs.SetInt ("HeroName", (int)name);
		Heroes.GetComponent<HeroesHome> ().GetHero (name);
		gameScreen.GetComponent<Animator> ().Play ("GameAppear");
		ShopPage.GetComponent<Animator> ().Play ("shopOut");
		if (Game.state != Game.State.BeforeGame) {
			ctrl.GetComponent<GameCtrl>().ReLoad();
		}
	}
}
