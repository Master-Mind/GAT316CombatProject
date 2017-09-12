using UnityEngine;
using System.Collections;
using Assets.Scripts.ActionSystem;

[System.Serializable]
public class WaitAction : Action
{
    private float time = 0;
    private float endTime = 0;


    public override bool Execute()
    {
        time += Time.deltaTime;
        return time >= endTime;
    }

    public WaitAction(GameObject objectToActOn, float endTime) : base(objectToActOn)
    {
        this.endTime = endTime;
    }
}
