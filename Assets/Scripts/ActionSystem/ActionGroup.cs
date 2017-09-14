using UnityEngine;
using System.Collections;
using Assets.Scripts.ActionSystem;

[System.Serializable]
public class ActionGroup : Action
{
    public ArrayThatWorksForActions _actionList;
    public ActionGroup()
    {
        _actionList = new ArrayThatWorksForActions();
    }
    public override bool Execute()
    {
        for (int index = 0; index < _actionList.Count(); ++index)
        {
            var act = _actionList[index];
            if (((Action)_actionList[index]).Execute())
            {
                _actionList.RemoveAt(index);
                --index;
            }
        }

        return _actionList.Count() == 0;
    }

    public ActionGroup(GameObject objectToActOn, ArrayThatWorksForActions actionList) : base(objectToActOn)
    {
        _actionList = actionList;
        _actionList.Reverse();
    }
}
