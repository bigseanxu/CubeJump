using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Shop : MonoBehaviour {
	public Transform HeroesForShow;
	public Transform Hero;
	public Transform Heroes;
	public Transform[] cube;//预制物体
	public Transform name;
	public Transform diamond;
	public Transform Buy;
	public Transform BuyNoColor;
	public Transform Play;
	public Transform camera;
	public float itemDistance=0.3f;
	public float movingTime=0.17f;


	public Transform ShopPage;
	public Transform StartPage;

	public Transform gameScreen;
	public Transform ctrl;
	public Transform UIAudio;
	public Camera shopCam;
	public float velocity;
	bool moveLeft;
	Vector3 mVecPos;
	Vector3 mVecPosStart;
	//Transform[] cube;//使用预制物体创建的副本
	int itemID;//当前选中物品的ID
	int usingHero;//当前选中物品的ID
	int diamondCount;
	bool canTweening;//是否已经完成上一次移动
	string itemName;
	Vector3 mVecStart = new Vector3 (0, 0, 0);
	Vector3 mVec=new Vector3(3,3,3);   //小
	Vector3 mVec2=new Vector3(0,0,0);   //中
	Vector3 mVec3=new Vector3(7f,7f,7f);   //最大
	int a;
//	Color temp;
	bool flag ;
	public void Start () {

		diamondCount=PlayerPrefs.GetInt("Diamonds");
		//temp=new Color(Color.gray.r-0.3f,Color.gray.g-0.3f,Color.gray.b-0.3f);
		//itemID =Game.heroItemID;
	//	cube = new Transform[cubes.Length];   //初始化商品
		//Vector3 vec = camera.position;
		//vec.z+= 10;vec.y-= 0.2f;//测试数据
		//Quaternion qua = Quaternion.Euler(290,180,40);//这个不要改
		for (int i=0; i<cube.Length; i++) {
			//cube [i] =cubes [i];// (Transform)GameObject.Instantiate (cubes [i], vec, qua);
		//	cube [i].SetParent (HeroesForShow);
			cube [i].localScale = mVecStart;
			//cube [i].GetComponent<Renderer> ().material.color =temp;
		//	if (i < cube.Length - 1) {
		//		vec.x += itemDistance;
				FixColor (i);
		//	}
		}
		//OnLoad ();
		FixGameObject ();

		//Heroes = GameObject.Find ("Heroes").transform;
//			Heroes.GetComponent<HeroesHome> ().SetDic ();
		usingHero=PlayerPrefs.GetInt("usingHero",0);
		itemID = usingHero;
		if (usingHero != 0) {
			mVecPosStart=HeroesForShow.localPosition;
			mVecPosStart.x=-10f*usingHero;
			HeroesForShow.localPosition=mVecPosStart;
			mVecPos=mVecPosStart;
		}
		transform.parent.parent.gameObject.SetActive(false);
	}

	//要修改输入条件
	void Update () {
		if (Game.isShopLoaded) {
			OnLoad();
			Game.isShopLoaded=false;
		}

		Check ();
		if (velocity > 30) {
			//cube [itemID].GetComponent<Animator> ().enabled = false;
			velocity = velocity * 0.95f;
			if(moveLeft)
				mVecPos.x+=velocity/1000;
			else{
				mVecPos.x-=velocity/1000;
			}
			LeanTween.move (HeroesForShow.gameObject, transform.localToWorldMatrix.MultiplyPoint(mVecPos), 0.01f);
			Check ();
		} else {
			
			velocity=0;

			//cube [itemID].GetComponent<Animator> ().enabled = true;
			//cube [itemID].GetComponent<Animator> ().Play ("item");
		}
		//StartCoroutine (Check ());
		if (mVecPos.x>0) {
			mVecPos.x=0;
			LeanTween.move (HeroesForShow.gameObject, transform.localToWorldMatrix.MultiplyPoint(mVecPos), 0.5f).setEase(LeanTweenType.easeOutElastic);
			velocity=0;

		}
		if (mVecPos.x < -200) {
			mVecPos.x=-200;
			LeanTween.move (HeroesForShow.gameObject, transform.localToWorldMatrix.MultiplyPoint(mVecPos), 0.5f).setEase(LeanTweenType.easeOutElastic);
			velocity=0;

		}


	}

   public void OnLoad(){
		canTweening = false;
		cube [itemID].GetComponent<Animator> ().enabled = false;
		for (int i=0; i<cube.Length; i++) {
			cube[i].localScale=mVec2;
			if(itemID==i)
				LeanTween.scale(cube[itemID].gameObject,mVec3,movingTime*6).setOnComplete(FixGameObject);

			if(itemID>i)
				LeanTween.scale(cube[itemID-(i+1)].gameObject,mVec,movingTime*6);
			if(cube.Length>itemID+(i+1))
				LeanTween.scale(cube[itemID+(i+1)].gameObject,mVec,movingTime*6);


		}

	}

	public void OnSwipe(SwipeGesture gesture)
	{
		if (canTweening) {
			// 完整的滑动数据
			//Vector2 move = gesture.Move;
			// 滑动的速度
			velocity = gesture.Velocity;
			// 大概的滑动方向
			FingerGestures.SwipeDirection direction = gesture.Direction;
			if (direction.ToString () == "Right") {
				moveLeft=true;
			}
			if (direction.ToString () == "Left") {
				moveLeft=false;
			}
		}



	}

	void OnDrag( DragGesture gesture ) 
	{
		
		// 当前识别器阶段 (Started/Updated/Ended)
		ContinuousGesturePhase phase = gesture.Phase;
		
		// 最后一帧的拖拽/移动数据
		Vector2 deltaMove = gesture.DeltaMove;
		///if (phase == ContinuousGesturePhase.Updated) {
	//		velocity = 0;
		//}
		
		//完整的拖拽数据
		//Vector2 totalMove = gesture.TotalMove;
		mVecPos.x += deltaMove.x*0.1f;
		//Vector3 vec = mVecStart.x;//*0.1f;
		LeanTween.move (HeroesForShow.gameObject, transform.localToWorldMatrix.MultiplyPoint(mVecPos), 0.01f);
		//HeroesForShow.transform.localPosition=mVecPos;



	}

	void Check(){
		a = itemID;
		for (int i=0; i<cube.Length; i++) {
			mVecStart= shopCam.WorldToScreenPoint(cube[i].position);
			//print(shopCam.WorldToScreenPoint(cube[1].position));
			if(mVecStart.x>=Screen.width*0.38f&&mVecStart.x<=Screen.width*0.62f&&itemID!=i){
				itemID=i;
				//print (itemID);
				
			}
			//ScaleItems();
		}
		if (a != itemID) {
			ScaleItems ();
		}
		Game.heroItemID = itemID;
		PlayerPrefs.SetInt ("heroItemID", itemID);
	}
	//缩放商品+移动商品,也可以分成2个方法,效果一样.
	void ScaleItems(){
		canTweening=false;
		for(int i=0;i<cube.Length;i++){
			if(itemID==i){

				cube [itemID].GetComponent<Animator> ().Rebind();
				cube [itemID].GetComponent<Animator> ().enabled = true;
				cube [itemID].GetComponent<Animator> ().Play ("item");
				cube [itemID].localScale=mVec3;
			}
			else{
				//LeanTween.scale(cube[i].gameObject,mVec,0.01f);
				cube [i].GetComponent<Animator> ().enabled = false;
				cube[i].localScale=mVec;
			}
			FixColor(i);
			FixGameObject();
		}

	}
	//对原始方法进行修补,重设是否购买,避免在tween的时候狂按方向键的bug
	void FixGameObject(){
		flag = cube [itemID].GetComponent<ShopItem> ().isbought;
		int price = cube [itemID].GetComponent<ShopItem> ().price;
		if (!flag) {
			Buy.gameObject.SetActive (Game.diamond < price ? false : true);
			BuyNoColor.gameObject.SetActive (Game.diamond < price ? true : false);
			Play.gameObject.SetActive (false);
			Buy.FindChild("Price").GetComponent<Text>().text= price.ToString ();
			BuyNoColor.FindChild("Price").GetComponent<Text>().text= price.ToString ();
		} else {
			Play.gameObject.SetActive(true);
			Buy.gameObject.SetActive(false);
			BuyNoColor.gameObject.SetActive(false);
		}
		//Buy.FindChild("Price").GetComponent<Text>().text= price.ToString ();
		//BuyNoColor.FindChild("Price").GetComponent<Text>().text= price.ToString ();
		name.GetComponent<Image> ().sprite = cube [itemID].GetComponent<ShopItem> ().nameSprite;
		name.GetComponent<Image> ().SetNativeSize ();
		PlayerPrefs.SetInt (itemName,flag ? 1 : 0);
		cube [itemID].GetComponent<ShopItem> ().check = true;
		canTweening = true;
		//cube [itemID].GetComponent<Animator> ().Rebind ();
		//cube [itemID].GetComponent<Animator> ().enabled = flag;
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

	public void SwitchHeroAudio(){
		string name=cube [itemID].GetComponent<ShopItem> ().name;
		switch (name) {
		case "Cube":
			UIAudio.GetComponent<AudioList> ().Cube.Play ();break;
		case "Alpaca":
			UIAudio.GetComponent<AudioList> ().Alpaca.Play ();break;
		case "Cartoon":
			UIAudio.GetComponent<AudioList> ().Cartoon.Play ();break;
		case "Chicken":
			UIAudio.GetComponent<AudioList> ().Chicken.Play ();break;
		case "Crab":
			UIAudio.GetComponent<AudioList> ().Crab.Play ();break;
		case "Deer":
			UIAudio.GetComponent<AudioList> ().Deer.Play ();break;
		case "Dinosaur":
			UIAudio.GetComponent<AudioList> ().Dinosaur.Play ();break;
		case "Dog":
			UIAudio.GetComponent<AudioList> ().Dog.Play ();break;
		case "Dragon":
			UIAudio.GetComponent<AudioList> ().Dragon.Play ();break;
		case "Duck":
			UIAudio.GetComponent<AudioList> ().Duck.Play ();break;
		case "Elephant":
			UIAudio.GetComponent<AudioList> ().Elephant.Play ();break;
		case "Fish":
			UIAudio.GetComponent<AudioList> ().Fish.Play ();break;
		case "Flamingos":
			UIAudio.GetComponent<AudioList> ().Flamingos.Play ();break;
		case "Giraffe":
			UIAudio.GetComponent<AudioList> ().Giraffe.Play ();break;
		case "Lion":
			UIAudio.GetComponent<AudioList> ().Lion.Play ();break;
		case "Mushroom":
			UIAudio.GetComponent<AudioList> ().Mushroom.Play ();break;
		case "Penguin":
			UIAudio.GetComponent<AudioList> ().Penguin.Play ();break;
		case "Snail":
			UIAudio.GetComponent<AudioList> ().Snail.Play ();break;
		case "Spider":
			UIAudio.GetComponent<AudioList> ().Spider.Play ();break;
		case "Unicorn":
			UIAudio.GetComponent<AudioList> ().Unicorn.Play ();break;
		case "Whale":
			UIAudio.GetComponent<AudioList> ().Whale.Play ();break;
		}

	}

	public bool CheckCanBuy(){
		for(int i=0;i<cube.Length;i++){
			if(!cube [i].GetComponent<ShopItem> ().isbought){
				if(Game.diamond>=cube [i].GetComponent<ShopItem> ().price){
					return true;
				}
			}
		}
		return false;
	}


	public void OnBuyBtnClick(){
		UIAudio.GetComponent<AudioList> ().NormalButton.Play ();
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
		SwitchHeroAudio ();

		if (Hero.childCount > 0) {
			for(int i=0;i<Hero.childCount;i++){
				Destroy(Hero.GetChild(i).gameObject);
			}
		}
		if(itemID==0){

			int a;
			do{
				a = Random.Range (1, cube.Length);
			}
			while(!cube[a].GetComponent<ShopItem>().isbought);
			itemID=a;
		}
		Heroes.GetComponent<HeroesHome> ().SetDic (itemID);
		
		HeroesHome.HeroName name = Heroes.GetComponent<HeroesHome> ().dic[cube[itemID].GetComponent<ShopItem>().name];
		PlayerPrefs.SetInt ("HeroName", (int)name);
		Heroes.GetComponent<HeroesHome> ().GetHero (name);
		gameScreen.GetComponent<Animator> ().Play ("GameAppear");
		ShopPage.GetComponent<Animator> ().Play ("shopOut");
		if (Game.state == Game.State.GameOver) {
			ctrl.GetComponent<GameCtrl> ().ReLoad ();
			Game.replay=true;
		} else {
			Game.state =Game.State.BeforeGame;
		}
		Game.isShopLoaded = false;
		usingHero = itemID;
		PlayerPrefs.SetInt ("usingHero", usingHero);
	}
}
