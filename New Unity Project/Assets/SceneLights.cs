using UnityEngine;
using System.Collections;

public class SceneLights : MonoBehaviour {
	public Transform [] lights;

	public void SetLights(int sceneType) {
		switch (sceneType)  {
		case 0:
		case 1:
			lights[0].GetComponent<Light>().intensity = 1.4f;
			lights[1].GetComponent<Light>().intensity = 1.5f;
			lights[2].GetComponent<Light>().intensity = 1.2f;
			lights[0].GetComponent<Light>().color = Color.white;
			lights[1].GetComponent<Light>().color = Color.white;
			lights[2].GetComponent<Light>().color = new Color(238.0f / 255f, 195f / 255f, 102f / 255f);
			break;
		case 2:
			lights[0].GetComponent<Light>().intensity = 1.8f;
			lights[1].GetComponent<Light>().intensity = 1.55f;
			lights[2].GetComponent<Light>().intensity = 1.2f;
			lights[0].GetComponent<Light>().color = Color.white;
			lights[1].GetComponent<Light>().color = Color.white;
			lights[2].GetComponent<Light>().color = new Color(248.0f / 255f, 129f / 255f, 120f / 255f);
			break;
		case 3:
			lights[0].GetComponent<Light>().intensity = 1.7f;
			lights[1].GetComponent<Light>().intensity = 1.55f;
			lights[2].GetComponent<Light>().intensity = 1.2f;
			lights[0].GetComponent<Light>().color = Color.white;
			lights[1].GetComponent<Light>().color = Color.white;
			lights[2].GetComponent<Light>().color = new Color(140.0f / 255f, 197f / 255f, 253f / 255f);
			break;
		case 4:
			lights[0].GetComponent<Light>().intensity = 1.5f;
			lights[1].GetComponent<Light>().intensity = 1.5f;
			lights[2].GetComponent<Light>().intensity = 1.2f;
			lights[0].GetComponent<Light>().color = new Color(184.0f / 255f, 217f / 255f, 255f / 255f);
			lights[1].GetComponent<Light>().color = new Color(135.0f / 255f, 181f / 255f, 255f / 255f);
			lights[2].GetComponent<Light>().color = new Color(255.0f / 255f, 66f / 255f, 78f / 255f);
			break;
		case 5:
			lights[0].GetComponent<Light>().intensity = 1.4f;
			lights[1].GetComponent<Light>().intensity = 1.3f;
			lights[2].GetComponent<Light>().intensity = 0.4f;
			lights[0].GetComponent<Light>().color = Color.white;
			lights[1].GetComponent<Light>().color = Color.white;
			lights[2].GetComponent<Light>().color = Color.white;
			break;
		};
	}
}
