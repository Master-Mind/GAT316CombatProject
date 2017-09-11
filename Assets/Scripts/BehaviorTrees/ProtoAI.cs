using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProtoAI : MonoBehaviour
{
    private BehaviorTree tree;
    private MovementController controller;
    private AIBehaviors behaviors;
    private GameObject player;
    private int curNode;
    // Use this for initialization
    void Start ()
    {
        controller = GetComponent<MovementController>();
        behaviors = GetComponent<AIBehaviors>();
        player = GameObject.Find("Player");
        behaviors = GetComponent<AIBehaviors>();
	    tree = new BehaviorTree(gameObject);
        tree.AddNode((int)BTNodeTypes.Selector, 0).
                AddNode((int)BTNodeTypes.WithinInRange, 1, new RangeData(10, 100)).
                    AddNode((int)BTNodeTypes.ApproachTarget, 2).
                AddNode((int)BTNodeTypes.WithinInRange, 1, new RangeData(0, 10)).
                    AddNode((int)BTNodeTypes.EnoughRoom, 2, new RoomData(4)).
                        AddNode((int)BTNodeTypes.AttackShort, 3); ;
        tree.Initialize();

        
        //behaviors.MovementTarget = nodeList[curNode].Location;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    //behaviors.Target = player.transform.position;
        //BehaviorTreeSystem.Update(tree);
	    
        Vector3 move = Vector3.zero;
	    

        if (true || behaviors.MovementTarget != Vector3.zero)
        {
            move = behaviors.MovementTarget - transform.position;
            move = move.normalized;
        }
        controller.MoveDir(move);
    }
}
