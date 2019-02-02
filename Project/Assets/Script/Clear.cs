using UnityEngine;
using System.Collections;

public class Clear : MonoBehaviour {
	public float clearTime = 1f;
	// Use this for initialization
	void Start () {
		StartCoroutine (WaitAndPrint (clearTime));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	IEnumerator WaitAndPrint(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		Destroy (gameObject);
	}
}
