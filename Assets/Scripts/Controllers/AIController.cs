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
    public float speed;
    // Use this for initialization
    void Start ()
    {
        behaviors = GetComponent<AIBehaviors>();

        tree = new BehaviorTree(gameObject).AddNode((int)BTNodeTypes.Selector,0).
                                                AddNode((int)BTNodeTypes.WithinInRange, 1, new RangeData(10,100)).
                                                    AddNode((int)BTNodeTypes.ApproachTarget, 2).
                                                AddNode((int)BTNodeTypes.WithinInRange, 1, new RangeData(0, 10)).
                                                    AddNode((int)BTNodeTypes.EnoughRoom, 2, new RoomData(4)).
                                                        AddNode((int)BTNodeTypes.AttackShort, 3);
        player = GameObject.Find("Player");
        tree.Initialize();
        controller = GetComponent<MovementController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        behaviors.Target = player.transform.position;
        BehaviorTreeSystem.Update(tree);

        var move = (behaviors.MovementTarget - transform.position).normalized;
        transform.LookAt(player.transform);
        controller.MoveDir(move * speed);
    }
}
