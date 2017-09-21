﻿using Assets.Scripts;
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
    private int _quickIndex = 0;
    private bool _isResting = false;
    private string _serializedQuickMoves;
    private string _serializedLongMoves;
    public bool IsWaiting = false;
    public string myObjName;
    // Use this for initialization
    void Start ()
    {
        transform.localPosition = RestingPos;
        RestingRot = transform.localRotation;
        _actions = GetComponent<ActionSystem>();
        _isResting = true;


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

            MovesetStr = fsJsonPrinter.PrettyJson(data);
            var foo = (gameObject.name + moveType + ".txt");
            var file = new FileStream(foo, FileMode.OpenOrCreate);
            byte[] fuck = System.Text.ASCIIEncoding.ASCII.GetBytes(MovesetStr);
            file.Write(fuck, 0, MovesetStr.Length);
            file.Close();
        }
    }

    public void FromJSON()
    {
        QuickMoveset = fromForArray(ref _serializedQuickMoves, "Quick");
        LongMoveset = fromForArray(ref _serializedLongMoves, "Long");


        resetActObjs(QuickMoveset);
        resetActObjs(LongMoveset);
    }

    private ArrayThatWorksForActions fromForArray(ref string serializedMoves, string setName)
    {
        ArrayThatWorksForActions ret = new ArrayThatWorksForActions();
		
        if (serializedMoves == null)
        {
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
        }
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
		if(!_actions.IsActive && !_isResting)
        {
            ArrayThatWorksForActions group = new ArrayThatWorksForActions();
            group.Add(new SlerpRotAction(gameObject, RestingRot, 0.1f));
            group.Add(new InterpolateAction(gameObject, RestingPos, 0.1f));
            _actions.AddAction(new ActionSequence(gameObject, group));
            _isResting = true;
            _quickIndex = 0;

        }
	}

    public void QuickAttack()
    {
        if (!_actions.IsActive || IsWaiting)
        {
            _isResting = false;

            _actions.CopyAction(QuickMoveset[_quickIndex]);
            _quickIndex++;
            if(_quickIndex >= QuickMoveset.Count())
            {
                _quickIndex = 0;
            }
        }
    }

    public void LongAttack()
    {

    }
}
