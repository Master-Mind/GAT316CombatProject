using UnityEngine;
using System.Collections;
using Assets.Scripts.ActionSystem;

[System.Serializable]
public class SlerpAboutAction : Action
{
    private float time = 0;
    private float angle = 0;
    private float endTime = 0;
    private GameObject rotAbout;


    public override bool Execute()
    {
        time += Time.deltaTime;
        myObj.transform.RotateAround(rotAbout.transform.position, Vector3.up, angle * Time.deltaTime * (1 / endTime));
        return time >= endTime;
    }

    public SlerpAboutAction(GameObject objectToActOn, GameObject rotAbout, float endTime, float angle) : base(objectToActOn)
    {
        this.rotAbout = rotAbout;
        this.endTime = endTime;
        this.angle = angle;
    }
}
