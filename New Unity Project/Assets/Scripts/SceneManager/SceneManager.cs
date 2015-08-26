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
	public Transform [] SceneGenerators;

	SceneType currSceneType = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangeScene(SceneType type) {
		currSceneType = type;
		Game.sceneType = (int)currSceneType;
	}

	public void Generate() {
		SceneGenerators [(int)currSceneType].GetComponent<BaseGenerator> () .StartGenerate ();
	}
}
