using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.BehaviorTrees
{
    public class BTParallel : BTControlFlowNode
    {
        public override void Initialize(ref BTAgentData nodeData, BTNodeData data)
        {
            
        }

        public override NodeStatus Enter(ref BTAgentData nodeData)
        {
            for (int i = 0; i < nodeData.ChildIndecies.Count; ++i)
            {
                RunChildDifStack(i, ref nodeData);
            }
            //BehaviorTreeSystem.StacksHaveBeenDirtied = true;
            return base.Enter(ref nodeData);
        }

        public override void Exit(ref BTAgentData nodeData)
        {
            for (int i = 0; i < nodeData.ChildIndecies.Count; ++i)
            {
                nodeData.MyTree.ClearStack(GetChild(i, ref nodeData).MyStackIndex);
            }
        }

        public override NodeStatus Update(ref BTAgentData nodeData)
        {
            for (int i = 0; i < nodeData.ChildIndecies.Count; ++ i)
            {
                var status = GetChildStatus(i, ref nodeData);
                if (status == NodeStatus.Failure || status == NodeStatus.Success)
                {
                    return status;
                }
            }

            return NodeStatus.Running;
        }
    }
}
