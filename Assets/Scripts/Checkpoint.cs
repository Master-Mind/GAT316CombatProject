using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public enum Where
    {
        Start,
        Corridor,
        AfterCamp,
    }

    public Where AmI;

    private const bool ActuallyDo = true;
    public static Where ShouldThePlayerBe = Where.Start;

    private GameObject _player;
	// Use this for initialization
	void Start () {
	    _player = GameObject.Find("Player");

        if (ActuallyDo && AmI == ShouldThePlayerBe)
        {
            _player.transform.position = transform.position;

        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
