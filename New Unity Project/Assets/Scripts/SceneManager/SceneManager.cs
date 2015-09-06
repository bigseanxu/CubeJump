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

	public Transform [] sceneGenerators;
	public Transform background;
	public Transform lights;

	public SceneType sceneType;

	SceneType currSceneType = (SceneType) 0;
	// Use this for initialization
	void Start () {
		int type = Random.Range (0, sceneGenerators.Length);
		while (type == (int)currSceneType) {
			type = Random.Range (0, sceneGenerators.Length);
		}

		currSceneType = (SceneType) type;
		background.GetComponent<Background> ().SetBackground (type);
		lights.GetComponent<SceneLights> ().SetLights (type);
		scenes [type].gameObject.SetActive (true);
		Game.sceneType = type;
		sceneType = (SceneType)type;
	}
	
	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR
		if (currSceneType != sceneType) {
			ChangeScene(sceneType);
		}
#endif
	}

	public void ChangeScene(SceneType type) {
		scenes [(int)currSceneType].gameObject.SetActive (false);
		currSceneType = type;
		Game.sceneType = (int)currSceneType;
		scenes [(int)type].gameObject.SetActive (true);
		background.GetComponent<Background> ().SetBackground ((int)type);
		lights.GetComponent<SceneLights> ().SetLights ((int)type);
		Generate ();
	}

	public void Generate() {
		print ("generate " + currSceneType);
		sceneGenerators [(int)currSceneType].GetComponent<BaseGenerator> () .StartGenerate ();
	}
}
