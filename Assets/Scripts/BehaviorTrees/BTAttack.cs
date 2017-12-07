using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using System.Collections;

public class AttackData : BTNodeData
{
    public AttackData(string triggerName)
    {
        TriggerName = triggerName;
        tim = 0.1f;
    }

    public string TriggerName;
    public bool GotMineIn;
    public float tim;
}

public class BTAttack : BTNode
{

    public override void Initialize(ref BTAgentData nodeData, BTNodeData data)
    {
        nodeData.MyData = data;
        Debug.Assert(data != null);
    }

    public override NodeStatus Enter(ref BTAgentData nodeData)
    {
        ((AttackData)nodeData.MyData).GotMineIn = false;
        ((AttackData) nodeData.MyData).tim = 0.1f;
        return NodeStatus.Running;
    }

    public override void Exit(ref BTAgentData nodeData)
    {

    }

    public override NodeStatus Update(ref BTAgentData nodeData)
    {
        //if (nodeData.MyTree.MyGameObject.name == "Boss")
        // Debug.Log(nodeData.MyTree.MyGameObject.GetComponent<CombatController>().IsAttacking());
        if (nodeData.MyTree.MyGameObject.GetComponent<CombatController>().IsAttacking())
        {
            return NodeStatus.Running;
        }
        else if (((AttackData) nodeData.MyData).GotMineIn)
        {
            ((AttackData)nodeData.MyData).tim -= Time.deltaTime;
            if (((AttackData)nodeData.MyData).tim >= 0)
                return NodeStatus.Running;
            return NodeStatus.Success;
        }
        //Debug.Log(((AttackData)nodeData.MyData).TriggerName);
        nodeData.MyTree.MyGameObject.GetComponent<CombatController>().AttackTrigger(((AttackData)nodeData.MyData).TriggerName);
        ((AttackData) nodeData.MyData).GotMineIn = true;
        return NodeStatus.Running;
    }
}
