using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class Stagger : Assets.Scripts.ActionSystem.Action
{
    private float _endTime;
    private float _curTime;
    private float _endAngle;
    private Quaternion origRot;

    public Stagger(float endTime, float endAngle, GameObject objToActOn) : base(objToActOn)
    {
        _endTime = endTime;
        _endAngle = endAngle;
        _curTime = 0;
        origRot = myObj.transform.rotation;
    }

    public override bool Execute()
    {
        _curTime += Time.deltaTime;
        if (myObj.name == "Enemy")
        {
            Debug.Log(myObj.transform.rotation);
        }
        myObj.transform.rotation = origRot * Quaternion.AngleAxis(_endAngle * Mathf.Sin(Mathf.PI * (_curTime / _endTime)), myObj.transform.right);
        if (myObj.name == "Enemy")
        {
            Debug.Log(myObj.transform.rotation);
        }
        if (_curTime >= _endTime)
        {
            myObj.transform.rotation = origRot;
        }
        return _curTime >= _endTime;
    }

    public override void Initialize()
    {
    }
}