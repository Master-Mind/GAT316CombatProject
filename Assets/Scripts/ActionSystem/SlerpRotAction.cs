using UnityEngine;
using System.Collections;
using Assets.Scripts.ActionSystem;
using Assets.Scripts;

[System.Serializable]
public class SlerpRotAction : Action
{
    private float time = 0;
    public float endTime = 0;
    public Quaternion rotTo;
    public string parseDisBoiiiiii;
    public SlerpRotAction()
    {
        
    }

    public override void Initialize()
    {
        if (parseDisBoiiiiii != null)
        {
            var hella = new RotationParser(parseDisBoiiiiii);
            rotTo = hella.finalRotation;
        }
        else
        {
            rotTo = Quaternion.identity;
        }
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
