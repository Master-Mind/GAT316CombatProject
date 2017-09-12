﻿using UnityEngine;
using System.Collections;
using Assets.Scripts.ActionSystem;

[System.Serializable]
public class ActionSequence : Action
{
    public ArrayThatWorks<Action> _actionList;
    public ActionSequence()
    {

    }
    public override bool Execute()
    {
        if (((Action) _actionList[_actionList.Count() - 1]).Execute())
        {
            _actionList.RemoveAt(_actionList.Count() - 1);
        }

        return _actionList.Count() == 0;
    }

    public ActionSequence(GameObject objectToActOn, ArrayThatWorks<Action> actionList) : base(objectToActOn)
    {
        _actionList = actionList;
        _actionList.Reverse();
    }
}
