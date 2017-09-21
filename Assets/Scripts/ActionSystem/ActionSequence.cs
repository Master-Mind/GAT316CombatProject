using UnityEngine;
using System.Collections;
using Assets.Scripts.ActionSystem;

[System.Serializable]
public class ActionSequence : Action
{
    public ArrayThatWorksForActions _actionList;
    private int Cur = 0;
    public ActionSequence()
    {
        _actionList = new ArrayThatWorksForActions();
    }
    public override bool Execute()
    {
        if (((Action) _actionList[Cur]).Execute())
        {
            _actionList.RemoveAt(Cur);
        }

        return _actionList.Count() == 0;
    }

    public ActionSequence(GameObject objectToActOn, ArrayThatWorksForActions actionList) : base(objectToActOn)
    {
        _actionList = actionList;
        _actionList.Reverse();
    }

    public ActionSequence(ActionSequence actseq)
    {
        _actionList = actseq._actionList;
    }
}
