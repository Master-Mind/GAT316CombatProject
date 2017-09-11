﻿using UnityEngine;
using System.Collections;
using Assets.Scripts.ActionSystem;

public class SlerpRotAction : Action
{
    private float time = 0;
    private float endTime = 0;
    private Quaternion rotTo;


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
