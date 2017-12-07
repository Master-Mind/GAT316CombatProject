using Assets.Scripts;
using Assets.Scripts.ActionSystem;
using FullSerializer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using Action = Assets.Scripts.ActionSystem.Action;

public class Weapon : MonoBehaviour
{
    public struct AttackPair
    {
        public Action Act;
        public float Damage;

        public AttackPair(Action act, float damage)
        {
            Act = act;
            Damage = damage;
        }
    }

    public AudioClip QuickHitSound;
    public AudioClip LongHitSound;
    public AudioClip ChargeHitSound;
    public AudioClip CurClip;
    public Vector3 RestingPos;
    public Quaternion RestingRot;
    private ActionSystem _actions;
    [SerializeField]
    public ArrayThatWorksForActions QuickMoveset;
    [SerializeField]
    public ArrayThatWorksForActions LongMoveset;
    [SerializeField]
    public ArrayThatWorksForActions ChargeMoveset;
    private int _quickIndex = 0;
    private int _longIndex = 0;
    private int _chargeIndex = 0;
    public bool _isResting = false;
    public string _serializedQuickMoves;
    public string _serializedLongMoves;
    public string _serializedChargeMoves;
    [NonSerialized]
    public bool IsWaiting = true;
    public string myObjName;
    private Queue<AttackPair> actionQueue;
    private DealsDamage _damageComp;
    private float _quickTim;
    private float _longTim;
    private float _chargeTim;
    public float QuickDamage;
    public float LongDamage;
    public float ChargeDamage;
    public CombatController MyController;
    public bool IsPlayers;
    private TrailRenderer[] _trails;
    private float _restTim = 0.1f;
    // Use this for initialization
    void Start ()
    {
        transform.localPosition = RestingPos;
        RestingRot = transform.localRotation;
        _actions = GetComponent<ActionSystem>();
        _isResting = true;
        actionQueue = new Queue<AttackPair>();
        _damageComp = GetComponent<DealsDamage>();
        IsPlayers = transform.root.gameObject.name == "Player";
        _trails = GetComponentsInChildren<TrailRenderer>();
        Trail(false);
    }
	public void ToJSON()
    {
        toForArray(QuickMoveset, "Quick", ref _serializedQuickMoves);
        toForArray(LongMoveset, "Long", ref _serializedLongMoves);
        toForArray(ChargeMoveset, "Charge", ref _serializedChargeMoves);

    }

    private void toForArray(ArrayThatWorksForActions moves, string moveType, ref string MovesetStr)
    {
        fsData data;
        fsSerializer serializer = new fsSerializer();
        if (moves != null)
        {
            serializer.TrySerialize(moves.GetType(), moves, out data);

            MovesetStr = fsJsonPrinter.PrettyJson(data);
            var foo = (gameObject.name + moveType + ".txt");
            File.WriteAllText(foo, MovesetStr);
        }
    }

    public void FromJSON()
    {
        QuickMoveset = fromForArray(ref _serializedQuickMoves, "Quick");
        LongMoveset = fromForArray(ref _serializedLongMoves, "Long");
        ChargeMoveset = fromForArray(ref _serializedChargeMoves, "Charge");


        resetActObjs(QuickMoveset);
        resetActObjs(LongMoveset);
        resetActObjs(ChargeMoveset);
    }

    private ArrayThatWorksForActions fromForArray(ref string serializedMoves, string setName)
    {
        ArrayThatWorksForActions ret = new ArrayThatWorksForActions();
		
       
            var fuckcSharp = new FileInfo(myObjName + setName + ".txt");

            if (!fuckcSharp.Exists)
            {
                return ret;
            }
            var foo = File.Open(myObjName + setName + ".txt", FileMode.Open);
            byte[] boots = new byte[fuckcSharp.Length];

            foo.Read(boots, 0, (int)fuckcSharp.Length);
            foo.Close();
            serializedMoves = System.Text.ASCIIEncoding.ASCII.GetString(boots);
       
        if(serializedMoves != "")
        {

            fsData data = fsJsonParser.Parse(serializedMoves);

            fsSerializer serializer = new fsSerializer();
            serializer.TryDeserialize<ArrayThatWorksForActions>(data, ref ret);
        }

        return ret;
    }

