using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameCam : MonoBehaviour {


	public Vector3 cameraTarget;
	private Transform target;
	public float cameraHeight=118;
	
	void Start()
	{

		target = GameObject.FindGameObjectWithTag("Player").transform;
		//target = GameObject.Find("Cube").transform;
	}
	
	
	void FixedUpdate()
	{
		//保持y方向不变，z，x方向变动

			cameraTarget = new Vector3 (target.position.x + 2, cameraHeight, target.position.x-2 );
		
		//使摄像机组件每秒钟向目标物体的位置移动
		transform.position = Vector3.Lerp(transform.position, cameraTarget, Time.deltaTime * 1);

	}
}
