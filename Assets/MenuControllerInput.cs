using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuControllerInput : MonoBehaviour
{
    private Button[] _butts;

    private int _curButt;
	// Use this for initialization
	void Start ()
	{
	    _butts = GetComponentsInChildren<Button>();

	}
	
	// Update is called once per frame
	void Update () {
	    //_butts[_curButt].OnPointerEnter(new PointerEventData());

    }
}
