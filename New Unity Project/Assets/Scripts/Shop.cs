using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Shop : MonoBehaviour {

	public Transform[] cubes;//预制物体
	public Transform name;
	public Transform shop;
	public Transform Buy;
	public Transform Play;
	public float itemDistance=13f;
	public float movingTime=0.5f;
	
	Transform[] cube;//使用预制物体创建的副本
	int itemID=0;//当前选中物品的ID
	bool canTweening=true;//是否已经完成上一次移动

	//缩放尺寸
	Vector3 mVec=new Vector3(40,40,40);//小
	Vector3 mVec2=new Vector3(60,60,60);//中
	Vector3 mVec3=new Vector3(120,120,120);//最大


	void Start () {
		itemID = 0;
		cube = new Transform[cubes.Length];
		//初始化商品
		Vector3 vec = transform.position;

		for (int i=0; i<cubes.Length; i++) {
			//还缺一个初始化角度的功能
			cube[i]=(Transform)GameObject.Instantiate(cubes[i],vec,Quaternion.identity);
			cube[i].SetParent(transform);
			cube[i].localScale=mVec;

			if(i<cubes.Length-1){
				vec.x+= itemDistance;
			}
			if(cube[0]!=null){
				LeanTween.scale(cube[0].gameObject,mVec3,movingTime);
			}
			if(cube[1]!=null){
				LeanTween.scale(cube[1].gameObject,mVec2,movingTime);

			}
			ResetCanTweening();
		}
	}
	



	void Update () {
		if (Input.GetKeyUp ("right") && canTweening ) {
			//判断是否有下一个商品,避免数组越界
			if(itemID<cube.Length-1)
			{
				itemID++;
				canTweening=false;
			}
			else
			{
				return;
			}

			//print(itemID);
			//对商品的大小做缩放
			for(int i=0;i<cube.Length;i++){
				if(itemID==i){
					LeanTween.scale(cube[itemID].gameObject,mVec3,movingTime).setOnComplete(ResetCanTweening);

				}
				if(itemID>0){
					LeanTween.scale(cube[itemID-1].gameObject,mVec2,movingTime);

				}
				if(cube.Length>itemID+1){
					LeanTween.scale(cube[itemID+1].gameObject,mVec2,movingTime);

				}
				if(itemID>1){
					LeanTween.scale(cube[itemID-2].gameObject,mVec,movingTime);
				
				}
				if(cube.Length>itemID+2){
					LeanTween.scale(cube[itemID+2].gameObject,mVec,movingTime);

				}
			}
			//切换商品后,要做到所有商品移动.这里是异步执行
			for(int i=0;i<cube.Length;i++){
				Vector3 vec=cube[i].position;
				vec.x-=itemDistance;
				LeanTween.move(cube[i].gameObject,vec,movingTime);
				}
		}


		//这里和上面一样,只是方向反过来
		if (Input.GetKeyUp ("left") && canTweening  ) {

			if(itemID>0)
			{
				itemID--;
				canTweening=false;
			}
			else{
				return;
			}

			for(int i=0;i<cube.Length;i++){
				if(itemID==i){
					LeanTween.scale(cube[itemID].gameObject,mVec3,movingTime).setOnComplete(ResetCanTweening);
		
				}
				if(itemID>0){
					LeanTween.scale(cube[itemID-1].gameObject,mVec2,movingTime);
				
				}
				if(cube.Length>itemID+1){
					LeanTween.scale(cube[itemID+1].gameObject,mVec2,movingTime);
				
				}
				if(itemID>1){
					LeanTween.scale(cube[itemID-2].gameObject,mVec,movingTime);
				
				}
				if(cube.Length>itemID+2){
					LeanTween.scale(cube[itemID+2].gameObject,mVec,movingTime);

				}
			}

			for(int i=0;i<cube.Length;i++){
				Vector3 vec=cube[i].position;
				vec.x+=itemDistance;
				LeanTween.move(cube[i].gameObject,vec,movingTime);
				}
			


		}

	}

	void Shake(){


	}


	void ResetCanTweening(){
		bool flag = cube [itemID].GetComponent<ShopItem> ().isbought;
		if(flag){
			Buy.gameObject.SetActive(false);
			Play.gameObject.SetActive(true);
		}else{
			Buy.gameObject.SetActive(true);
			Play.gameObject.SetActive(false);
		}
		name.GetComponent<Text> ().text = cube [itemID].GetComponent<ShopItem> ().name;
		canTweening = true;
	}

	//以下代码可以放在任何类中
	public void OnBuyBtnClick(){
		if (canTweening) {
			int diamonds=PlayerPrefs.GetInt("Diamonds",100);
			int price=cube [itemID].GetComponent<ShopItem> ().price;
			if(diamonds>=price){
				PlayerPrefs.SetInt("Diamonds",diamonds-price);
				cube [itemID].GetComponent<ShopItem> ().isbought=true;
				ResetCanTweening();
				print ("购买成功");
			}else{
				print ("钱不够");
			}
		}
	}

	public void OnPlayBtnClick(){
		//开始游戏
	}

	public void OnCloseBtnClick(){
		//关闭商店界面
	}

}
