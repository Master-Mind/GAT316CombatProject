using UnityEngine;
using System.Collections;
using Assets.Scripts.ActionSystem;

[System.Serializable]
public class InterpolateAction : Action
{
    private float time = 0;
    public float endTime = 0;
    public Vector3 moveTo;

    public InterpolateAction()
    {
    
    }
    public override bool Execute()
    {
        time += Time.deltaTime;
        myObj.transform.localPosition = Vector3.Lerp(myObj.transform.localPosition, moveTo, time);

        return time >= endTime;
    }

    public InterpolateAction(GameObject objectToActOn, Vector3 moveTo, float endTime) : base(objectToActOn)
    {
        this.moveTo = moveTo;
        this.endTime = endTime;
    }
}
