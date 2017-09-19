using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.ActionSystem
{
    [Serializable]
    public class Action
    {
        public GameObject myObj;
        protected Action()
        {
        }
        public virtual void Initialize()
        {

        }
        protected Action(GameObject objectToActOn)
        {
            myObj = objectToActOn;
        }
        
        public virtual bool Execute()
        {
            return false;
        }

        public Action Copy()
        {
            if (GetType() == typeof(ActionSequence))
            {
                return copySeq();
            }
            else if (GetType() == typeof(ActionGroup))
            {
                return copyGroup();
            }
            else
            {
                Type actType = GetType();
                object newAct = Activator.CreateInstance(actType);

                foreach (var member in actType.GetMembers())
                {
                    //if the member is not a function
                    if (member.MemberType == System.Reflection.MemberTypes.Field)
                    {
                        System.Reflection.FieldInfo field = ((System.Reflection.FieldInfo)member);
                        field.SetValue(newAct, (field.GetValue(this)));
                    }
                }

                ((Action)newAct).Initialize();
                return (Action)newAct;
            }
        }

        private ActionSequence copySeq()
        {
            ActionSequence actseq = (ActionSequence)this;
            ActionSequence ret = new ActionSequence();
            ret._actionList = new ArrayThatWorksForActions();
            foreach (Action act in actseq._actionList)
            {
                ret._actionList.Add(act.Copy());
            }

            return ret;
        }
        private ActionGroup copyGroup()
        {

            ActionGroup actseq = (ActionGroup)this;
            ActionGroup ret = new ActionGroup();
            ret._actionList = new ArrayThatWorksForActions();
            foreach (Action act in actseq._actionList)
            {
                ret._actionList.Add(act.Copy());
            }

            return ret;
        }
    }
}
