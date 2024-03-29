﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RubkiCubeMaterialSet", order = 1)]
public class RubikCubeMaterialSet : ScriptableObject
{
    public Material Up;
    public Material Down;
    public Material Left;
    public Material Right;
    public Material Front;
    public Material Back;

    public Dictionary<string, Material> map;

    public void Init()
    {
        map = new Dictionary<string, Material>();
        map.Add(Models.FaceName.Up, Up);
        map.Add(Models.FaceName.Down, Down);
        map.Add(Models.FaceName.Left, Left);
        map.Add(Models.FaceName.Right, Right);
        map.Add(Models.FaceName.Front, Front);
        map.Add(Models.FaceName.Back, Back);
    }

    
    
}

#if UNITY_EDITOR
[CustomEditor(typeof(RubikCubeMaterialSet))]
public class InspectorItemHolder : Editor
{

    RubikCubeMaterialSet t;

    void OnEnable()
    {
        t = (RubikCubeMaterialSet)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();
        DrawDefaultInspector();
        if (!t.Up || !t.Left || !t.Front || !t.Right || !t.Back || !t.Down)
        {
            EditorGUILayout.TextArea("Null values are not allowed !!");
            GUILayout.BeginHorizontal("box");
            Texture2D myTexture = new Texture2D(1, 1);
            myTexture.LoadImage(System.IO.File.ReadAllBytes("Assets/Icons/err.png"));
            myTexture.Apply();
            GUILayout.Label(myTexture, GUILayout.Width(30), GUILayout.Height(30));
            GUILayout.Label("Null values are not allowed !!");
            GUILayout.EndHorizontal();
        }

        //if (GUILayout.Button("reset"))
        //{
        //    t.materialSet = new Dictionary<string, Material>();
        //}
    }
}
#endif