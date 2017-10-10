using UnityEngine;
using System.Collections;

public class RepeatData : BTNodeData
{
    public RepeatData(int numRepeat)
    {
        NumRepeat = numRepeat;
        maxNum = numRepeat;
    }
    public int NumRepeat = 0;
    public int maxNum;
}

public class BTRepeat : BTDecoratorNode
{

    public override void Initialize(ref BTAgentData nodeData, BTNodeData data)
    {
        if (data == null)
        {
            nodeData.MyData = new RangeData(0,1);
        }
        else
        {
            nodeData.MyData = data;
        }
    }

    public override NodeStatus Enter(ref BTAgentData nodeData)
    {
        ((RepeatData) nodeData.MyData).NumRepeat = ((RepeatData) nodeData.MyData).maxNum;
        return NodeStatus.Running;
    }

    public override void Exit(ref BTAgentData nodeData)
    {

    }

    public override NodeStatus Update(ref BTAgentData nodeData)
    {
        var data = GetChild(ref nodeData);
        var mydat = ((RepeatData) nodeData.MyData);
        var behave = nodeData.MyTree.MyGameObject.GetComponent<AIBehaviors>();
        //Vector3 toVec = behave.Target - nodeData.MyTree.MyGameObject.transform.position;

        NodeStatus stat = (BehaviorTreeSystem.nodes[data.MyType]).Tick(ref data);
        mydat.NumRepeat--;
        //If the node is done and there are more repeats
        if ((stat == NodeStatus.Failure || stat == NodeStatus.Success) && mydat.NumRepeat > 0)
        {
            stat = NodeStatus.Ready;
            data.CurStatus = stat;
            return NodeStatus.Running;
        }
        else
        {
            data.CurStatus = stat;

            return stat;
        }
        
        return NodeStatus.Running;
        //if the distance is too far
        //if (((RangeData)nodeData.MyData).minDist < toVec.sqrMagnitude && toVec.sqrMagnitude < ((RangeData)nodeData.MyData).maxDist)
        //{
        //    NodeStatus stat = (BehaviorTreeSystem.nodes[data.MyType]).Tick(ref data);
        //    data.CurStatus = stat;
        //    return stat;
        //}
        //else
        //{
        //    return NodeStatus.Failure;
        //}
    }
}
