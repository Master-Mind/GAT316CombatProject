using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BTAgentData
{
    public BehaviorTree MyTree;
    public AIBehaviors MyBehaviors;
    public BTNodeData MyData;
    public List<int> ChildIndecies = new List<int>();
    public int MyType;
    public int MyIndex;
    public int ParentIndex;
    public int MyStackIndex;
    public int CurChild;
    public int Depth;
    public NodeStatus CurStatus;
}
