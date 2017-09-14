using Assets.Scripts.ActionSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Assets.Scripts.ActionSystem;
using Assets.Scripts;

[CustomEditor(typeof(Weapon))]
public class WeaponEditor : Editor
{
    List<Type> actTypes = new List<Type>();
    private Weapon _editedWeapon;
    private SerializedProperty _editedMoveset;
    int selected = 0;
    private List<bool> _foldoutBools;
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
        _editedWeapon = (Weapon) target;
        _editedWeapon.FromJSON();
        _editedMoveset = serializedObject.FindProperty("QuickMoveset");
        //_editedWeapon.QuickMoveset = new ArrayThatWorksForActions();
        _foldoutBools = new List<bool>();
        for (int i = 0; i < _editedWeapon.QuickMoveset.Count(); ++i)
        {
            _foldoutBools.Add(false);
        }
    }
    class unityisforfuckbois
    {
        public int foo = 0;
    }
    private void OnDisable()
    {
        _editedWeapon.ToJSON();
    }
    // Update is called once per frame
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        List<string> labels = new List<string>();
        foreach(var type in actTypes)
        {
            labels.Add(type.Name);
        }
        for(int i = 0; i < _editedWeapon.QuickMoveset.Count(); ++i)
        {
            if(EditorGUILayout.Foldout(_foldoutBools[i], _editedWeapon.QuickMoveset[i].GetType().Name))
            {
                _foldoutBools[i] = true;
                if (_editedWeapon.QuickMoveset[i].GetType() == typeof(ActionGroup))
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
            else
            {
                _foldoutBools[i] = false;
            }
            //GUILayout.Label(_editedMoveset.GetArrayElementAtIndex(i).GetType().Name);
            
        }

        selected = EditorGUILayout.Popup("Add Action", selected, labels.ToArray());
        if(GUILayout.Button("Add"))
        {
            
            _editedWeapon.QuickMoveset.Add((Assets.Scripts.ActionSystem.Action)CreateInstance(actTypes[selected]));
            _foldoutBools.Add(false);
            _editedWeapon.QuickMoveset[0].myObj = _editedWeapon.gameObject;
        }
        serializedObject.ApplyModifiedProperties();
    }

    void editMultiAction(ArrayThatWorks<Assets.Scripts.ActionSystem.Action> actionList)
    {
        //TODO: Put shit in this function

    }

    void editSingleAction(Assets.Scripts.ActionSystem.Action action)
    {
        //TODO: Put shit in this function
        Type actType = action.GetType();
        foreach (var member in actType.GetMembers())
        {
            //if the member is not a function
            if(member.MemberType == System.Reflection.MemberTypes.Field)
            {
                System.Reflection.FieldInfo field = ((System.Reflection.FieldInfo)member);
                if (field.FieldType == typeof(Single))
                {
                    field.SetValue(action,EditorGUILayout.FloatField(member.Name, (float)(field.GetValue(action))));
                }
                else if (field.FieldType == typeof(Vector2))
                {
                    field.SetValue(action, EditorGUILayout.Vector2Field(member.Name, (Vector2)(field.GetValue(action))));
                }
                else if (field.FieldType == typeof(Vector3))
                {
                    field.SetValue(action, EditorGUILayout.Vector3Field(member.Name, (Vector3)(field.GetValue(action))));
                }
                else if (field.FieldType == typeof(string))
                {
                    field.SetValue(action, EditorGUILayout.TextField(member.Name, (string)(field.GetValue(action))));
                }
                else if (field.FieldType == typeof(Quaternion))
                {
                    string RotationScript = "";

                    RotationScript = EditorGUILayout.TextField(member.Name, RotationScript);

                    RotationParser rotPar = new RotationParser(RotationScript);

                    field.SetValue(action,rotPar.finalRotation);
                }
                else
                {
                    GUILayout.Label(member.Name + " is of type " + field.FieldType.Name + " which has not been reflected yet");
                }

            }
        }
    }
}
