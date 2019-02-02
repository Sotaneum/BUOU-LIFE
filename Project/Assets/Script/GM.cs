using UnityEngine;
using System.Collections;

public class GM : MonoBehaviour {
    public GameObject[] _touch;
    public Transform CreateTransfrom;
    public Camera _camera;

    public int _hp=100;
    public int _score=0;
    public int _addPointStep = 50;
    public int _loseLifeStep = 20;
    public int _MaxPoint = 5;
    public int _Level = 0;
    public float _createSpeed = 1;
    public float _deleteSpeed = 5;

    public bool _CreateOn = false;
    public bool _GameStartOn = false;

    private int score_ = 1000;
    void Start()
    {
        Reset();
    }

    void Update()
    {
        /*
         *0 : 1000 p
         *1 : 2000 p
         *2 : 10000 p
         *3 : 20000 p
         *4 : 40000 p
         *5 : 80000 p
         *6 : 160000p
         *7 : 320000p
         */
        if(_score>=score_)
        {
            _Level++;
            score_ *= 2;
            UpgradeLevel();
        }
    }
    
    void GameStart()
    {
        _CreateOn = true;

    }

    void UpgradeLevel()
    {
        _addPointStep += _Level;
        _MaxPoint +=_Level/3;
        if (_hp < 90)
            _hp += 10;
        else
            _hp = 100;
        if(_deleteSpeed>1.5f)
            _deleteSpeed -= _Level * 0.15f;
        _createSpeed -= _Level * 0.15f;
    }

    void Reset()
    {
        _hp=100;
        _addPointStep = 50;
        _loseLifeStep = 20;
        _MaxPoint = 1;
        _Level = 0;
        _createSpeed = 1;
        _deleteSpeed = 5;
        _CreateOn = false;
        _GameStartOn = true;
        height = Screen.height;
        width = Screen.width;
        StartCoroutine(CreatePoint(_createSpeed));
    }
    void addPoint()
    {
        _score += _addPointStep;
    }

    void loseLife()
    {
        _hp -= _loseLifeStep;
        _Level = 0;
    }
    float height = 0;
    float width = 0;
    IEnumerator CreatePoint(float Step)
    {
        while (_GameStartOn)
        {
            while (_CreateOn == false) yield return new WaitForSeconds(0.1f) ;
            yield return new WaitForSeconds(Step);
            for (int i = 0; i < _MaxPoint; i++)
            {
                var touch = Instantiate(_touch[0], Vector3.zero, Quaternion.identity) as GameObject;
                touch.transform.parent = CreateTransfrom;
                touch.transform.localScale = new Vector3(100, 100, 1);
                touch.transform.localPosition = new Vector3( Random.Range(-(height / 2), (height / 2)),  Random.Range(-(height / 2), (height / 2)), 0);
                touch.GetComponent<TouchSystem>()._deleteSpeed = _deleteSpeed;
                touch.GetComponent<TouchSystem>().camera = _camera;
                touch.GetComponent<TouchSystem>().GM = gameObject;
                touch.GetComponent<TouchSystem>().Active = true;
               
            }
        }
        
    }
   

}
