using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject objectToMonitor;
    private Health _healthComp;
    private GameObject _actualBar;
    private Vector3 originalScale;
    private Vector3 originalPos;
    // Use this for initialization
    void Start ()
    {
        _healthComp = objectToMonitor.GetComponent<Health>();
        _actualBar = transform.GetChild(0).gameObject;
        originalScale = _actualBar.transform.localScale;
        originalPos = _actualBar.transform.localPosition;
    }
	
	// Update is called once per frame
	void Update ()
    {
        _actualBar.transform.localScale = new Vector3(originalScale.x * (_healthComp.GetHealth() /_healthComp.MaxHealth), originalScale.y);
        _actualBar.transform.localPosition = new Vector3(originalPos.x - originalScale.x / 2, originalPos.y);

    }
}
