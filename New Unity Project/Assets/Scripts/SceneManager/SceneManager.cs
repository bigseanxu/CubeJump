using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {
	public enum SceneType {
		Water=0,
		Pond=1,
		Forest=2,
		Sky=3,
		Hell=4,
		Space=5
	};
	public Transform [] SceneGenerators;
	public Transform [] Scenes;

	SceneType currSceneType=SceneType.Hell;
	// Use this for initialization
	void Start () {
	//	int r = Random.Range (0, SceneGenerators.Length - 1);
	//	Scenes [r].gameObject.SetActive (true);
	//	ChangeScene((SceneType)r);

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
