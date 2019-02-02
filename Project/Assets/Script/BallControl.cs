using UnityEngine;
using System.Collections;

public class BallControl : MonoBehaviour {
	public float _rspeed = 400f;
	public float _mspeed = 10f;
	float height=0;
	float width=0;
	bool pause=false;
	void Start(){
		//height=2*Camera.main.orthographicSize;
		//width = height*Camera.main.aspect;
		height = Screen.height;
		width = Screen.width;
	}
	void Update () {
		if (pause == false) {
			transform.Rotate (new Vector3 (0, 0, _rspeed * Time.deltaTime));
			transform.parent.transform.Translate (-1 * _mspeed * Time.deltaTime, 0, 0);
			if (transform.parent.transform.localPosition.x < -1 * width - 200) {
				Destroy (transform.parent.gameObject);
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
	void OnTriggerEnter(Collider other) {
	if (pause == false) {
			if (other.gameObject.tag == "Player") {
				this.GetComponent<AudioSource> ().Play ();
				StartCoroutine (WaitAndPrint (0.4f));
			}
		}
	}
	IEnumerator WaitAndPrint(float waitTime)
	{
		while (pause)yield return new WaitForSeconds (waitTime);
		yield return new WaitForSeconds (waitTime);
		Destroy (transform.parent.gameObject);
	}
}
