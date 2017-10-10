using UnityEngine;
using System.Collections.Generic;
using System;
using System.Security.Policy;
using Assets.Scripts.BehaviorTrees;

public enum BTNodeTypes : int
{
    Test,
    Sequencer,
    Idle,
    ApproachTarget,
    WithinInRange,
    AttackShort,
    Selector,
    EnoughRoom,
    Patrol,
    Negate,
    RunWhileSuccess,
    Parrallel,
    Forever,
    IgnoreStat,
    Sequence,
    RandomSeq,
    Repeat,
    AttackLong,
    Count
}
public static class BehaviorTreeSystem
{

    public static BTNode [] nodes = new BTNode[(int)BTNodeTypes.Count];

    public static bool StacksHaveBeenDirtied = false;
	// Use this for initialization
    static BehaviorTreeSystem()
    {
        nodes[(int)BTNodeTypes.Test] = new TestNode();
        nodes[(int)BTNodeTypes.Sequencer] = new BTSequencer();
        nodes[(int)BTNodeTypes.Idle] = new BTIdle();
        nodes[(int)BTNodeTypes.ApproachTarget] = new ApproachTarget();
        nodes[(int)BTNodeTypes.WithinInRange] = new WithinInRange();
        nodes[(int)BTNodeTypes.AttackShort] = new AttackShortRange();
        nodes[(int)BTNodeTypes.Selector] = new BTSelector();
        nodes[(int)BTNodeTypes.Patrol] = new BTPatrol();
        nodes[(int)BTNodeTypes.Negate] = new BTNegate();
        nodes[(int)BTNodeTypes.RunWhileSuccess] = new BTRunWhileSuccess();
        nodes[(int)BTNodeTypes.Parrallel] = new BTParallel();
        nodes[(int)BTNodeTypes.Forever] = new BTForever();
        nodes[(int)BTNodeTypes.IgnoreStat] = new BTIgnoreStatus();
        nodes[(int)BTNodeTypes.Sequence] = new BTSequence();
        nodes[(int)BTNodeTypes.Sequence] = new BTRandomSequence();
        nodes[(int)BTNodeTypes.Repeat] = new BTRepeat();
        nodes[(int)BTNodeTypes.AttackLong] = new AttackLongRange();
        nodes[(int)BTNodeTypes.EnoughRoom] = new HasEnoughRoom();
        nodes[(int)BTNodeTypes.RandomSeq] = new BTRandomSequence();
    }

    static void Register(BTAgentData nodeData)
    {
        
    }

    public static BehaviorTree RequestTree(string treeName, GameObject parent)
    {
        return new BehaviorTree(parent);
    }

    public static void Update(BehaviorTree tree)
    {
        //update the root
        //update its children
        bool fist = true;
        for (int i = 0; i < tree.excecutionStacks.Count; ++i)
        {
            if (tree.excecutionStacks[i].Count > 0)
            {
                var node = tree.excecutionStacks[i].Peek();
                var dat = (BTAgentData)tree.myData[node.Value];
                ((BTAgentData)tree.myData[node.Value]).CurStatus = nodes[node.Key].Tick(ref dat);
                if (StacksHaveBeenDirtied)
                {
                    StacksHaveBeenDirtied = false;
                    Update(tree);
                    return;
                }
                if (((BTAgentData)tree.myData[node.Value]).CurStatus == NodeStatus.Failure ||
                    ((BTAgentData)tree.myData[node.Value]).CurStatus == NodeStatus.Success)
                {
                    if (fist && tree.excecutionStacks[i].Count == 1)
                    {
                        ((BTAgentData)tree.myData[node.Value]).CurStatus = NodeStatus.Ready;
                    }
                    else
                    {
                        nodes[node.Key].Tick(ref dat);
                        tree.excecutionStacks[i].Pop();
                    }
                }

                tree.myData[node.Value] = dat;

            }
            fist = false;
        }
        
    }
}
