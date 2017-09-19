using UnityEngine;
using System.Collections;
using Assets.Scripts.ActionSystem;

[System.Serializable]
public class SlerpAboutAction : Action
{
    private float time = 0;
    public float angle = 0;
    public float endTime = 0;
    public Vector3 rotAbout;
    private GameObject bazinga;

    public SlerpAboutAction()
    {

    }

    public override void Initialize()
    {
        //rotAbout = myObj;
        bazinga = myObj.transform.parent.parent.gameObject;
    }
    public override bool Execute()
    {
        time += Time.deltaTime;
        myObj.transform.RotateAround(bazinga.transform.position + rotAbout, Vector3.up, angle * Time.deltaTime * (1 / endTime));
        return time >= endTime;
    }

    public SlerpAboutAction(GameObject objectToActOn, Vector3 rotAbout, float endTime, float angle) : base(objectToActOn)
    {
        this.rotAbout = rotAbout;
        this.endTime = endTime;
        this.angle = angle;
    }
}
