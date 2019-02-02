using UnityEngine;
using System.Collections;

public class Start_GM : MonoBehaviour {
	public GameObject _logo;
	public GameObject _b_start;
	public GameObject _l_Start;
	public GameObject _l_copyright;
	float height=0;
	float width=0;
	void GameStart()
	{
		Application.LoadLevel (1);
	}
	void Start(){
		height = Screen.height;
		width = Screen.width;
		_logo.transform.localPosition = new Vector3 (0,(height / 2) - 200,0);
		_b_start.transform.localPosition = new Vector3 (0,((height / 2) - 200+-1* (height / 2)+50)/2,0);
		_l_Start.transform.localPosition = new Vector3 (0,((height / 2) - 200+-1* (height / 2)+50)/2,0);
		_l_copyright.transform.localPosition =new Vector3 ( 0,-1* (height / 2)+50,0);
	}
}
