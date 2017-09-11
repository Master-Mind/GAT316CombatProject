using UnityEngine;
using System.Collections;
using Assets.Scripts.ActionSystem;

public class DodgeAction : Action
{
    private float time = 0;
    private float endTime = 0;
    private float _speed = 0;

    private Vector3 _dir;
    private CharacterController _charControl = null;

    public override bool Execute()
    {
        time += Time.deltaTime;

        _charControl.Move(_dir * Time.deltaTime / endTime * _speed);

        return time >= endTime;
    }

    public DodgeAction(GameObject objectToActOn, Vector3 dir,float speed, float endTime) : base(objectToActOn)
    {
        _charControl = objectToActOn.GetComponent<CharacterController>();
        _speed = speed;
        this._dir = dir;
        this.endTime = endTime;
    }
}
