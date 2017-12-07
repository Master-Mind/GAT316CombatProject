using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponSelector : MonoBehaviour
{
    public enum Selection
    {
        Sword,
        Falx,
        GreatSword,
        Halberd,
        Count
    }

    public static Selection Selected;
    public Selection WhatISelected;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetSelction()
    {
        Selected = WhatISelected;
        SceneManager.LoadScene("TestScene");
    }
}
