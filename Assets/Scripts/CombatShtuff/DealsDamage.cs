using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealsDamage : MonoBehaviour
{
    [SerializeField]
    private float DamageToDeal;
    private float _defaultDamage;
    private float _curDamage;
    [HideInInspector]
    public bool DealDamageNow;
    public float Damage
    {
        get
        {
            return _curDamage;
        }
        set
        {
            _curDamage = value;
        }
    }
	// Use this for initialization
	void Start ()
    {
        _defaultDamage = _curDamage = DamageToDeal;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    

    void OnCollisionEnter(Collision collision)
    {
        Health heath = collision.gameObject.GetComponent<Health>();

        if(heath != null)
        {
            heath.DealDamage(_curDamage);
        }
    }

    public void SetDamage(float newDamage)
    {
        _curDamage = newDamage;
        GetComponent<Weapon>().Trail(newDamage > 0.1f);
    }
}
