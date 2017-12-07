using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRot : MonoBehaviour
{
    private GameObject Obj;
	// Use this for initialization
	void Start () {
	    Obj = GameObject.Find("Boss");

    }
	
	// Update is called once per frame
	void Update () {
	    if (Obj == null)
	    {
	        transform.RotateAround(transform.position - transform.localScale, transform.right, 90);
	    }

    }
}
