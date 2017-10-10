using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.BehaviorTrees;
using UnityEngine.AI;

[RequireComponent(typeof(EventDispatcher))]
public class AIBehaviors : MonoBehaviour
{
    [HideInInspector]
    public Vector3 Target = new Vector3();
    [HideInInspector]
    public Vector3 MovementTarget = new Vector3();

    public EventDispatcher MyDispatcher;
    public float Speed = 0.05f;
    private BehaviorTree _myTree;
    public string TreeType;
    private GameObject _player;
    public Vector3[] MyPatrolRoute;
    
    public float WalkingSpeed = 0.5f;
    public float RunningSpeed = 1f;
    // Use this for initialization
    void Start ()
    {
        Speed = WalkingSpeed;
        
        MyDispatcher = GetComponent<EventDispatcher>();

        
        
        _player = GameObject.Find("Player");
        
        //_myTree.Initialize();

    }

    private void Update()
    {
        Target = _player.transform.position;
        //BehaviorTreeSystem.Update(_myTree);
        var temp = MovementTarget;
        temp.y = transform.position.y;
        //transform.LookAt(temp);
        //GetComponent<MoveController>().Move((MovementTarget - transform.position).normalized * Speed);

    }
}
