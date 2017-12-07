using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDeath : MonoBehaviour
{
    public float Tim;

    public bool HaltButts;

    private GameObject[] _butts;
	// Use this for initialization
	void Start ()
	{
	    if (HaltButts)
	    {
	        _butts = GameObject.FindGameObjectsWithTag("Button");
	        foreach (var butt in _butts)
	        {
	            butt.SetActive(false);
	        }
        }

	}
	
	// Update is called once per frame
	void Update ()
	{
	    Tim -= Time.deltaTime;
	    if (Tim <= 0 || Input.anyKey)
	    {
	        if (HaltButts)
	        {
	            foreach (var butt in _butts)
	            {
	                butt.SetActive(true);
	            }
	        }
            Destroy(gameObject);
	    }
	}
}