    public void resetActObjs(ArrayThatWorksForActions actArray)
    {
        foreach (Assets.Scripts.ActionSystem.Action move in actArray)
        {
            if(move.myObj != gameObject)
            {
                DestroyImmediate(move.myObj);
            }
            move.myObj = gameObject;
            move.Initialize();
            if(move.GetType() == typeof(ActionGroup))
            {
                resetActObjs(((ActionGroup)move)._actionList);
            }
            else if (move.GetType() == typeof(ActionSequence))
            {
                resetActObjs(((ActionSequence)move)._actionList);
            }
        }
    }
	// Update is called once per frame
	void Update ()
    {
        _isResting = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Rest");
        if (!IsPlayers)
        {
            return;
        }
        if (_quickIndex > 0)
	    {
	        _quickIndex--;
	        _quickTim = 0.25f;
	    }
        if (_longIndex > 0)
        {
            _longIndex--;
            _longTim = 0.25f;
        }
        if (_chargeIndex > 0)
        {
            _chargeIndex--;
            _chargeTim = 0.25f;
        }
        GetComponent<Animator>().SetBool("QuickAttack", _quickTim > 0);
        GetComponent<Animator>().SetBool("LongAttack", _longTim > 0);
        GetComponent<Animator>().SetBool("ChargeAttack", _chargeTim > 0);
        _quickTim -= Time.deltaTime;
        _longTim -= Time.deltaTime;
        _chargeTim -= Time.deltaTime;
        AttackAnimBehave behave = null;
        //if(gameObject.name.Contains("Falx"))
        //Debug.Log(_damageComp.Damage);
        //if (!_actions.IsActive)
        //{
        //    
        //    if(actionQueue.Count > 0)
        //    {
        //        var temp = actionQueue.Dequeue();
        //        _actions.AddAction(temp.Act);
        //        _damageComp.SetDamage(temp.Damage);
        //    }
        //    else if(!_isResting)
        //    {
        //        _damageComp.SetDamage(0);
        //        ArrayThatWorksForActions group = new ArrayThatWorksForActions();
        //        group.Add(new SlerpRotAction(gameObject, RestingRot, 0.1f));
        //        group.Add(new InterpolateAction(gameObject, RestingPos, 0.1f));
        //        _actions.AddAction(new ActionSequence(gameObject, group));
        //        _isResting = true;
        //        _quickIndex = 0;
        //        _longIndex = 0;
        //    }
        //}

        _damageComp.DealDamageNow = !_isResting;
    }

    public void PlaySound(AudioClip clip)
    {
        GetComponent<AudioSource>().PlayOneShot(clip);
    }
    public void PlaySound(AudioClip clip, float pitch)
    {
        GetComponent<AudioSource>().PlayOneShot(clip);
    }
    public void SetHitClip(AudioClip clip)
    {
        CurClip = clip;
    }

    public void Trail(bool active)
    {
        foreach (var trail in _trails)
        {
            trail.gameObject.SetActive(active);
        }
    }
    public void QuickAttack()
    {
        //Attack(ref _quickIndex, QuickMoveset, QuickDamage);
        _quickIndex++;
    }

    public void CloseDistance(float distance)
    {
        MyController.CloseDistance(distance);
    }

    public void SetShake(int setTo)
    {
        FindObjectOfType<CameraController>().CameraShake = setTo != 0;
    }

    public void JumpBack()
    {
        MyController.GetComponent<MovementController>().Dodge(-transform.forward);
    }
    public void LongAttack()
    {
        //Attack(ref _longIndex, LongMoveset, LongDamage);
        _longIndex++;
    }

    public void ChargeAttack()
    {
        //Attack(ref _chargeIndex, ChargeMoveset, ChargeDamage);
        _chargeIndex++;
    }

    public void Attack(string triggerName)
    {
        GetComponent<Animator>().SetTrigger(triggerName);
    }

    public void ForceRest()
    {
        GetComponent<Animator>().SetTrigger("Stagger");
    }

    public void SetStaggerable(int staggerable)
    {

        transform.root.GetComponent<Health>().CanStagger = staggerable != 0;
    }
}
