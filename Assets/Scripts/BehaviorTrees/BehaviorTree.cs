using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;

public class BehaviorTree
{
    public List<BTAgentData> myData = new List<BTAgentData>();
    private List<BTAgentData> tempArray = new List<BTAgentData>();
    private int lastIndex = 0;
    private int lastDepth = 0;
    private int curIndex = 0;
    public GameObject MyGameObject;
    public List<Stack<KeyValuePair<int, int>>> excecutionStacks = new List<Stack<KeyValuePair<int, int>>>();
    public Stack<BTAgentData> nodeStack = new Stack<BTAgentData>();
    public BehaviorTree(GameObject obj)
    {
        MyGameObject = obj;
        excecutionStacks.Add(new Stack<KeyValuePair<int, int>>());
    }
    public BehaviorTree AddNode(int nodeIndex, int depth, BTNodeData data = null)
    {
        BTAgentData newNode = new BTAgentData
        {
            MyType = nodeIndex,
            MyTree = this,
            MyIndex = curIndex++,
            MyBehaviors = MyGameObject.GetComponent<AIBehaviors>()
        };

        if (nodeStack.Count == 0)
        {
            newNode.ParentIndex = -1;
            newNode.Depth = 0;
            newNode.CurStatus = NodeStatus.Ready;
            nodeStack.Push(newNode);
        }
        else
        {
            //if the node is a child
            while (nodeStack.Count != 0)
            {
                if (depth > nodeStack.Peek().Depth)
                {
                    newNode.ParentIndex = nodeStack.Peek().MyIndex;
                    nodeStack.Peek().ChildIndecies.Add(newNode.MyIndex);
                    newNode.Depth = depth;
                    nodeStack.Push(newNode);


                    break;
                }
                else
                {
                    nodeStack.Pop();
                }
            }

        }

        BehaviorTreeSystem.nodes[newNode.MyType].Initialize(ref newNode, data);

        myData.Add(newNode);

        return this;
    }

    public NodeStatus AddToExcecutionList(int type, int index, BTAgentData nodeData)
    {
        excecutionStacks[nodeData.MyStackIndex].Push(new KeyValuePair<int, int>(type, index));
        ((BTAgentData)myData[index]).MyStackIndex = nodeData.MyStackIndex;

        return NodeStatus.Ready;
    }

    public NodeStatus AddToExcecutionListOnDifferentStack(int type, int index, BTAgentData nodeData)
    {
        int selectissimo = GetEmptyStack();
        excecutionStacks[selectissimo].Push(new KeyValuePair<int, int>(type, index));

        ((BTAgentData) myData[index]).MyStackIndex = selectissimo;

        return NodeStatus.Ready;
    }

    private int GetEmptyStack()
    {
        for(int i = 0; i < excecutionStacks.Count; ++i)
        {
            if (excecutionStacks[i].Count == 0)
            {
                return i;
            }
        }

        excecutionStacks.Add(new Stack<KeyValuePair<int, int>>());

        return excecutionStacks.Count - 1;
    }

    public void ClearStack(int stackIndex)
    {
        //clear this stack
        while (excecutionStacks[stackIndex].Count > 0)
        {
            var node = excecutionStacks[stackIndex].Peek();
            var dat = (BTAgentData)myData[node.Value];
            //go through each child and clear their stacks recursively
            for (int i = 0; i < dat.ChildIndecies.Count; ++i)
            {
                if (myData[dat.ChildIndecies[i]].MyStackIndex > stackIndex)
                {
                    ClearStack(myData[dat.ChildIndecies[i]].MyStackIndex);
                }
            }

            excecutionStacks[stackIndex].Pop();
        }
    }

    public void Initialize()
    {
        AddToExcecutionList(((BTAgentData)myData[0]).MyType, ((BTAgentData)myData[0]).MyIndex, (BTAgentData)myData[0]);
    }
}