using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Vector3 RestingPos;
    private ActionSystem _actions;
    public
	// Use this for initialization
	void Start ()
    {
        transform.localPosition = RestingPos;
        _actions = GetComponent<ActionSystem>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void QuickAttack()
    {
        if (!_actions.IsActive)
        {
            ArrayList group = new ArrayList();
            ArrayList seq = new ArrayList();
            int scaler = 1;
            if (gameObject.transform.localPosition.x < 0)
            {
                scaler = -1;
            }

            group.Add(new SlerpRotAction(gameObject, Quaternion.AngleAxis(90, Vector3.forward), 0.1f));
            group.Add(new InterpolateAction(gameObject, Vector3.right * scaler, 0.1f));
            seq.Add(new ActionGroup(gameObject, group));
            seq.Add(new SlerpAboutAction(gameObject, gameObject, 0.2f, (-180) * scaler));


            _actions.AddAction(new ActionSequence(gameObject, seq));
        }
    }

    public void LongAttack()
    {

    }
}
