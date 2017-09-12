using Assets.Scripts.ActionSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Assets.Scripts.ActionSystem;
[CustomEditor(typeof(Weapon))]
public class WeaponEditor : Editor
{
    List<Type> actTypes = new List<Type>();
    private Weapon _editedWeapon;
    int selected = 0;
    // Use this for initialization
    void OnEnable()
    {
        Type actType = typeof(Assets.Scripts.ActionSystem.Action);
        var types = actType.Assembly.GetTypes();
        
        foreach (var type in types)
        {
            if (type.IsSubclassOf(actType))
            {
                actTypes.Add(type);
            }
        }
        _editedWeapon = (Weapon)target;
        if(_editedWeapon.QuickMoveset == null)
        {
            _editedWeapon.QuickMoveset = new ArrayThatWorks<Assets.Scripts.ActionSystem.Action>();
        }
    }

    // Update is called once per frame
    public override void OnInspectorGUI()
    {
        List<string> labels = new List<string>();
        foreach(var type in actTypes)
        {
            labels.Add(type.Name);
        }
        for(int i = 0; i < _editedWeapon.QuickMoveset.Count(); ++i)
        {
            GUILayout.Label(_editedWeapon.QuickMoveset[i].GetType().Name);
            if(_editedWeapon.QuickMoveset[i].GetType() == typeof(ActionGroup))
            {
                editMultiAction(((ActionGroup)_editedWeapon.QuickMoveset[i])._actionList);
            }
            else if (_editedWeapon.QuickMoveset[i].GetType() == typeof(ActionSequence))
            {
                editMultiAction(((ActionSequence)_editedWeapon.QuickMoveset[i])._actionList);
            }
            else
            {
                editSingleAction(_editedWeapon.QuickMoveset[i]);
            }
            if (GUILayout.Button("Remove"))
            {
                _editedWeapon.QuickMoveset.RemoveAt(i);
                i--;
            }
        }

        selected = EditorGUILayout.Popup("Add Action", selected, labels.ToArray());
        if(GUILayout.Button("Add"))
        {

            _editedWeapon.QuickMoveset.Add((Assets.Scripts.ActionSystem.Action)Activator.CreateInstance(actTypes[selected]));
            _editedWeapon.QuickMoveset[0].myObj = _editedWeapon.gameObject;
        }

    }

    void editMultiAction(ArrayThatWorks<Assets.Scripts.ActionSystem.Action> actionList)
    {
        //TODO: Put shit in this function

    }

    void editSingleAction(Assets.Scripts.ActionSystem.Action action)
    {
        //TODO: Put shit in this function
        Type actType = action.GetType();

        foreach(var member in actType.GetMembers())
        {
            //if the member is not a function
            if(member.MemberType == System.Reflection.MemberTypes.Field)
            {
                GUILayout.Label(member.Name);
                GUILayout.Label(((System.Reflection.FieldInfo)member).FieldType.Name);

            }
        }
    }
}
