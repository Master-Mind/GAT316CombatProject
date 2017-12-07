using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsPanel : MonoBehaviour
{
    private int _curSlide = 0;
	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i != 0)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NextSlide()
    {
        transform.GetChild(_curSlide).gameObject.SetActive(false);
        _curSlide++;
        if (_curSlide >= transform.childCount)
        {
            _curSlide = 0;
        }
        transform.GetChild(_curSlide).gameObject.SetActive(true);
    }
}
