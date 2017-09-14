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
        protected Action(GameObject objectToActOn)
        {
            myObj = objectToActOn;
        }
        
        public virtual bool Execute()
        {
            return false;
        }
    }
}
