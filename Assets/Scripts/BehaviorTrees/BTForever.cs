using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.BehaviorTrees
{
    class BTForever : BTDecoratorNode
    {
        public override void Initialize(ref BTAgentData nodeData, BTNodeData data)
        {
        }

        public override NodeStatus Enter(ref BTAgentData nodeData)
        {
            RunChild(ref nodeData);
            return NodeStatus.Running;
        }

        public override void Exit(ref BTAgentData nodeData)
        {
        }

        public override NodeStatus Update(ref BTAgentData nodeData)
        {
            RunChild(ref nodeData);
            return NodeStatus.Running;
        }
    }
}
