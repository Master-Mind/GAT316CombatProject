using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private BehaviorTree tree;
    private MovementController controller;
    private GameObject player;
    private int curNode;
    private AIBehaviors behaviors;
    // Use this for initialization
    void Start ()
    {
        behaviors = GetComponent<AIBehaviors>();


    }
	
	// Update is called once per frame
	void Update ()
    {

        var move = (behaviors.MovementTarget - transform.position).normalized;


    }
}
