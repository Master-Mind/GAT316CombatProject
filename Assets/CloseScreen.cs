using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseScreen : MonoBehaviour
{
    private GameObject _myPan;
    public static GameObject ReturnTo;
    private bool Firft = true;
	// Use this for initialization
	void Start ()
	{
	    _myPan = transform.parent.gameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Firft)
        {
            Firft = false;
            _myPan.SetActive(false);
        }
    }

    public void Close()
    {
        GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(ReturnTo);
        _myPan.SetActive(false);
    }
}
