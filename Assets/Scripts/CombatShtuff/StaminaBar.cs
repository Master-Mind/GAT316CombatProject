using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBar : MonoBehaviour
{

    public GameObject objectToMonitor;
    private CombatController _combatComp;
    private GameObject _actualBar;
    private Vector3 originalScale;
    private Vector3 originalPos;
    // Use this for initialization
    void Start()
    {
        _combatComp = objectToMonitor.GetComponent<CombatController>();
        _actualBar = transform.GetChild(0).gameObject;
        originalScale = _actualBar.transform.localScale;
        originalPos = _actualBar.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        _actualBar.transform.localScale = new Vector3(originalScale.x * (_combatComp.stamina / _combatComp.stamina), originalScale.y);
        _actualBar.transform.localPosition = new Vector3(originalPos.x - originalScale.x / 2, originalPos.y);

    }
}
