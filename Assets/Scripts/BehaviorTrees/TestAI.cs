using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAI : MonoBehaviour
{
    public string MyTreeName;
    private BehaviorTree _myTree;
	// Use this for initialization
	void Start ()
    {
        _myTree = BehaviorTreeSystem.RequestTree(MyTreeName, gameObject);
	}
	
	// Update is called once per frame
	void Update ()
    {
        BehaviorTreeSystem.Update(_myTree);
    }
}
