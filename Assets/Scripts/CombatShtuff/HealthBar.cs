using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject objectToMonitor;
    private Health _healthComp;
    private GameObject _actualBar;
    private float _originalRight;
    private float _myWidth;
    public bool HideWhenFull;
    // Use this for initialization
    void Start ()
    {
        _healthComp = objectToMonitor.GetComponent<Health>();
        _actualBar = transform.GetChild(0).gameObject;
        _originalRight = _actualBar.GetComponent<RectTransform>().anchorMax.x;
        _myWidth = GetComponent<RectTransform>().rect.width;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (HideWhenFull && Mathf.Abs(_healthComp.GetHealth() - _healthComp.MaxHealth) < 0.01f)
	    {
	        _actualBar.GetComponent<CanvasRenderer>().SetAlpha(0);
            GetComponent<CanvasRenderer>().SetAlpha(0);

        }
	    else
	    {
	        _actualBar.GetComponent<CanvasRenderer>().SetAlpha(1);
	        GetComponent<CanvasRenderer>().SetAlpha(1);
            _actualBar.GetComponent<Image>().fillAmount = _healthComp.GetHealth() / _healthComp.MaxHealth;
        }

    }
}
