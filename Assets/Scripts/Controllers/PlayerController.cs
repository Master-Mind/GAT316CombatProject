using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;
public class PlayerController : MonoBehaviour
{
    private MovementController _move;
    private CombatController _fight;
    private CameraController _camo;
    private GameObject _leLookAtMaTron;
    public float LateralSpeed = 0;
    private bool curRTrigger;
    private GameObject _lockedOnObject = null;
    private bool _cheatMode;
    private GameObject _cam;
    private GameObject _lose;
    private GameObject _end;
    private GameObject _pause;
    private Text _tutText;
    private GameObject _hafsdjhfdalskhf;
    private CinemachineVirtualCamera _camSettings;
    public Controller MyController;
    public float TurnSpeed = 50;
    [HideInInspector] public bool Stap;
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
	    _lose = GameObject.Find("LosePan");
	    _end = GameObject.Find("EndPanel");
	    _pause = GameObject.Find("PausePanel");
        _lose.SetActive(false);
	    _pause.SetActive(false);
	    _tutText = GameObject.Find("TutText").GetComponent<Text>();
	    _tutText.text = "";
	    _hafsdjhfdalskhf = GameObject.Find("TutorialZones");
	    MyController = new Controller();

        MyController.Init();
    }

    Vector3 GetForward()
    {
        return _camo.GetForward();
    }

    Vector3 GetRight()
    {
        return Quaternion.AngleAxis(90, Vector3.up) *_camo.GetForward();
    }
    // Update is called once per frame
    void Update ()
    {
        MyController.Update();
        if (GetComponent<Health>().MARKEDFORDEATH)
        {
            return;
        }
        if (Stap)
        {
            return;
        }
        //basic movement
        Vector3 movement = new Vector3();

        if (Mathf.Abs(MyController.GetAxis(true).y) > 0.2f)
        {
            //movement += GetForward() * GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y;
        }
        
        
        if (!_camo.IsLockedOn())
        {
            movement += Quaternion.AngleAxis(_camo.GetAngle(), Vector3.up) *
                        new Vector3(MyController.GetAxis(true).x, 0, MyController.GetAxis(true).y);
            //var foof = new Vector3(0,
            //    Mathf.Rad2Deg * Mathf.Acos(GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).x));
            //transform.eulerAngles = foof;
            if (movement.sqrMagnitude > 0.1)
            {
                if (MyController.GetAxis(true).y > 0)
                {
                    transform.LookAt(transform.position + movement);
                }
                else
                {
                    transform.LookAt(transform.position + movement);
                }
            }
        }
        else
        {
            transform.LookAt(_camo.GetLockedTransform());

            movement += transform.forward * MyController.GetAxis(true).y +
                        transform.right * MyController.GetAxis(true).x;
        }
        //movement += GetRight() * GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).x;
        if (Input.GetKeyDown(KeyCode.F1))
        {
            _cheatMode = !_cheatMode;
            GetComponent<MovementController>().enabled = !_cheatMode;
            GetComponent<CharacterController>().enabled = !_cheatMode;
        }

        //Dodge
        if (MyController.GetButtonPressed(Controller.Button.B))
        {
            _move.Dodge(movement);
        }
        else
        {
            if (_cheatMode)
            {
                transform.position += movement;
            }
            else
            {
                _move.MoveDir(movement);
            }
        }
        _move.IsSprinting = !_move.isDodging() && MyController.GetButtonDown(Controller.Button.B);
        //Lock on handling

        if (MyController.GetButtonPressed(Controller.Button.RightBumper))
        {
            _fight.ShortAttack();
        }
        if (MyController.GetButtonPressed(Controller.Button.Y))
        {
            _fight.NextWeapon();
        }

        var foo = MyController.GetTrigger(false) > 0;
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
            //movement += Vector3.up;
        }
        movement.Normalize();
        movement *= LateralSpeed;
        if (MyController.GetButtonPressed(Controller.Button.Start))
        {
            Time.timeScale = 0;
            _pause.SetActive(true);
            GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(GameObject.Find("Resume"));
        }
    }

    void OnDestroy()
    {
        _lose.SetActive(true);
        _end.SetActive(true);
        GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(GameObject.Find("Restart"));
        MyController.Stap();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Checkpoint>() != null)
        {
            Checkpoint.ShouldThePlayerBe = other.GetComponent<Checkpoint>().AmI;
            GetComponent<Health>().HealFull();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform.parent != null && other.transform.parent == _hafsdjhfdalskhf.transform && _tutText.text != other.name)
        {
            _tutText.text = other.name;
            _tutText.GetComponent<TutText>().ComeOut();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.transform.parent != null && other.transform.parent == _hafsdjhfdalskhf.transform)
        {
            _tutText.GetComponent<TutText>().GoBack();
        }
    }
}
