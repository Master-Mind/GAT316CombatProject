using Assets.Scripts.ActionSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Assets.Scripts.ActionSystem;
using Assets.Scripts;
using System.IO;

[CustomEditor(typeof(Weapon))]
public class WeaponEditor : Editor
{
    List<Type> actTypes = new List<Type>();
    private Weapon _editedWeapon;
    private SerializedProperty _editedMoveset;
    int selected = 0;
    int selectedWep = 0;
    private List<bool> _foldoutBools;
    List<string> WeaponTypes;
    // Use this for initialization
    void OnEnable()
    {
        Type actType = typeof(Assets.Scripts.ActionSystem.Action);
        var types = actType.Assembly.GetTypes();
        WeaponTypes = ScanForWeapons();
        foreach (var type in types)
        {
            if (type.IsSubclassOf(actType))
            {
                actTypes.Add(type);
            }
        }
        _editedWeapon = (Weapon) target;
        _editedWeapon.myObjName = _editedWeapon.gameObject.name;
        
        _editedWeapon.FromJSON();
        _editedMoveset = serializedObject.FindProperty("QuickMoveset");
        //_editedWeapon.QuickMoveset = new ArrayThatWorksForActions();
        _foldoutBools = new List<bool>();
        for (int i = 0; i < _editedWeapon.QuickMoveset.Count(); ++i)
        {
            _foldoutBools.Add(false);
        }

        _editedWeapon.resetActObjs(_editedWeapon.QuickMoveset);
        _editedWeapon.resetActObjs(_editedWeapon.LongMoveset);
    }

    private List<string> ScanForWeapons()
    {
        List<string> ret = new List<string>();

        ret.AddRange(System.IO.Directory.GetFiles(Environment.CurrentDirectory, "*k.txt"));

        for(int i = 0; i < ret.Count; ++i)
        {
            var foo = System.IO.Path.GetFileName(ret[i]);
            ret[i] = foo.Substring(0, foo.LastIndexOf('Q'));
        }

        return ret;
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
        //update the properties of the object
        serializedObject.Update();
        var field = typeof(Weapon).GetField("RestingPos");
        field.SetValue((Weapon)target, EditorGUILayout.Vector3Field(field.Name, (Vector3)field.GetValue((Weapon)target)));
        //Copying
        EditorGUILayout.BeginHorizontal();
        selectedWep = EditorGUILayout.Popup("Copy From", selectedWep, WeaponTypes.ToArray());
        if (GUILayout.Button("Copy Moveset", GUILayout.Width(100f)))
        {
            LoadInto();
        }
        EditorGUILayout.EndHorizontal();
        //Clearing
        if (GUILayout.Button("CLEAR ALL NO TOUCHY", GUILayout.Width(500f)))
        {
            _editedWeapon.QuickMoveset.Clear();
            _editedWeapon.LongMoveset.Clear();
        }
        //get the labels for each type of action
        List<string> labels = new List<string>();
        foreach(var type in actTypes)
        {
            labels.Add(type.Name);
        }
        //edit the moveset recursively
        editMultiAction(_editedWeapon.QuickMoveset, "Quick moveset",20);
        editMultiAction(_editedWeapon.LongMoveset, "Long Moveset", 20);

        selected = EditorGUILayout.Popup("Add Action", selected, labels.ToArray());
        serializedObject.ApplyModifiedProperties();
    }

    private void LoadInto()
    {
        {
            var fuckcSharp = new FileInfo(WeaponTypes[selectedWep] + "Quick" + ".txt");

            if (!fuckcSharp.Exists)
            {
                _editedWeapon.QuickMoveset = new ArrayThatWorksForActions();
            }
            var foo = File.Open(WeaponTypes[selectedWep] + "Quick" + ".txt", FileMode.Open);
            byte[] boots = new byte[fuckcSharp.Length];

            foo.Read(boots, 0, (int)fuckcSharp.Length);
            foo.Close();
            _editedWeapon._serializedQuickMoves = System.Text.ASCIIEncoding.ASCII.GetString(boots);

            if (_editedWeapon._serializedQuickMoves != "")
            {

                FullSerializer.fsData data = FullSerializer.fsJsonParser.Parse(_editedWeapon._serializedQuickMoves);

                FullSerializer.fsSerializer serializer = new FullSerializer.fsSerializer();
                serializer.TryDeserialize<ArrayThatWorksForActions>(data, ref _editedWeapon.QuickMoveset);
            }
        }
        {
            var fuckcSharp = new FileInfo(WeaponTypes[selectedWep] + "Long" + ".txt");

            if (!fuckcSharp.Exists)
            {
                _editedWeapon.LongMoveset = new ArrayThatWorksForActions();
            }
            var foo = File.Open(WeaponTypes[selectedWep] + "Long" + ".txt", FileMode.Open);
            byte[] boots = new byte[fuckcSharp.Length];

            foo.Read(boots, 0, (int)fuckcSharp.Length);
            foo.Close();
            _editedWeapon._serializedLongMoves = System.Text.ASCIIEncoding.ASCII.GetString(boots);

            if (_editedWeapon._serializedLongMoves != "")
            {

                FullSerializer.fsData data = FullSerializer.fsJsonParser.Parse(_editedWeapon._serializedLongMoves);

                FullSerializer.fsSerializer serializer = new FullSerializer.fsSerializer();
                serializer.TryDeserialize<ArrayThatWorksForActions>(data, ref _editedWeapon.LongMoveset);
            }
        }
    }

