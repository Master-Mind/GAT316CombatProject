using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.BehaviorTrees;
using UnityEngine;

public class SpearEnemy : MonoBehaviour
{
    private BehaviorTree tree;
    private MovementController controller;
    private GameObject player;
    private int curNode;
    private AIBehaviors behaviors;
    public float speed;
    public float ApproachDist;
    public float AttackDist;

    public float AITickRate = 0.5f;
    public string[] Attacks;
    private float curTick;
    // Use this for initialization
    void Start ()
    {
        behaviors = GetComponent<AIBehaviors>();
        curTick = AITickRate;
        tree = new BehaviorTree(gameObject).AddNode((int)BTNodeTypes.Selector,0).
                                                AddNode((int)BTNodeTypes.Parrallel, 1).
                                                    AddNode((int)BTNodeTypes.IgnoreStat, 2, new IgnoreData(NodeStatus.Success)).
                                                        AddNode((int)BTNodeTypes.WithinInRange, 3, new RangeData(ApproachDist, 100)).
                                                    AddNode((int)BTNodeTypes.Idle, 2).
                                                AddNode((int)BTNodeTypes.Parrallel, 1).
                                                    AddNode((int)BTNodeTypes.IgnoreStat, 2, new IgnoreData(NodeStatus.Success)).
                                                        AddNode((int)BTNodeTypes.WithinInRange, 3, new RangeData(AttackDist, ApproachDist)).
                                                    AddNode((int)BTNodeTypes.ApproachTarget, 2).
                                                AddNode((int)BTNodeTypes.Parrallel, 1).
                                                    AddNode((int)BTNodeTypes.IgnoreStat, 2, new IgnoreData(NodeStatus.Success)).
                                                        AddNode((int)BTNodeTypes.WithinInRange, 3, new RangeData(0, AttackDist)).
                                                    AddNode((int)BTNodeTypes.Forever, 2).
                                                        AddNode((int)BTNodeTypes.RandomSeq, 3);
        foreach (var attack in Attacks)
        {
            tree.AddNode((int) BTNodeTypes.AttackTrig, 4, new AttackData(attack));
        }
        player = GameObject.Find("Player");
        tree.Initialize();
        controller = GetComponent<MovementController>();
    }
	
	// Update is called once per frame
	void Update ()
	{
	    curTick -= Time.deltaTime;
	    if (curTick <= 0)
	    {
	        BehaviorTreeSystem.Update(tree);
	        curTick = AITickRate;
	    }
        behaviors.Target = player.transform.position;

        var move = (behaviors.MovementTarget - transform.position).normalized;
        transform.LookAt(player.transform);
        controller.MoveDir(move * speed);
    }
}
