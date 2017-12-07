using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutText : MonoBehaviour
{
    private Vector2 _startPos;
    private Vector2 _targPos;
    public Vector2 EndPos;

    private bool _isOut;
    private bool _goBack;

    private float _tim = 1;
    // Use this for initialization
    void Start ()
    {
        _startPos = GetComponent<RectTransform>().anchoredPosition;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    var dif = _targPos - GetComponent<RectTransform>().anchoredPosition;

	    if (dif.sqrMagnitude > 0.01f)
	    {
	        GetComponent<RectTransform>().anchoredPosition += dif * Time.deltaTime;

	    }
	}

    public void ComeOut()
    {
        _targPos = EndPos;
    }
    public void GoBack()
    {
        _targPos = _startPos;
    }
}
