using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
    public bool _return=false;
    public bool _userControll=false;
    public float _speed = -2.0f;
    public float _y = 560f;

    void Update()
    {
        if (_userControll == true)
        {
            _return = false;
        }
        else
        {


            transform.Translate(_speed * Time.deltaTime, 0, 0);
            if (_return == true)
            {
                if (_speed < 0 && transform.localPosition.x < -Screen.width/2)
                {
                    transform.localPosition = new Vector3(Screen.width/2, _y, 0);
                }
                else if (_speed > 0 && transform.localPosition.x > -Screen.width/2)
                {
                    transform.localPosition = new Vector3(Screen.width/2, _y, 0);
                }
            }
            else
            {
                if (_speed < 0 && transform.localPosition.x < -Screen.width)
                {
                    Destroy(gameObject);
                }
                else if (_speed > 0 && transform.localPosition.x > -Screen.width)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
