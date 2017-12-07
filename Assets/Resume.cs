using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resume : MonoBehaviour
{
    private GameObject _player;
	// Use this for initialization
	void Start () {
	    _player = GameObject.Find("Player");

    }
	
	// Update is called once per frame
	void Update () {
	    if (_player.GetComponent<PlayerController>().MyController.GetButtonPressed(Controller.Button.Start))
	    {
	        ResumeFunc();
	    }
	}

    public void ResumeFunc()
    {
        Time.timeScale = 1;
        transform.parent.gameObject.SetActive(false);
    }
}
