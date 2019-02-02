using UnityEngine;
using System.Collections;

public class PLAY_GM : MonoBehaviour {
	public GameObject[] _ball;
	public GameObject _CreateBall;
	public GameObject _playerObj;
	public int _badBall;
	public bool _SpawnChk = true;
	public float GenTime=0.5f;
	public bool _createball;
	public GameObject _ScoreBox;
	public GameObject _Stop;
	public GameObject _pibox;
	public GameObject _piboxGage;
	public GameObject _LScore;
	public GameObject _GameOverObj;
	public GameObject _BackgroundObj;
	public GameObject _GamePauseObj;
	public int Score=0;
	//public UILabel _ScoreText;
	float height=0;
	float width=0;
	bool pauseMode=false;
	bool PiverMode=false;
	void Start(){
		Restart ();
	}
	void Restart()
	{
		height = Screen.height;
		width = Screen.width;
		_CreateBall.transform.position = new Vector3 (width/height, 0, 0);
		_playerObj.transform.localPosition = new Vector3 (-width/4,(height/2),0);
		_createball = true;

		_ScoreBox.transform.localPosition = new Vector3 (-1*(width/2)+100,height/2-35,0);
		_Stop.transform.localPosition = new Vector3 (1*(width/2)-35,-height/2+35,0);
		_pibox.transform.localPosition = new Vector3 (-1*(width/2)+20,-height/2+250,0);
		_piboxGage.transform.localPosition = new Vector3 (-1*(width/2)+20,-height/2+250+21,0);
		_LScore.transform.localPosition = new Vector3 (-1*(width/2)+100,height/2-35-15,0);
		_piboxGage.GetComponent<UIFilledSprite> ().fillAmount = 0.5f;

		_playerObj.SendMessage ("Reset");

		StartCoroutine(WaitAndPrint(GenTime));
		StartCoroutine (WaitTime (0.5f));
	}

	void Update(){
		if (_createball==true && pauseMode==false) {
			_LScore.GetComponent<UILabel> ().text = Score.ToString ("0");
			_piboxGage.GetComponent<UIFilledSprite> ().fillAmount = _playerObj.GetComponent<Player_Ctrol> ()._hp / 100f;
		}
	}

	void Piver(){
		PiverMode = true;
		StartCoroutine(PiverWait (10f));
		StartCoroutine(PiverModeActive (0f));
	}
	IEnumerator PiverModeActive(float waitTime)
	{
		yield return new WaitForSeconds (waitTime);
		while (PiverMode) {
			yield return new WaitForSeconds(0.1f);
			GameObject[] balls = GameObject.FindGameObjectsWithTag ("ball");
			for (int i=0; i<balls.Length; i++) {
				if(balls[i].transform.parent.gameObject.tag!="ball")
				{
					var Set1 = Instantiate (_ball[1], Vector3.zero, Quaternion.identity) as GameObject;
					Set1.transform.parent = _CreateBall.transform;
					Set1.transform.localScale = new Vector3 (1, 1, 1);
					Set1.transform.localPosition =balls[i].transform.localPosition;
					Destroy(balls[i].gameObject);
				}
			}
	
		}
	}
	IEnumerator PiverWait(float waitTime)
	{
		yield return new WaitForSeconds (waitTime);
		PiverMode = false;
		yield return new WaitForSeconds (3f);
		_playerObj.SendMessage ("UnlockPiba");
	}

	void Regame()
	{
		Application.LoadLevel (1);
	}
	void Out(){
		Application.LoadLevel (0);
	}
	void AddLove()
	{
		Score += 25;
	}

	void GameOver()
	{
		_createball = false;
		_piboxGage.GetComponent<UIFilledSprite> ().fillAmount = 0;
		GetComponent<AudioSource>().Stop();
		_GameOverObj.transform.FindChild ("Score").GetComponent<UILabel> ().text = "Score : " + Score.ToString("0");
		_GameOverObj.SetActive (true);
	}

	void pause(){
		if (_createball) {
			if (pauseMode) {
				pauseMode = false;
				GetComponent<AudioSource> ().Play ();
				_GamePauseObj.SetActive(false);
			} else {
				pauseMode = true;
				GetComponent<AudioSource> ().Stop ();
				_GamePauseObj.SetActive(true);
			}
			GameObject[] balls = GameObject.FindGameObjectsWithTag ("ball");
			for (int i=0; i<balls.Length; i++) {
				balls [i].SendMessage ("Pause");
			}
			GameObject[] items = GameObject.FindGameObjectsWithTag ("item");
			for (int i=0; i<items.Length; i++) {
				items [i].SendMessage ("Pause");
			}
			_playerObj.SendMessage ("Pause");
			_BackgroundObj.transform.FindChild ("BG_Far").SendMessage ("Pause");
			_BackgroundObj.transform.FindChild ("BG_Near").SendMessage ("Pause");
		}
	}

	IEnumerator WaitTime(float waitTime)
	{	
		while (_createball) {
			yield return new WaitForSeconds (waitTime);
			Score += 1;
			while(pauseMode)yield return new WaitForSeconds (waitTime);
		}
	}

	IEnumerator WaitAndPrint(float waitTime)
	{
		int count=0;
		while (_createball) {
			yield return new WaitForSeconds (waitTime);
			for(int i=0;i<_ball.Length;i++)
			{
				while(pauseMode)yield return new WaitForSeconds (waitTime);
				if(i<=_badBall || (i>_badBall && count>1))
				{
					count++;
					var Set1 = Instantiate (_ball[i], Vector3.zero, Quaternion.identity) as GameObject;
					Set1.transform.parent = _CreateBall.transform;
					Set1.transform.localScale = new Vector3 (1, 1, 1);
					Set1.transform.localPosition =new Vector3(0,Random.Range(-(height/2),(height/2)),0);
					if(i>_badBall)
					{
						count=0;
					}
				}
			}
		}
	}

}
/*
using UnityEngine;
using System.Collections;

public class GM : MonoBehaviour {

	public GameObject _enemySet;
	public GameObject _nearBgObj;
	public Transform _PlayerObjPool;
	public bool _SpawnChk = true;
	public UILabel _ScoreText;
	void Update(){
		_ScoreText.text = (Time.timeSinceLevelLoad * 100.0f).ToString ("0");
		if (_nearBgObj.transform.localPosition.x < -2460.0f && _SpawnChk) {
			var Set1 = Instantiate (_enemySet, Vector3.zero, Quaternion.identity) as GameObject;
			Set1.transform.parent = _PlayerObjPool;
			Set1.transform.localScale = new Vector3 (1, 1, 1);
			Set1.transform.localPosition = Vector3.zero;
			_SpawnChk=false;
		}
		if (_nearBgObj.transform.localPosition.x > -1300.0f && ! _SpawnChk) {
			_SpawnChk=true;
		}
	}
	void Regame()
	{
		Application.LoadLevel ("start");
	}
}

*/