    void addOption(ArrayThatWorksForActions thingToAddItTo)
    {
        if (GUILayout.Button("Add", GUILayout.Width(50f)))
        {
            var thing = (Assets.Scripts.ActionSystem.Action)Activator.CreateInstance(actTypes[selected]);
            thingToAddItTo.Add(thing);
            thingToAddItTo[0].myObj = _editedWeapon.gameObject;
            _foldoutBools.Add(false);
        }
    }

    void move(ArrayThatWorksForActions thingToMoveAroundItTo, int thingGettingMoved)
    {
        GUILayout.BeginHorizontal();
        if (thingGettingMoved > 0 && GUILayout.Button("Move Up", GUILayout.Width(80f)))
        {
            swap(thingGettingMoved,thingGettingMoved - 1, thingToMoveAroundItTo);
        }
        if (thingGettingMoved < thingToMoveAroundItTo.Count() - 1 && GUILayout.Button("Move Down", GUILayout.Width(80f)))
        {
            swap(thingGettingMoved, thingGettingMoved + 1, thingToMoveAroundItTo);
        }
        GUILayout.EndHorizontal();
    }

    void swap(int a, int b, ArrayThatWorksForActions sigh)
    {
        var wew = sigh[a];
        sigh[a] = sigh[b];
        sigh[b] = wew;
    }

    void indent(int space)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(space);
        EditorGUILayout.BeginVertical();
    }
    void unindent()
    {
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }

    void editMultiAction(ArrayThatWorksForActions actionList, string nameOfThing, int curIndent)
    {
        if(EditorGUILayout.Foldout(true, nameOfThing))
        {
            indent(curIndent);
            //loop through each existing action and display it
            for (int i = 0; i < actionList.Count(); ++i)
            {
                //if the foldout for this move is out, then edit the move
                //if (EditorGUILayout.Foldout(true, actionList[i].GetType().Name))
                {
                    //_foldoutBools[i] = true;
                    //if the action is a composite, then edit it as such
                    if (actionList[i].GetType() == typeof(ActionGroup))
                    {
                        editMultiAction(((ActionGroup)actionList[i])._actionList, actionList[i].GetType().Name, curIndent + 20);
                    }
                    else if (actionList[i].GetType() == typeof(ActionSequence))
                    {
                        editMultiAction(((ActionSequence)actionList[i])._actionList, actionList[i].GetType().Name, curIndent + 20);
                    }
                    //otherwise, edit it as a single action
                    else
                    {
                        editSingleAction(actionList[i]);
                    }
                    if (GUILayout.Button("Remove",GUILayout.Width(100f)))
                    {
                        actionList.RemoveAt(i);
                        i++;
                    }
                    move(actionList, i);
                }
                //else
                {
                    //_foldoutBools[i] = false;
                }
                //GUILayout.Label(_editedMoveset.GetArrayElementAtIndex(i).GetType().Name);

            }

            addOption(actionList);
            unindent();
        }
    }

    void editSingleAction(Assets.Scripts.ActionSystem.Action action)
    {
        GUILayout.Label(action.GetType().Name);
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
                else if (field.FieldType == typeof(GameObject))
                {
                    if(field.Name != "myObj")
                    {
                        field.SetValue(action, EditorGUILayout.ObjectField(member.Name, (GameObject)(field.GetValue(action)), typeof(GameObject), true));
                    }
                }
                else if (field.FieldType == typeof(Quaternion))
                {
                    //string RotationScript = "";
                    //
                    //RotationScript = EditorGUILayout.TextField(member.Name, RotationScript);
                    //
                    //RotationParser rotPar = new RotationParser(RotationScript);
                    //
                    //field.SetValue(action,rotPar.finalRotation);
                }
                else
                {
                    GUILayout.Label(member.Name + " is of type " + field.FieldType.Name + " which has not been reflected yet");
                }

            }
        }
    }
}
