using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private MovementController _move;
    private CombatController _fight;
    private GameObject _leLookAtMaTron;
    public float LateralSpeed = 0;

    private GameObject _lockedOnObject = null;

    private GameObject _cam;

    private CinemachineVirtualCamera _camSettings;
    public float TurnSpeed = 50;
	// Use this for initialization
	void Start ()
	{
	    _move = GetComponent<MovementController>();
        _fight = GetComponent<CombatController>();
        _leLookAtMaTron = GameObject.Find("LOOKATME");
	    _cam = GameObject.Find("MainCamera");
	    _camSettings = _cam.GetComponent<CinemachineVirtualCamera>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //basic movement
        Vector3 movement = new Vector3();

        if (Mathf.Abs(Input.GetAxis("MoveVertical")) > 0.2f)
        {
            movement -= transform.forward * Input.GetAxis("MoveVertical");
        }
        if (Mathf.Abs(Input.GetAxis("MoveHorizontal")) > 0.1f)
        {
            if(!_lockedOnObject)
                GetComponent<Transform>().eulerAngles += Vector3.up * Time.deltaTime * TurnSpeed * Input.GetAxis("MoveHorizontal");
            else
            {
                transform.LookAt(_lockedOnObject.transform);
            }
            movement += transform.right * Input.GetAxis("MoveHorizontal");
        }
        
        //Lock on handling
        if (Input.GetKeyDown(KeyCode.Joystick1Button9))
        {
            if (!_lockedOnObject)
            {
                var enemies = GameObject.FindGameObjectsWithTag("Enemy");
                _lockedOnObject = enemies[0];
            }
            else
            {
                _lockedOnObject = null;
            }
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            _fight.ShortAttack();
        }

        if (_lockedOnObject)
        {
            _leLookAtMaTron.transform.position = _lockedOnObject.transform.position;
            //change how the gosh darn camera is recentered
            //_camSettings.GetCinemachineComponent<CinemachineOrbitalTransposer>().m_RecenterToTargetHeading.m_HeadingDefinition = CinemachineOrbitalTransposer.Recentering.HeadingDerivationMode.WorldForward;
        }
        else
        {
            //change how the gosh darn camera is recentered
            //_camSettings.GetCinemachineComponent<CinemachineOrbitalTransposer>().m_RecenterToTargetHeading.m_HeadingDefinition = CinemachineOrbitalTransposer.Recentering.HeadingDerivationMode.TargetForward;
            _leLookAtMaTron.transform.localPosition = Vector3.forward * 3.5f;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            movement += Vector3.up;
        }
        movement.Normalize();
        movement *= LateralSpeed;
        //Dodge
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            _move.Dodge(movement);
        }
        else
        {
            _move.MoveDir(movement);
        }
    }
}
