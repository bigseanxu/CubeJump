using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {
	public enum SceneType {
		Water,
		Pond,
		Forest,
		Sky,
		Hell,
		Space
	};

	public Transform[] scenes;

	public Transform [] SceneGenerators;
	public Transform background;
	public Transform lights;

	public SceneType sceneType;

	SceneType currSceneType = 0;
	// Use this for initialization
	void Start () {
		int type = Random.Range (0, 5);
		currSceneType = (SceneType) type;
		background.GetComponent<Background> ().SetBackground (type);
		lights.GetComponent<SceneLights> ().SetLights (type);
		scenes [type].gameObject.SetActive (true);
		Game.sceneType = type;
	}
	
	// Update is called once per frame
	void Update () {
//		if (currSceneType != sceneType) {
//			currSceneType = sceneType;
//			ChangeScene(currSceneType);
//		}
	}

	public void ChangeScene(SceneType type) {
		currSceneType = type;
		Game.sceneType = (int)currSceneType;
		background.GetComponent<Background> ().SetBackground ((int)type);
		lights.GetComponent<SceneLights> ().SetLights ((int)type);
	}

	public void Generate() {
		print ("generate");
		SceneGenerators [(int)currSceneType].GetComponent<BaseGenerator> () .StartGenerate ();
	}
}
