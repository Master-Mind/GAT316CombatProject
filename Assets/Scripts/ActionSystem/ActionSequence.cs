using UnityEngine;
using System.Collections;
using Assets.Scripts.ActionSystem;

public class ActionSequence : Action
{
    private ArrayList _actionList;

    public override bool Execute()
    {
        if (((Action) _actionList[_actionList.Count - 1]).Execute())
        {
            _actionList.RemoveAt(_actionList.Count - 1);
        }

        return _actionList.Count == 0;
    }

    public ActionSequence(GameObject objectToActOn, ArrayList actionList) : base(objectToActOn)
    {
        _actionList = actionList;
        _actionList.Reverse();
    }
}
