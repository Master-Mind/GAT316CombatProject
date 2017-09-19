using UnityEngine;
using System.Collections;
using Assets.Scripts.ActionSystem;

[System.Serializable]
public class WaitAction : Action
{
    private float time = 0;
    public float endTime = 0;
    private Weapon wep;

    public override void Initialize()
    {
        wep = myObj.GetComponent<Weapon>();
    }

    public override bool Execute()
    {
        time += Time.deltaTime;
        return !(wep.IsWaiting = time <= endTime);
    }

    public WaitAction(GameObject objectToActOn, float endTime) : base(objectToActOn)
    {
        this.endTime = endTime;
    }

    public WaitAction() 
    {
    }
}
