using UnityEngine;
using System.Collections;

public class PillarMaterial : MonoBehaviour {
	public Material [] pillarMaterials;
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
	}
}
