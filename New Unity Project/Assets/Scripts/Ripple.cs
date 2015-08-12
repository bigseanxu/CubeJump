using UnityEngine;
using System.Collections;

public class Ripple : MonoBehaviour {
	Object [] ripples;
	float startTime = 0;
	bool isPlay;
	Material mat;
	float animationDuration = 0.7f;
	// Use this for initialization
	void Start () {
		ripples = Resources.LoadAll ("Ripple");
	//print (ripples.Length);
		mat = GetComponent<MeshRenderer> ().material;
	}
	
	// Update is called once per frame
	void Update () {
		if (isPlay) {
			//print ("curr time = " + Time.time + " starttime = " + startTime);
			int k = (int)((Time.time - startTime) / (animationDuration / (float) ripples.Length));
			if (k <= ripples.Length - 1) {
				mat.mainTexture = (Texture) ripples[k];
				print (k);
			} else {
				isPlay = false;
			}

		}
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.name == "CubeHero") {
			PlayAnimation();
		}
	}

	void PlayAnimation() {
		startTime = Time.time;
		isPlay = true;
	}
}
