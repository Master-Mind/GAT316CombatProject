using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float curHealth;
    public float MaxHealth;
    private bool MARKEDFORDEATH = false;
	// Use this for initialization
	void Start ()
    {
        curHealth = MaxHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(MARKEDFORDEATH)
        {
            Destroy(gameObject);
        }
	}

    public void DealDamage(float damage)
    {
        curHealth -= damage;
        MARKEDFORDEATH = curHealth <= 0;
    }

    public float GetHealth()
    {
        return curHealth;
    }
}
