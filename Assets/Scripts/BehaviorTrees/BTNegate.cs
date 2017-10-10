using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.BehaviorTrees
{
    public class BTNegate : BTDecoratorNode
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
            var data = GetChild(ref nodeData);
            var behave = nodeData.MyTree.MyGameObject.GetComponent<AIBehaviors>();

            NodeStatus stat = (BehaviorTreeSystem.nodes[data.MyType]).Tick(ref data);

            data.CurStatus = stat;
            switch (stat)
            {
                case NodeStatus.Ready:
                case NodeStatus.Running:
                    return NodeStatus.Running;
                    break;
                case NodeStatus.Failure:
                    data.CurStatus = NodeStatus.Ready;
                    return NodeStatus.Success;
                    break;
                case NodeStatus.Success:

                    return NodeStatus.Failure;
                    break;
                default:
                    break;
            }
            return stat;

        }
    }
}
