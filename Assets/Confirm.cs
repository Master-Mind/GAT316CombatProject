using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Confirm : MonoBehaviour
{
    public enum This
    {
        Exit,
        Restart,
        Return,
        None
    }
    public static This ConfirmScene = This.None;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ConfirmNow()
    {
        switch (ConfirmScene)
        {
            case This.Exit:
                Application.Quit();
                break;
            case This.Restart:
                Time.timeScale = 1;
                SceneManager.LoadScene("TestScene");
                break;
            case This.Return:
                Time.timeScale = 1;
                SceneManager.LoadScene("MainMenu");
                break;
            case This.None:
                break;
            default:
                Debug.LogAssertion("Confirm 40");
                break;
        }

    }
}
