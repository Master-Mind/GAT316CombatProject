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
	// Use this for initialization
	void Start ()
    {
        curHealth = MaxHealth;
        movyForDodgy = GetComponent<MovementController>();
        _mesh = GetComponent<MeshRenderer>();
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
	}

    public void DealDamage(float damage)
    {
        if(!movyForDodgy.isDodging())
        {
            _colorMod = 0;
            curHealth -= damage;
            MARKEDFORDEATH = curHealth <= 0;
        }
    }

    public float GetHealth()
    {
        return curHealth;
    }
}
