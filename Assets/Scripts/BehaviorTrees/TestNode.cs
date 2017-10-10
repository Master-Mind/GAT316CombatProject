using UnityEngine;
using System.Collections;

public class TestNodeData : BTNodeData
{
    public float timer = 0.0f;
}

public class TestNode : BTNode
{
    public override void Initialize(ref BTAgentData nodeData, BTNodeData data)
    {
        if (data == null)
        {
            nodeData.MyData = new TestNodeData();
        }
        else
        {
            nodeData.MyData = data;
        }
    }
    public override NodeStatus Enter(ref BTAgentData nodeData)
    {
        var dat = ((TestNodeData)nodeData.MyData);
        dat.timer = 0;
        return NodeStatus.Running;
    }

    public override void Exit(ref BTAgentData nodeData)
    {

    }

    // Update is called once per frame
    public override NodeStatus Update(ref BTAgentData nodeData)
    {
        Debug.Log("testing testing 1 2 3");
        var dat = ((TestNodeData) nodeData.MyData);
        dat.timer += Time.deltaTime;
        if (dat.timer >= 1)
        {
            Debug.Log("Swiggity Swoggity im fucken dones");
            return NodeStatus.Success;
        }

        return NodeStatus.Running;
    }
}
