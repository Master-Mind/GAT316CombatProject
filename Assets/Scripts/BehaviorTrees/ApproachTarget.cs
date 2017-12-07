using UnityEngine;
using System.Collections;

public class ApproachTarget : BTNode
{
    
    public override void Initialize(ref BTAgentData nodeData, BTNodeData data)
    {
        
    }

    public override NodeStatus Enter(ref BTAgentData nodeData)
    {
        return NodeStatus.Running;
    }

    public override void Exit(ref BTAgentData nodeData)
    {
        nodeData.MyBehaviors.MovementTarget = nodeData.MyBehaviors.transform.position;
    }

    public override NodeStatus Update(ref BTAgentData nodeData)
    {
        var behave = nodeData.MyBehaviors;
        //if (behave.gameObject.name == "Boss")
        //{
        //
        //    Debug.Log("Targ: " + behave.Target);
        //}
        nodeData.MyBehaviors.MovementTarget = behave.Target;

        nodeData.MyBehaviors.Speed = nodeData.MyBehaviors.RunningSpeed;
        return NodeStatus.Running;
    }
}
