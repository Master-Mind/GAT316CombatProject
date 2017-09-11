using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    private GameObject _mount;
    public GameObject weapon = null;
    private GameObject _weaponInternal = null;
    private Weapon _weaponComp = null;
    // Use this for initialization
    void Start ()
    {
        _mount = new GameObject();
        _mount.transform.SetParent(transform);
        _mount.transform.localPosition = Vector3.zero;
        _weaponInternal = (GameObject)Instantiate(weapon);
        _weaponInternal.transform.SetParent(_mount.transform);
        _weaponComp = _weaponInternal.GetComponent<Weapon>();
        _weaponInternal.transform.localPosition = _weaponComp.RestingPos;
       // _weapon.transform.SetParent(_mount.transform);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShortAttack()
    {
        _weaponComp.QuickAttack();
    }

    public void LongAttack()
    {
        _weaponComp.LongAttack();
    }
}
