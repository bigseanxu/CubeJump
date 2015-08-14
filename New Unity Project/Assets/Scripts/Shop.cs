using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Shop : MonoBehaviour {
	public Transform[] cubes;//预制物体
	public Transform name;
	public Transform Buy;
	public Transform Play;
	public float itemDistance=13f;
	public float movingTime=0.5f;
	Transform[] cube;//使用预制物体创建的副本
	int itemID;//当前选中物品的ID
	bool canTweening;//是否已经完成上一次移动
	Vector3 mVec=new Vector3(40,40,40);   //小
	Vector3 mVec2=new Vector3(60,60,60);   //中
	Vector3 mVec3=new Vector3(120,120,120);   //最大
	
	void Start () {
		itemID = 0;
		cube = new Transform[cubes.Length];   //初始化商品
		Vector3 vec = transform.position;
		for (int i=0; i<cubes.Length; i++) {
			cube[i]=(Transform)GameObject.Instantiate(cubes[i],vec,Quaternion.identity);//还缺一个初始化角度的功能
			cube[i].SetParent(transform);
			cube[i].localScale=mVec;
			if(i<cubes.Length-1)
				vec.x+= itemDistance;
			if(cube[0]!=null)
				LeanTween.scale(cube[0].gameObject,mVec3,movingTime);
			if(cube[1]!=null)
				LeanTween.scale(cube[1].gameObject,mVec2,movingTime);
			FixGameObject();
		}
	}
	//要修改输入条件
	void Update () {
		if (Input.GetKeyUp ("right") && canTweening) {
			if(itemID<cube.Length-1){
				itemID++;
				ScaleItems(false);//对商品的大小做缩放
			}
		}
		if (Input.GetKeyUp ("left") && canTweening) {       
			if(itemID>0){
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
		}
		for(int i=0;i<cube.Length;i++){
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
		bool flag = cube [itemID].GetComponent<ShopItem> ().isbought;
		Buy.gameObject.SetActive(flag?false:true);
		Play.gameObject.SetActive(flag?true:false);
		name.GetComponent<Text> ().text = cube [itemID].GetComponent<ShopItem> ().name;
		canTweening = true;
	}

	public void OnBuyBtnClick(){
		if (canTweening) {
			int diamonds=PlayerPrefs.GetInt("Diamonds",100);
			int price=cube [itemID].GetComponent<ShopItem> ().price;
			if(diamonds>=price){
				PlayerPrefs.SetInt("Diamonds",diamonds-price);
				cube [itemID].GetComponent<ShopItem> ().isbought=true;
				FixGameObject();
				print ("购买成功");//测试
			}else
				print ("钱不够");
		}
	}

	public void OnPlayBtnClick(){
	}

	public void OnCloseBtnClick(){
	}

}
