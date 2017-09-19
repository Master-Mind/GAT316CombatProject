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
    public ArrayThatWorksForActions LongMoveset;
    private int _quickIndex = 0;
    private bool _isResting = false;
    [SerializeField]
    public string _serializedQuickMoves;
    [SerializeField]
    public string _serializedLongMoves;
    // Use this for initialization
    void Start ()
    {
        transform.localPosition = RestingPos;
        RestingRot = transform.localRotation;
        _actions = GetComponent<ActionSystem>();
        _isResting = true;
        FromJSON();
        //ArrayList group = new ArrayList();
        //ArrayList seq = new ArrayList();
        //
        //
        //group.Add(new SlerpRotAction(gameObject, Quaternion.AngleAxis(90, Vector3.forward), 0.1f));
        //group.Add(new InterpolateAction(gameObject, Vector3.right, 0.1f));
        //seq.Add(new ActionGroup(gameObject, group));
        //seq.Add(new SlerpAboutAction(gameObject, gameObject, 0.2f, (-180)));
        //seq.Add(new WaitAction(gameObject, 0.5f));
        //QuickMoveset = new Action;
        //QuickMoveset[0] = new ActionSequence(gameObject, seq);
    }
	public void ToJSON()
    {
        toForArray(QuickMoveset, "Quick", ref _serializedQuickMoves);
        toForArray(LongMoveset, "Long", ref _serializedLongMoves);

    }

    private void toForArray(ArrayThatWorksForActions moves, string moveType, ref string MovesetStr)
    {
        fsData data;
        fsSerializer serializer = new fsSerializer();
        if (moves != null)
        {
            serializer.TrySerialize(moves.GetType(), moves, out data);

            MovesetStr = fsJsonPrinter.CompressedJson(data);
            var foo = (gameObject.name + moveType + ".txt");
            var file = new FileStream(foo, FileMode.OpenOrCreate);
            byte[] fuck = System.Text.ASCIIEncoding.ASCII.GetBytes(_serializedQuickMoves);
            file.Write(fuck, 0, _serializedQuickMoves.Length);
        }
    }

    public void FromJSON()
    {
        QuickMoveset = fromForArray(ref _serializedQuickMoves, "Quick");
        LongMoveset = fromForArray(ref _serializedLongMoves, "Long");


        resetActObjs(QuickMoveset);
    }

    private ArrayThatWorksForActions fromForArray(ref string serializedMoves, string setName)
    {
        ArrayThatWorksForActions ret = new ArrayThatWorksForActions();
        if(setName == "Long")
        {
            return ret;
        }
        if (serializedMoves == null)
        {
            var fuckcSharp = new FileInfo(gameObject.name + setName + ".txt");

            if (!fuckcSharp.Exists)
            {
                return ret;
            }
            var foo = File.Open(gameObject.name + ".txt", FileMode.Open);
            byte[] boots = new byte[fuckcSharp.Length];

            foo.Read(boots, 0, (int)fuckcSharp.Length);

            serializedMoves = System.Text.ASCIIEncoding.ASCII.GetString(boots);
        }
        if(serializedMoves != "")
        {

            fsData data = fsJsonParser.Parse(serializedMoves);

            fsSerializer serializer = new fsSerializer();
            serializer.TryDeserialize<ArrayThatWorksForActions>(data, ref ret);
        }

        return ret;
    }

    private void resetActObjs(ArrayThatWorksForActions actArray)
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
		if(!_actions.IsActive && !_isResting)
        {
            //ArrayThatWorks<Action> group = new ArrayThatWorks<Action>();
            //group.Add(new SlerpRotAction(gameObject, RestingRot, 0.1f));
            //group.Add(new InterpolateAction(gameObject, RestingPos, 0.1f));
            //_actions.AddAction(new ActionSequence(gameObject, group));
            //_isResting = true;
        }
	}

    public void QuickAttack()
    {
        if (!_actions.IsActive)
        {
            _isResting = false;

            _actions.CopyAction(QuickMoveset[_quickIndex]);
        }
    }

    public void LongAttack()
    {

    }
}
