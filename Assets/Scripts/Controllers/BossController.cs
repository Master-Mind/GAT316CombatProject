using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.BehaviorTrees;
using UnityEngine;

public class BossController : MonoBehaviour
{

    private BehaviorTree tree;
    private MovementController controller;
    private GameObject player;
    private int curNode;
    private AIBehaviors behaviors;
    public float speed;
    public float ApproachDist;
    public float ShortAttackDist;
    public float LongAttackDist;

    public string[] Attacks;
    public float AITickRate = 0.5f;

    private float curTick;
    // Use this for initialization
    void Start()
    {
        behaviors = GetComponent<AIBehaviors>();
        curTick = AITickRate;
        tree = new BehaviorTree(gameObject).AddNode((int)BTNodeTypes.Selector, 0).
                                                AddNode((int)BTNodeTypes.Parrallel, 1).
                                                    AddNode((int)BTNodeTypes.IgnoreStat, 2, new IgnoreData(NodeStatus.Success)).
                                                        AddNode((int)BTNodeTypes.WithinInRange, 3, new RangeData(ApproachDist, 100)).
                                                    AddNode((int)BTNodeTypes.Idle, 2).
                                                AddNode((int)BTNodeTypes.Parrallel, 1).
                                                    AddNode((int)BTNodeTypes.IgnoreStat, 2, new IgnoreData(NodeStatus.Success)).
                                                        AddNode((int)BTNodeTypes.WithinInRange, 3, new RangeData(LongAttackDist, ApproachDist)).
                                                    AddNode((int)BTNodeTypes.ApproachTarget, 2).
                                                AddNode((int)BTNodeTypes.Parrallel, 1).
                                                    AddNode((int)BTNodeTypes.IgnoreStat, 2, new IgnoreData(NodeStatus.Success)).
                                                        AddNode((int)BTNodeTypes.WithinInRange, 3, new RangeData(ShortAttackDist, LongAttackDist)).
                                                    AddNode((int)BTNodeTypes.Forever, 2).
                                                        AddNode((int)BTNodeTypes.RandomSeq, 3).
                                                            AddNode((int)BTNodeTypes.AttackTrig, 4, new AttackData("SweepLong")).
                                                            AddNode((int)BTNodeTypes.AttackTrig, 4, new AttackData("Quick1")).
                                                AddNode((int)BTNodeTypes.Parrallel, 1).
                                                    AddNode((int)BTNodeTypes.IgnoreStat, 2, new IgnoreData(NodeStatus.Success)).
                                                        AddNode((int)BTNodeTypes.WithinInRange, 3, new RangeData(0, ShortAttackDist)).
                                                    AddNode((int)BTNodeTypes.Forever, 2).
                                                        AddNode((int)BTNodeTypes.RandomSeq, 3).
                                                            AddNode((int)BTNodeTypes.AttackTrig, 4, new AttackData("SweepShort"));
        
        player = GameObject.Find("Player");
        tree.Initialize();
        controller = GetComponent<MovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        curTick -= Time.deltaTime;
        if (curTick <= 0)
        {
            BehaviorTreeSystem.Update(tree);
            curTick = AITickRate;
        }
        behaviors.Target = player.transform.position;

        var move = (behaviors.MovementTarget - transform.position).normalized;

       
        if (!GetComponent<CombatController>().IsAttacking())
        {
            var foo = player.transform.position;

            foo.y = transform.position.y;

            transform.LookAt(foo);
        }
        controller.MoveDir(move * speed);
    }
}
