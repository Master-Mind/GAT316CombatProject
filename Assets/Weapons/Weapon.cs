using Assets.Scripts;
using Assets.Scripts.ActionSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Vector3 RestingPos;
    public Quaternion RestingRot;
    private ActionSystem _actions;
    public ArrayThatWorks<Action> QuickMoveset;
    private int _quickIndex = 0;
    private bool _isResting = false;
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
	
	// Update is called once per frame
	void Update () {
		if(!_actions.IsActive && !_isResting)
        {
            ArrayList group = new ArrayList();
            group.Add(new SlerpRotAction(gameObject, RestingRot, 0.1f));
            group.Add(new InterpolateAction(gameObject, RestingPos, 0.1f));
            _actions.AddAction(new ActionSequence(gameObject, group));
            _isResting = true;
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
