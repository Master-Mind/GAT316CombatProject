using Assets.Scripts;
using Assets.Scripts.ActionSystem;
using FullSerializer;
using System;
using System.Collections;
using System.Collections.Generic;
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
    private string _serializedQuickMoves;
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
        fsData data;
        fsSerializer serializer = new fsSerializer();

        serializer.TrySerialize(QuickMoveset.GetType(), QuickMoveset, out data);

        //string ret = "";
        //char curDepth = (char)1;
        //foreach(Assets.Scripts.ActionSystem.Action move in QuickMoveset)
        //{
        //    ret += move.GetType().Name + serializer + curDepth;
        //}

        _serializedQuickMoves = fsJsonPrinter.PrettyJson(data);
    }

    public void FromJSON()
    {
        fsData data = fsJsonParser.Parse(_serializedQuickMoves);

        fsSerializer serializer = new fsSerializer();
        serializer.TryDeserialize<ArrayThatWorksForActions>(data, ref QuickMoveset);
        //char curDepth = (char)1;
        //string[] actionStrs = _serializedQuickMoves.Split(curDepth);
        //Dictionary<string, Type> typeDic = new Dictionary<string, Type>();
        //foreach(var type in typeof(Assets.Scripts.ActionSystem.Action).Assembly.GetTypes())
        //{
        //    if(type.IsSubclassOf(typeof(Assets.Scripts.ActionSystem.Action)))
        //    {
        //        typeDic.Add(type.Name, type);
        //    }
        //}
        //if(QuickMoveset == null)
        //{
        //    QuickMoveset = new ArrayThatWorksForActions();
        //}
        //else
        //{
        //    QuickMoveset.Clear();
        //}
        //
        //foreach(var str in actionStrs)
        //{
        //    if(str == "")
        //    {
        //        continue;
        //    }
        //    else
        //    {
        //        int splitter = str.IndexOf('{');
        //        string typename = str.Substring(0, splitter);
        //        string data = str.Substring(splitter);
        //        var fu = ScriptableObject.CreateInstance(typeDic[typename]);
        //        JsonUtility.FromJsonOverwrite(str, fu);
        //        QuickMoveset.Add((Assets.Scripts.ActionSystem.Action)fu);
        //    }
        //}
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
