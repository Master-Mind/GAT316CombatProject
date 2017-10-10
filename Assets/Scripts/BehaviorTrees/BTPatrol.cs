using UnityEngine;
using System.Collections;
public class PatrolData : BTNodeData
{
    public PatrolData(Vector3 [] route)
    {
        Route = route;
    }
    public Vector3[] Route;
    public int CurNode;
}

public class BTPatrol : BTNode
{

    public override void Initialize(ref BTAgentData nodeData, BTNodeData data)
    {
        if (data == null)
        {
            nodeData.MyData = new PatrolData(null);
            throw new System.Exception("Error, visiblessed data was not passed in initialize");
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
        var behave = nodeData.MyTree.MyGameObject.GetComponent<AIBehaviors>();
        var dat = ((PatrolData) nodeData.MyData);
        var route = dat.Route;
        if ((nodeData.MyTree.MyGameObject.transform.position - route[dat.CurNode]).sqrMagnitude < 0.5f)
        {
            dat.CurNode++;
            if (dat.CurNode >= route.Length)
            {
                dat.CurNode = 0;
            }
        }
        
        nodeData.MyBehaviors.MovementTarget = route[dat.CurNode];


        nodeData.MyBehaviors.Speed = nodeData.MyBehaviors.WalkingSpeed;

        return NodeStatus.Running;
    }
}
