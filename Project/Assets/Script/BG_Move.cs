using UnityEngine;
using System.Collections;

public class BG_Move : MonoBehaviour {
	public float _speed = -2.0f;
	public float _y=560f;
	bool pause=false;
	void Update () {
		if (pause == false) {
			transform.Translate (_speed * Time.deltaTime, 0, 0);
			if (transform.localPosition.x < -1280.0f) {
				transform.localPosition = new Vector3 (1280.0f, _y, 0);
			}
		}
	}
	void Pause(){
		if (pause)
			pause = false;
		else {
			pause = true;
		}
	}
}
