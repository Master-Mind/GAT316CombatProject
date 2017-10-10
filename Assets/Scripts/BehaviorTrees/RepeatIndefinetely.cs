using UnityEngine;
using System.Collections;



public class RepeatIndefinietely : BTDecoratorNode
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
        return NodeStatus.Running;
    }

    public override void Exit(ref BTAgentData nodeData)
    {

    }

    public override NodeStatus Update(ref BTAgentData nodeData)
    {
        var data = GetChild(ref nodeData);
        var behave = nodeData.MyTree.MyGameObject.GetComponent<AIBehaviors>();
        //Vector3 toVec = behave.Target - nodeData.MyTree.MyGameObject.transform.position;

        NodeStatus stat = (BehaviorTreeSystem.nodes[data.MyType]).Tick(ref data);
        if (stat == NodeStatus.Failure)
        {
            stat = NodeStatus.Ready;
        }
        data.CurStatus = stat;
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
