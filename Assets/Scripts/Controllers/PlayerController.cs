using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using GamepadInput;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private MovementController _move;
    private CombatController _fight;
    private CameraController _camo;
    private GameObject _leLookAtMaTron;
    public float LateralSpeed = 0;
    private bool curRTrigger;
    private GameObject _lockedOnObject = null;

    private GameObject _cam;

    private CinemachineVirtualCamera _camSettings;
    public float TurnSpeed = 50;

    private bool _attacked;
	// Use this for initialization
	void Start ()
	{
	    _move = GetComponent<MovementController>();
        _fight = GetComponent<CombatController>();
        _leLookAtMaTron = GameObject.Find("LOOKATME");
	    _cam = GameObject.Find("MainCamera");
	    _camSettings = _cam.GetComponent<CinemachineVirtualCamera>();
        _camSettings.LookAt = _leLookAtMaTron.transform;
        _camSettings.Follow = transform;
	    _camo = GetComponent<CameraController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //basic movement
        Vector3 movement = new Vector3();

        if (Mathf.Abs(GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y) > 0.2f)
        {
            movement += transform.forward * GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y;
        }
        if (Mathf.Abs(GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).x) > 0.1f)
        {
            if(!_camo.IsLockedOn())
                GetComponent<Transform>().eulerAngles += Vector3.up * Time.deltaTime * TurnSpeed * GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).x;
            else
            {
                transform.LookAt(_camo.GetLockedTransform());
            }
            movement += transform.right * GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).x;
        }

        //Dodge
        if (GamePad.GetButtonDown(GamePad.Button.B, GamePad.Index.One))
        {
            _move.Dodge(movement);
        }
        else
        {
            _move.MoveDir(movement);
        }
        _move.IsSprinting = !_move.isDodging() && GamePad.GetButton(GamePad.Button.B, GamePad.Index.One);
        //Lock on handling

        if (GamePad.GetButtonDown(GamePad.Button.RightShoulder, GamePad.Index.One))
        {
            _fight.ShortAttack();
        }


        var foo = GamePad.GetTrigger(GamePad.Trigger.RightTrigger, GamePad.Index.One) > 0;
        if (foo && !_attacked)
        {
            _fight.LongAttack();
            _attacked = true;
        }
        else if (!foo && _attacked)
        {
            _attacked = false;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            movement += Vector3.up;
        }
        movement.Normalize();
        movement *= LateralSpeed;
    }
}
