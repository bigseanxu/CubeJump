using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShowNumberInCanvas : MonoBehaviour {
	public string NumberName="Number";
	public Sprite [] sprites = new Sprite[10];
	public int numberInsp = 0;
	int num;
	public Transform position;
	public float gap = 10;
	
	int numberCount;
	string numberString;

	List<GameObject> numberList = new List<GameObject>();

	public float spriteWidth = 207;
	public float spriteHeight = 294;

	public float defaultY = -85;

	// Use this for initialization
	void Awake() {
		num = numberInsp;
		numberString = num.ToString ();
		numberCount = numberString.Length;
		GenerateImageNumber (numberCount);
	}

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (numberInsp != num) {
			SetNumber(numberInsp);
		}
	}

	public void SetNumber (int n) {
		if (n != num) {
			num = n;
			numberInsp = num;
			numberString = num.ToString();
			numberCount = numberString.Length;
			
			if (numberCount > numberList.Count) {
				int m = numberCount - numberList.Count;
				GenerateImageNumber (m);
			} else if (numberCount < numberList.Count) {
				for (int i = numberCount - 1; i < numberList.Count; i++) {
					GameObject o = numberList[i];
					numberList.Remove(o);
					Destroy(o);
				}
			}
			NumberToImage ();
			Reorder ();
		}
	}

	void GenerateImageNumber(int n) {
		for (int i = 0; i < n; i++) {
			GameObject number = (GameObject) Instantiate ((GameObject) Resources.Load (NumberName), new Vector3 (), new Quaternion());
			number.transform.SetParent(position);

			// Fixme: after setparent to a canvas, anchored position and scale will change.
			Vector3 newPosition = new Vector3(0, defaultY, 0);
			number.GetComponent<RectTransform>().anchoredPosition3D = newPosition;
			Vector3 newScale = new Vector3(1, 1, 1);
			number.GetComponent<RectTransform>().localScale = newScale;

			numberList.Add(number);
		}
	}

	void NumberToImage() {
		int number = num;

		for (int i = numberCount - 1; i >= 0; i--) {
			int n = number % 10;
			GameObject gameObject = numberList[i];
			gameObject.GetComponent<Image>().sprite = sprites[n];
			number /= 10;
		}
	}

	void Reorder() {
		float stringWidth = spriteWidth * numberCount + gap * (numberCount - 1);
		for (int i = 0; i < numberCount; i++) {
			GameObject gameObject = numberList[i];
			Vector3 position = gameObject.GetComponent<RectTransform>().anchoredPosition3D;
			position.x = ((float)i - (numberCount - 1) / 2.0f) * (spriteWidth + gap);
			gameObject.GetComponent<RectTransform>().anchoredPosition3D = position;
		}
	}
}
