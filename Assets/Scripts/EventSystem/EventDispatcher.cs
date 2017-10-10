using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDispatcher : MonoBehaviour
{
    [HideInInspector]
    public Dictionary<int, List<Delegate>> Subscribers;
    // Use this for initialization
    void Start ()
    {
        Subscribers = new Dictionary<int, List<Delegate>>();

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
