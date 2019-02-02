using UnityEngine;
using System.Collections;

public class Player_Ctrol : MonoBehaviour {
	public float _speed = 1f;
	public bool state=false;
	public int GetLove=0;
	public int GetDie=0;
	public bool On=false;
	public GameObject GM;
	bool change=false;
	// Use this for initialization
	float height=0;
	float width=0;
	bool pause=false;
	void Start(){
		Reset ();
	}

	void Pause(){
		if (pause)
			pause = false;
		else {
			pause = true;
		}
	}

	// Update is called once per frame
	void Update () {
		if (On && pause==false) {
			Vector2 touchDeltaPosition;
			float v=0f;
			if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				touchDeltaPosition = Input.GetTouch(0).deltaPosition;
				v= touchDeltaPosition.y;
			}
			else
			{
				v= Input.GetAxis ("Vertical");
			}
			if (v > 0f) {
				if (transform.localPosition.y < height - 60f)
					transform.Translate (0, _speed * Time.deltaTime, 0);
				v = 0f;
			}
			if (v < 0f) {
				if (transform.localPosition.y > 30f)
					transform.Translate (0, -1 * _speed * Time.deltaTime, 0);
				v = 0f;
			}
			if (state == true) {
				if (change == false) {
					change = true;
					GetComponent<Animation> ().Play ("1_damage");
				}
			} else {
				if (change == false) {
					change = true;
					GetComponent<Animation> ().Play ("0_idle");
				}
			}
			if (GetComponent<Animation> ().isPlaying == false) {
				GetComponent<Animation> ().Play ("0_idle");
			}
		}
	}
	
	public int _hp = 100;
	public GameObject _DamageEff;
	bool Piba=false;
	//public UIFilledSprite _GuageBarWidget;
	void OnTriggerEnter(Collider other)
	{
		if (On && pause==false) {
			if (other.gameObject.tag == "ball") {
				if (_hp >= 0)
					_hp -= 7;
				//_GuageBarWidget.fillAmount = _hp * 0.01f;
				GetComponent<AudioSource> ().Play ();
				change = false;
				state = true;
				var _Eff1 = Instantiate (_DamageEff, transform.localPosition, Quaternion.identity) as GameObject;
				_Eff1.transform.parent = transform;
				_Eff1.transform.localPosition = Vector3.zero;
				_Eff1.transform.localScale = new  Vector3 (1, 1, 1);
				GetDie++;
				StartCoroutine (WaitAndPrint (2000f));
				if (_hp <= 0) {
					On = false;
					GM.SendMessage ("GameOver");
					StartCoroutine (Fail (0.01f));
				}
			} else {
				if (_hp <= 100 && Piba==false)
					_hp += 3;
				GetLove++;
				GM.SendMessage ("AddLove");
				if(_hp >=100)
				{
					GM.SendMessage ("Piver");
					Piba=true;
					_hp=50;
				}
			}
		}
	}
	void UnlockPiba()
	{
		Piba = false;
	}
	void Reset()
	{
		height = Screen.height;
		width = Screen.width;
	 	_speed = 1f;
		state=false;
		GetLove=0;
		GetDie=0;
		On=true;
	}
	IEnumerator Fail(float waitTime)
	{
		while (transform.localPosition.y>-100F) {
			yield return new WaitForSeconds (waitTime);
			transform.Translate (0, -_speed * Time.deltaTime, 0);
		}

	}
	IEnumerator WaitAndPrint(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		change = false;
		state = false;
	}

}
