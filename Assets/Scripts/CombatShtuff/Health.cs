using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float curHealth;
    public float MaxHealth;
    private bool MARKEDFORDEATH = false;
    private MovementController movyForDodgy;
    private MeshRenderer _mesh;
    private float _colorMod = 0;
    public float StaggerTime = 0.5f;
    private float _stageTim = 0;
    public float Poise = 10;
    public float PoiseRecovery = 1;
    private float _curPoise = 10;
    public bool IsStaggered;

    public bool CanStagger;
	// Use this for initialization
	void Start ()
	{
	    CanStagger = true;
        curHealth = MaxHealth;
        movyForDodgy = GetComponent<MovementController>();
        _mesh = GetComponent<MeshRenderer>();
	    _curPoise = Poise;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    _mesh.material.color = new Color(1, _colorMod, _colorMod);
	    if (_colorMod < 1)
	    {
	        _colorMod += Time.deltaTime;

	    }
        if (MARKEDFORDEATH)
        {
            Destroy(gameObject);
        }
	    if (Input.GetKeyDown(KeyCode.P))
	    {
	        curHealth += 10;
	    }
	    IsStaggered = _stageTim > 0;
        if (_curPoise < Poise)
        {
            _curPoise += Time.deltaTime * PoiseRecovery;

        }
        
        if (IsStaggered)
        {

            //for glitchy do transform.right
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, transform.eulerAngles.z) *
                                 Quaternion.AngleAxis(30 * Mathf.Sin(Mathf.PI * (_stageTim / StaggerTime)), Vector3.right);
            _stageTim -= Time.deltaTime;

	    }
        else
        {
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, transform.eulerAngles.z);
        }
	}

    public void DealDamage(float damage)
    {
        if(!movyForDodgy.isDodging() && damage > 0.01f)
        {
            _colorMod = 0;
            curHealth -= damage;
            MARKEDFORDEATH = curHealth <= 0;
            if (CanStagger)
            {
                _curPoise -= damage;
                if (_curPoise < 0)
                {
                    _curPoise = Poise;
                    GetComponent<CombatController>().Stagger();
                    _stageTim = StaggerTime;
                }
            }
        }
    }

    public float GetHealth()
    {
        return curHealth;
    }
}
