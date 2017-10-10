﻿using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using GamepadInput;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject LookAt;
    private float _curRot = 0;
    public float RotateSpeed = 1;
    private GameObject _lockedOnObject = null;
    private CinemachineVirtualCamera _camSettings;
    // Use this for initialization
    void Start ()
    {
        var _cam = GameObject.Find("MainCamera");
        _camSettings = _cam.GetComponent<CinemachineVirtualCamera>();

    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (GamePad.GetButtonDown(GamePad.Button.RightStick, GamePad.Index.One))
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
	    if (_lockedOnObject)
	    {
	        LookAt.transform.position = transform.position + (_lockedOnObject.transform.position - transform.position) / 2;
	        //change how the gosh darn camera is recentered
	        //_camSettings.GetCinemachineComponent<CinemachineOrbitalTransposer>().m_RecenterToTargetHeading.m_HeadingDefinition = CinemachineOrbitalTransposer.Recentering.HeadingDerivationMode.WorldForward;
	    }
	    else
	    {
            //change how the gosh darn camera is recentered
            _camSettings.GetCinemachineComponent<CinemachineOrbitalTransposer>().m_RecenterToTargetHeading.m_HeadingDefinition = CinemachineOrbitalTransposer.Recentering.HeadingDerivationMode.TargetForward;
	        LookAt.transform.localPosition = Vector3.forward * 3.5f;
	        //LookAt.transform.rotation = Quaternion.identity;
            //_curRot += RotateSpeed * GamePad.GetAxis(GamePad.Axis.RightStick, GamePad.Index.One).x;
            //LookAt.transform.RotateAround(LookAt.transform.position - LookAt.transform.localPosition, Vector3.up, _curRot);
        }
    }

    public bool IsLockedOn()
    {
        return _lockedOnObject != null;
    }

    public Transform GetLockedTransform()
    {
        return _lockedOnObject.transform;
    }
}
