using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunTrig : MonoBehaviour
{
    public Vector3 EndPoint;

    private NavMeshPath Path;

    private int _curNode;
	// Use this for initialization
	void Start ()
	{
	    Path = new NavMeshPath();

        NavMesh.CalculatePath(transform.position, EndPoint, NavMesh.AllAreas, Path);
        Debug.Log(Path.status);
	    Debug.Log(transform.position);
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (Path.status == NavMeshPathStatus.PathInvalid)
	    {
	        NavMesh.CalculatePath(transform.position, EndPoint, NavMesh.AllAreas, Path);
	        Debug.Log(Path.status);

	        if (Path.status == NavMeshPathStatus.PathInvalid)
	        {
	            return;
	        }
	    }
        if (_curNode >= Path.corners.Length)
	    {
	        return;
	    }
	    var node = Path.corners[_curNode];
	    node.y = transform.position.y;

        var movement = node - transform.position;

        GetComponent<MovementController>().MoveDir(movement.normalized);
        transform.rotation = Quaternion.LookRotation(movement.normalized, Vector3.up);
	    if (movement.sqrMagnitude < 0.1)
	    {
	        _curNode++;
	    }
	}
}
