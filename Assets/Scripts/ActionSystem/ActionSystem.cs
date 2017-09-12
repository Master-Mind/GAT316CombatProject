using UnityEngine;
using System.Collections;
using Assets.Scripts.ActionSystem;

public class ActionSystem : MonoBehaviour
{
    private static ArrayList _actions = new ArrayList();
    public bool IsActive = false;
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
	    IsActive = _actions.Count > 0;
        for (int index = 0; index < _actions.Count; ++index)
	    {
	        var act = _actions[index];
	        if (((Action) _actions[index]).Execute())
	        {
	            _actions.RemoveAt(index);
	            --index;
	        }
	    }
	}

    public void AddAction(Action action)
    {
        _actions.Add(action);
    }
}
