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
    private int _quickIndex = 0;
    private bool _isResting = false;
    [SerializeField]
    public string _serializedQuickMoves;
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
        fsData data;
        fsSerializer serializer = new fsSerializer();

        if(QuickMoveset != null)
        {
            serializer.TrySerialize(QuickMoveset.GetType(), QuickMoveset, out data);

            _serializedQuickMoves = fsJsonPrinter.CompressedJson(data);
            var foo = (gameObject.name + ".txt");
            var file = new FileStream(foo, FileMode.OpenOrCreate);
            byte[] fuck = System.Text.ASCIIEncoding.ASCII.GetBytes(_serializedQuickMoves);
            file.Write(fuck, 0, _serializedQuickMoves.Length);
        }
    }

    public void FromJSON()
    {
        QuickMoveset = new ArrayThatWorksForActions();
        if(_serializedQuickMoves == null)
        {
            var foo = File.Open(gameObject.name + ".txt", FileMode.Open);
            if(!foo.CanRead)
            {
                return;
            }
            var fuckcSharp = new FileInfo(gameObject.name + ".txt");
            byte[] boots = new byte[fuckcSharp.Length];

            foo.Read(boots, 0, (int)fuckcSharp.Length);

            _serializedQuickMoves = System.Text.ASCIIEncoding.ASCII.GetString(boots);
        }
        fsData data = fsJsonParser.Parse(_serializedQuickMoves);

        fsSerializer serializer = new fsSerializer();
        serializer.TryDeserialize<ArrayThatWorksForActions>(data, ref QuickMoveset);

        resetActObjs(QuickMoveset);
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

            _actions.AddAction(QuickMoveset[_quickIndex]);
        }
    }

    public void LongAttack()
    {

    }
}
