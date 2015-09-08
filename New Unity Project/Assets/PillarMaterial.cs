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
