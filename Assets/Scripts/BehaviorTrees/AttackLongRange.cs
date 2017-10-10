using UnityEngine;
using System.Collections;

public class AttackLongRange : BTNode
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

    }

    public override NodeStatus Update(ref BTAgentData nodeData)
    {
        if (nodeData.MyTree.MyGameObject.GetComponent<CombatController>().IsAttacking())
        {
            return NodeStatus.Running;
        }
        nodeData.MyTree.MyGameObject.GetComponent<CombatController>().LongAttack();
        return NodeStatus.Success;
    }
}