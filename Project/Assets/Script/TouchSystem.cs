using UnityEngine;
using System.Collections;

public class TouchSystem : MonoBehaviour {
    public float _deleteSpeed = 0;
    public bool Active = false;
    public GameObject GM;
    public Camera camera;
    void Start()
    {
        StartCoroutine(deletePoint());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { // if left button pressed...
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject == gameObject)
                {
                    GM.SendMessage("addPoint");
                    Destroy(gameObject);
                }
            }
        }
    }


    IEnumerator deletePoint()
    {
        while (Active == false) yield return new WaitForSeconds(0.1f); 
        yield return new WaitForSeconds(_deleteSpeed);
        GM.SendMessage("loseLife");
        Destroy(gameObject);
    }
}
