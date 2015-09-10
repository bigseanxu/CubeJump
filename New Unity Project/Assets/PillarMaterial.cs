using UnityEngine;
using System.Collections;

public class PillarMaterial : MonoBehaviour {
	public Material [] pillarMaterials;
	public Texture hellMaterial;
	public Texture spaceMaterial;
	public Transform logoUp;
	public Transform logoDown;
	// Use this for initialization
	void Start () {
//		Vector3 v = logoUp.localPosition;
//		v.z = 0.32f;
//		logoUp.localPosition = v;
//
//		Vector3 v2 = logoDown.localPosition;
//		v2.z = -0.32f;
//		logoDown.localPosition = v2;
	}
	
	// Update is called once per frame
	void Update () {
		MeshRenderer [] meshRenderers = GetComponentsInChildren<MeshRenderer> ();
		foreach (MeshRenderer m in meshRenderers) {
			if (m.gameObject.name.StartsWith("cube")) {
				m.material = pillarMaterials[Game.sceneType];
			}
		}
		
		if (Game.sceneType == (int)SceneManager.SceneType.Hell) {
			
			logoUp.GetComponent<MeshRenderer>().material.mainTexture = hellMaterial;
			logoDown.GetComponent<MeshRenderer>().material.mainTexture = hellMaterial;
		} else if (Game.sceneType == (int)SceneManager.SceneType.Space) {

			logoUp.GetComponent<MeshRenderer>().material.mainTexture = spaceMaterial;
			logoDown.GetComponent<MeshRenderer>().material.mainTexture = spaceMaterial;
		}

	}
}
