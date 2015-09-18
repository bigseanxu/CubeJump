using UnityEngine;
using System.Collections;

public class CheckCanBuy : MonoBehaviour {

	public Transform shopping;
	// Use this for initialization
	void OnEnable () {
#if UNITY_EDITOR
		PlayerPrefs.SetInt("Diamonds",90);
#endif

		StartCoroutine(Checking ());
	}
	
	// Update is called once per frame
	IEnumerator Checking () {
		yield return new WaitForSeconds(0.1f);
		if(shopping.GetComponent<Shop>().CheckCanBuy()){
			transform.GetChild(0).gameObject.SetActive(false);
			transform.GetChild(1).gameObject.SetActive(true);
		}else{
			transform.GetChild(1).gameObject.SetActive(false);
			transform.GetChild(0).gameObject.SetActive(true);
		}
		StartCoroutine(Checking ());
	}
}
