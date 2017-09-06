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

    private float _effectiveRotation = 0;
    // Use this for initialization
    void Start()
    {
        _charControl = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 curFriction = new Vector3();
        if (_charControl.isGrounded)
        {
            curFriction = _charControl.velocity * -Friction;
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


    public void Rotate(float rotation)
    {
        _effectiveRotation += rotation;
    }
}
