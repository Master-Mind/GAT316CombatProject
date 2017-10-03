using UnityEngine;
using System.Collections;
public class RandomData : BTNodeData
{
    public RandomData()
    {
    }
    public int numTried = 0;
}
public class BTRandomSelector : BTControlFlowNode
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
            nodeData.CurChild = Mathf.RoundToInt(Random.value * (float)nodeData.ChildIndecies.Count);
            ((RandomData) nodeData.MyData).numTried++;
        }
        else if (stat == NodeStatus.Success)
        {
            return NodeStatus.Success;
        }
        if (((RandomData)nodeData.MyData).numTried == nodeData.ChildIndecies.Count)
        {
            nodeData.CurChild = 0;
            return NodeStatus.Failure;
        }

        RunChild(nodeData.CurChild, ref nodeData);
        return NodeStatus.Running;
    }
}