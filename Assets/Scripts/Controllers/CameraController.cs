using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject LookAt;
    public GameObject LockDisplay;
    private float _curRot = 0;
    public float RotateSpeed = 1;
    private GameObject _lockedOnObject = null;
    private CinemachineVirtualCamera _camSettings;
    public float CameraShakeStrength = 1;
    [HideInInspector]
    public bool CameraShake = false;
    
    // Use this for initialization
    void Start ()
    {
        var _cam = GameObject.Find("MainCamera");
        _camSettings = _cam.GetComponent<CinemachineVirtualCamera>();
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (GetComponent<Health>().MARKEDFORDEATH)
	    {
	        _camSettings.LookAt = null;
	        return;
	    }
	    if (GetComponent<PlayerController>().MyController.GetButtonPressed(Controller.Button.RightStick))
	    {
	        if (!_lockedOnObject)
	        {
                LockOn();
	        }
	        else
	        {
	            _lockedOnObject = null;
	        }
	    }
	    if (_lockedOnObject)
	    {
	        LookAt.transform.position = transform.position + (_lockedOnObject.transform.position - transform.position) / 2;

	        LockDisplay.transform.position = _lockedOnObject.transform.position;
	        _camSettings.GetCinemachineComponent<CinemachineOrbitalTransposer>().m_HeadingBias = 3;

	    }
	    else
	    {
	        LockDisplay.transform.localPosition = Vector3.zero;
            //change how the gosh darn camera is recentered
            _camSettings.GetCinemachineComponent<CinemachineOrbitalTransposer>().m_RecenterToTargetHeading.m_HeadingDefinition = CinemachineOrbitalTransposer.Recentering.HeadingDerivationMode.TargetForward;
	        LookAt.transform.localPosition = Vector3.forward * 3.5f;
	        LookAt.transform.rotation = Quaternion.identity;
	        float xRot = GetComponent<PlayerController>().MyController.GetAxis(false).x;
            if(Mathf.Abs(xRot) > 0.01f || GetComponent<PlayerController>().MyController.GetAxis(true).sqrMagnitude < 0.1f)
                _curRot += RotateSpeed * GetComponent<PlayerController>().MyController.GetAxis(false).x;
            else
            {
                _curRot -= _curRot * Time.deltaTime * RotateSpeed;
            }
            LookAt.transform.RotateAround(transform.position, Vector3.up, _curRot);
	        _camSettings.GetCinemachineComponent<CinemachineOrbitalTransposer>().m_HeadingBias = _curRot;
        }
        
        if (CameraShake)
	    {
	        _camSettings.GetCinemachineComponent<CinemachineComposer>().m_TrackedObjectOffset = Random.onUnitSphere * CameraShakeStrength;
	    }
	    else
	    {
	        _camSettings.GetCinemachineComponent<CinemachineComposer>().m_TrackedObjectOffset = Vector3.zero;
	    }
    }

    public Vector3 GetForward()
    {
        return (LookAt.transform.position - transform.position).normalized;
    }

    public float GetAngle()
    {
        return _camSettings.transform.eulerAngles.y;
    }
    public bool IsLockedOn()
    {
        return _lockedOnObject != null;
    }

    public Transform GetLockedTransform()
    {
        return _lockedOnObject.transform;
    }

    public void LockOn()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float minAngle = float.MaxValue;
        foreach (var enemy in enemies)
        {
            var toVec = enemy.transform.position - transform.position;
            var foo = Vector3.Angle(toVec, transform.forward);
            float angle = foo * foo + toVec.sqrMagnitude;
            if (angle < minAngle && Physics.Raycast(new Ray(transform.position, toVec)))
            {
                minAngle = angle;
                _lockedOnObject = enemy;
            }
        }
    }
}
