using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CombatController : MonoBehaviour
{
    private GameObject _mount;
    public GameObject Weapon = null;
    public GameObject ShortSword = null;
    public GameObject Falx = null;
    public GameObject GreatSword = null;
    public GameObject Halberd = null;
    private GameObject _weaponInternal = null;
    private MovementController _movementController;
    private Weapon _weaponComp = null;
    private DealsDamage _damageDealer = null;
    private DealsDamage _damagingMe;
    public float stamina = 10f;
    private WeaponSelector.Selection _myWepType;
    private bool [] GottenWeapons;
    private GameObject _hitParticals;
    public GameObject HitParticals;
    // Use this for initialization
    void Start ()
    {
        _mount = new GameObject();
        _mount.transform.SetParent(transform);
        _mount.transform.localPosition = Vector3.zero;
        _mount.transform.localRotation = Quaternion.identity;
        GottenWeapons = new bool[4] ;
        SwitchWeaponTo(WeaponSelector.Selected);
        _movementController = GetComponent<MovementController>();
        GottenWeapons[0] = true;
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
        if (Other.gameObject == _weaponInternal)
        {
            return;
        }

        if (Other.transform.parent != null &&  Other.transform.parent.gameObject.name == "TutorialZones")
        {
            var foo = Other.GetComponent<AddWeapon>();
            if (foo && gameObject.name == "Player" && !GottenWeapons[(int)Other.GetComponent<AddWeapon>().WeponType])
            {
                SwitchWeaponTo(Other.GetComponent<AddWeapon>().WeponType, true);

                GottenWeapons[(int)Other.GetComponent<AddWeapon>().WeponType] = true;
            }
            return;
        }

        Health heath = gameObject.GetComponent<Health>();

        if (heath != null)
        {
            if (Other.GetComponent<DealsDamage>())
            {
                _damagingMe = Other.GetComponent<DealsDamage>();
            }
            else
            {
                _damagingMe = Other.transform.parent.GetComponent<DealsDamage>();
            }
        }
    }

    public void NextWeapon()
    {
        while (true)
        {
            _myWepType++;
            if (_myWepType == WeaponSelector.Selection.Count)
            {
                _myWepType = WeaponSelector.Selection.Sword;
            }
            if (GottenWeapons[(int) _myWepType])
            {
                SwitchWeaponTo(_myWepType, true);
                return;
            }
        }
    }
    void SwitchWeaponTo(WeaponSelector.Selection toSelection, bool JustSwitch = false)
    {
        if(_weaponInternal != null)
            Destroy(_weaponInternal);
        if(JustSwitch || Weapon == null)
        switch (toSelection)
        {
            case WeaponSelector.Selection.Sword:
                Weapon = ShortSword;
                    break;
            case WeaponSelector.Selection.Falx:
                Weapon = Falx;
                    break;
            case WeaponSelector.Selection.GreatSword:
                Weapon = GreatSword;
                    break;
            case WeaponSelector.Selection.Halberd:
                Weapon = Halberd;
                    break;
            default:
                Debug.LogAssertion("Error at line 154 in CombatController, no selection matches the given selection");
                break;
        }

        _weaponInternal = (GameObject)Instantiate(Weapon);
        _weaponInternal.transform.SetParent(_mount.transform);
        _weaponComp = _weaponInternal.GetComponent<Weapon>();
        _damageDealer = _weaponInternal.GetComponent<DealsDamage>();

        _weaponInternal.transform.localPosition = _weaponComp.RestingPos;
        _weaponComp.FromJSON();
        _weaponComp.MyController = this;
        _myWepType = toSelection;
    }

    void OnTriggerStay(Collider Other)
    {
        if(Other.gameObject == _weaponInternal)
        {
            return;
        }
        Health heath = gameObject.GetComponent<Health>();

        if (heath != null)
        {
            if (_damagingMe != null && _damagingMe.Damage > 0.001f)
            {
                _hitParticals = Instantiate(HitParticals, Other.transform, false);
                Destroy(_hitParticals, 0.3f);
                heath.DealDamage(_damagingMe.Damage);
                _damagingMe = null;
                if(Other.GetComponent<AudioSource>() != null)
                    Other.GetComponent<AudioSource>().PlayOneShot(Other.GetComponent<Weapon>().CurClip);
                else
                {
                    var fck = Other.transform.parent;
                    fck.GetComponent<AudioSource>().PlayOneShot(fck.GetComponent<Weapon>().CurClip);
                }
            }
        }
    }
    void OnTriggerExit(Collider Other)
    {
        _damagingMe = null;
    }
}
