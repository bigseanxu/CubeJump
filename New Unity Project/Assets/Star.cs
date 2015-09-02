using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour {

	public Vector2 circle = new Vector2(1f, 2f);
	public Vector2 lifeTime = new Vector2(5f, 10f);
	float rLifeTime;
	float time;
	// Use this for initialization
	void Start () {
		float rCircle = Random.Range (circle.x, circle.y);
		rLifeTime = Random.Range (lifeTime.x, lifeTime.y);

		LeanTween.alpha (gameObject, 1, rCircle).setLoopPingPong(-1).setOnComplete(TweenCallback).setOnCompleteOnRepeat(true);
		time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void TweenCallback() {
		if (Time.time - time > rLifeTime) {
			GameObject.Destroy(gameObject);
		}
	}
}
