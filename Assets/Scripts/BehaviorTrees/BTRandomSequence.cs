﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BTRandomSequence : BTControlFlowNode
{

    public override void Initialize(ref BTAgentData nodeData, BTNodeData data)
    {
        nodeData.CurChild = Mathf.RoundToInt(Random.value * (float)nodeData.ChildIndecies.Count);
        nodeData.MyData = new RandomData();
        
    }
    public override NodeStatus Enter(ref BTAgentData nodeData)
    {
        return base.Enter(ref nodeData);
    }

    public override void Exit(ref BTAgentData nodeData)
    {
        base.Exit(ref nodeData);
    }

    public override NodeStatus Update(ref BTAgentData nodeData)
    {
        NodeStatus stat = GetChild(nodeData.CurChild, ref nodeData).CurStatus;
        if (stat == NodeStatus.Failure)
        {
            var foo = (int)Mathf.FloorToInt(Random.value * (float) nodeData.ChildIndecies.Count);
            nodeData.CurChild = foo;
            ((RandomData) nodeData.MyData).numTried++;
        }
        else if (stat == NodeStatus.Success)
        {
            var foo = (int)Mathf.FloorToInt(Random.value * (float)nodeData.ChildIndecies.Count);
            nodeData.CurChild = foo;
            ((RandomData)nodeData.MyData).numTried++;
        }
        if (((RandomData)nodeData.MyData).numTried == nodeData.ChildIndecies.Count)
        {
            ((RandomData) nodeData.MyData).numTried = 0;
            nodeData.CurChild = 0;
            return NodeStatus.Success;
        }

        RunChild(nodeData.CurChild, ref nodeData);
        return NodeStatus.Running;
    }
}