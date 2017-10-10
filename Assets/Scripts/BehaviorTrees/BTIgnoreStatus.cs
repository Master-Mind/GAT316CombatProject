using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.BehaviorTrees
{
    public class IgnoreData : BTNodeData
    {
        public IgnoreData(NodeStatus whatToIgnore)
        {
            ignoreThis = whatToIgnore;
        }
        public NodeStatus ignoreThis;
    }
    class BTIgnoreStatus : BTDecoratorNode
    {
        public override void Initialize(ref BTAgentData nodeData, BTNodeData data)
        {
            if (data == null)
            {
                throw new System.Exception("Missing data in BTIgnore");
            }
            else
            {
                nodeData.MyData = data;
            }
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
            var stat = GetChild(ref nodeData).CurStatus;
            var mydat = (IgnoreData) nodeData.MyData;

            if (stat == mydat.ignoreThis)
            {
                RunChild(ref nodeData);
                return NodeStatus.Running;
            }

            return stat;
        }
    }
}
