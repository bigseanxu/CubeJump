using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {
	float offset;
	public Texture [] numbers;
	int num = 0;
	// Use this for initialization
	void Start () {
		num = 0;
		offset = 0;
		GetComponent<Image> ().material.SetFloat ("_Offset", 1);
		GetComponent<Image> ().material.SetTexture ("_MainTex2", numbers[0]);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void StartCountDown () {
		StartCoroutine (CoStartCountDown());
	}

	IEnumerator CoStartCountDown() {
		LeanTween.value (gameObject, updateNewValue, -1, 1, 1).setIgnoreTimeScale(true).setOnComplete(callback1);
		yield return null;
	}

	public void OnContinue() {
		GetComponent<QuitShop> ().OnContinue ();
	}

	void callback1() {
		LeanTween.value (gameObject, updateNewValue, -1, 1, 1).setIgnoreTimeScale(true).setOnComplete(callback2);
		GetComponent<Image> ().material.SetTexture ("_MainTex2", numbers[++num]);
	}

	void callback2() {
		LeanTween.value (gameObject, updateNewValue, -1, 1, 1).setIgnoreTimeScale(true).setOnComplete(OnContinue);
		GetComponent<Image> ().material.SetTexture ("_MainTex2", numbers[++num]);
	}

	private void updateNewValue (float newValue) {
		//print (newValue);
		GetComponent<Image> ().material.SetFloat ("_Offset", newValue);
	}
}
