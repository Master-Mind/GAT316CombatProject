using Assets.Scripts;
using Assets.Scripts.ActionSystem;
using FullSerializer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

public class Weapon : MonoBehaviour
{
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
    private Queue<Assets.Scripts.ActionSystem.Action> actionQueue;
    private DealsDamage _damageComp;
    // Use this for initialization
    void Start ()
    {
        transform.localPosition = RestingPos;
        RestingRot = transform.localRotation;
        _actions = GetComponent<ActionSystem>();
        _isResting = true;
        actionQueue = new Queue<Assets.Scripts.ActionSystem.Action>();
        _damageComp = GetComponent<DealsDamage>();
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
	void Update () {
		if(!_actions.IsActive)
        {
            if(actionQueue.Count > 0)
            {
                _actions.AddAction(actionQueue.Dequeue());
            }
            else if(!_isResting)
            {
                ArrayThatWorksForActions group = new ArrayThatWorksForActions();
                group.Add(new SlerpRotAction(gameObject, RestingRot, 0.1f));
                group.Add(new InterpolateAction(gameObject, RestingPos, 0.1f));
                _actions.AddAction(new ActionSequence(gameObject, group));
                _isResting = true;
                _quickIndex = 0;
                _longIndex = 0;
            }
        }

        _damageComp.DealDamageNow = !_isResting && !IsWaiting;
    }

    public void QuickAttack()
    {
        Attack(ref _quickIndex, QuickMoveset);
    }

    public void LongAttack()
    {
        Attack(ref _longIndex, LongMoveset);
    }

    public void ChargeAttack()
    {
        Attack(ref _chargeIndex, ChargeMoveset);
    }

    private void Attack(ref int index, ArrayThatWorksForActions acts)
    {
        if (actionQueue.Count < 2)
        {
            _isResting = false;

            actionQueue.Enqueue(acts[index].Copy());
            index++;
            if (index >= acts.Count())
            {
                index = 0;
            }
        }
    }
}
