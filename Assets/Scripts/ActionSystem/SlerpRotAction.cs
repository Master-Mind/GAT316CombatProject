using UnityEngine;
using System.Collections;
using Assets.Scripts.ActionSystem;

[System.Serializable]
public class SlerpRotAction : Action
{
    private float time = 0;
    public float endTime = 0;
    public Quaternion rotTo;

    public SlerpRotAction()
    {

    }
    public override bool Execute()
    {
        time += Time.deltaTime;
        myObj.transform.localRotation = Quaternion.Slerp(myObj.transform.localRotation, rotTo, time / endTime);
        return time >= endTime;
    }

    public SlerpRotAction(GameObject objectToActOn, Quaternion rotTo, float endTime) : base(objectToActOn)
    {
        this.rotTo = rotTo;
        this.endTime = endTime;
    }
}
