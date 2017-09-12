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
    private SerializedProperty _quickMoveset;
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
        _quickMoveset = serializedObject.FindProperty("QuickMoveset");
    }

    // Update is called once per frame
    public override void OnInspectorGUI()
    {
        int selected = 0;
        List<string> labels = new List<string>();
        foreach(var type in actTypes)
        {
            labels.Add(type.Name);
        }

        for(int i = 0; i < _quickMoveset.arraySize; ++i)
        {
            _quickMoveset.GetArrayElementAtIndex(i);
        }

        selected = EditorGUILayout.Popup("Add Action", selected, labels.ToArray());


    }
}
