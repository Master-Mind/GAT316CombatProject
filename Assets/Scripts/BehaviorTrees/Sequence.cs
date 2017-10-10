using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.BehaviorTrees
{
    class BTSequence : BTControlFlowNode
    {
        public override void Initialize(ref BTAgentData nodeData, BTNodeData data)
        {

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
                nodeData.CurChild = 0;
                return NodeStatus.Failure;
            }
            else if (stat == NodeStatus.Success)
            {
                ++nodeData.CurChild;
            }
            if (nodeData.CurChild == nodeData.ChildIndecies.Count)
            {
                nodeData.CurChild = 0;
                return NodeStatus.Failure;
            }

            RunChild(nodeData.CurChild, ref nodeData);
            return NodeStatus.Running;
        }
    }
}
