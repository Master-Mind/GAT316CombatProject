using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    private GameObject _mount;
    public GameObject weapon = null;
    private GameObject _weaponInternal = null;
    private MovementController _movementController;
    private Weapon _weaponComp = null;
    private DealsDamage _damageDealer = null;

    public float stamina = 10f;
    // Use this for initialization
    void Start ()
    {
        _mount = new GameObject();
        _mount.transform.SetParent(transform);
        _mount.transform.localPosition = Vector3.zero;
        _weaponInternal = (GameObject)Instantiate(weapon);
        _weaponInternal.transform.SetParent(_mount.transform);
        _weaponComp = _weaponInternal.GetComponent<Weapon>();
        _damageDealer = _weaponInternal.GetComponent<DealsDamage>();

        _weaponInternal.transform.localPosition = _weaponComp.RestingPos;
        _movementController = GetComponent<MovementController>();
        _weaponComp.FromJSON();
        _weaponComp.MyController = this;
        // _weapon.transform.SetParent(_mount.transform);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CloseDistance(float dist)
    {
        Ray ray = new Ray(transform.position, transform.forward);

        var hits = Physics.RaycastAll(ray, dist);

        foreach (var hit in hits)
        {
            if (hit.transform.GetComponent<CombatController>())
            {
                _movementController.ScootTarg = (hit.point - transform.position);
            }
        }
    }

    public bool IsAttacking()
    {
        return !(_weaponComp._isResting);
    }

    public void ShortAttack()
    {
        _weaponComp.QuickAttack();
    }

    public void LongAttack()
    {
        if (_movementController.IsSprinting)
            _weaponComp.ChargeAttack();
        else
            _weaponComp.LongAttack();
    }

    public void AttackTrigger(string triggerName)
    {
        _weaponComp.Attack(triggerName);
    }

    public void Stagger()
    {
        _weaponComp.ForceRest();
    }

    void OnTriggerEnter(Collider Other)
    {
        if(Other.gameObject == _weaponInternal)
        {
            return;
        }
        Health heath = gameObject.GetComponent<Health>();

        if (heath != null)
        {
            if (Other.GetComponent<DealsDamage>())
            {
                heath.DealDamage(Other.GetComponent<DealsDamage>().Damage);
            }
            else
            {
                heath.DealDamage(Other.transform.parent.GetComponent<DealsDamage>().Damage);
            }
        }
    }
}
