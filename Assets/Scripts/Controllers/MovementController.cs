using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private CharacterController _charControl = null;

    public float Gravity = 0;
    public float Friction = 0;

    private Vector3 _curVelocity = new Vector3();
    private Vector3 _curMove = new Vector3();
    private ActionSystem _actions = null;
    private float _effectiveRotation = 0;
    private bool _isDodging;
    // Use this for initialization
    void Start()
    {
        _charControl = GetComponent<CharacterController>();
        _actions = GetComponent<ActionSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_isDodging)
        {
            if(!_actions.IsActive)
            {
                _isDodging = false;
            }
            else
            {
                return;
            }
        }
        Vector3 curFriction = new Vector3();
        if (_charControl.isGrounded)
        {
            curFriction = _charControl.velocity * -Friction;
        }
        else
        {
            _curMove = new Vector3();
        }
        Vector3 moveComposed = Vector3.down * Gravity + _curMove + curFriction;

        _curVelocity += moveComposed;


        _charControl.Move(_curVelocity * Time.deltaTime);

        _curMove = new Vector3();
    }

    public void MoveDir(Vector3 move)
    {
        _curMove = move;
    }

    public void Dodge(Vector3 dir)
    {
        _isDodging = true;

        _actions.AddAction(new DodgeAction(gameObject, dir, 3, 0.3f));
    }

    public void Rotate(float rotation)
    {
        _effectiveRotation += rotation;
    }
}
