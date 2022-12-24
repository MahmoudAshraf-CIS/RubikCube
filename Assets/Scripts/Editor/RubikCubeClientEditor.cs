using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Models;

[CustomEditor(typeof(RubikCubeClient))]
public class RubikCubeClientEditor : Editor
{
    bool showRubikCube = true;
   

    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();
        RubikCubeClient t = (RubikCubeClient)target;
        EditorUtility.SetDirty(target);
        if (GUILayout.Button("Apply operation"))
        {
            
        }
        if (GUILayout.Button("Save"))
        {
            
        }
        if (GUILayout.Button("Load"))
        {
            
        }


        if (t.model == null)
            return;
        //GUILayout.Label("Model executer stack = "+ (t.modelExecuter ==null ? 0:t.modelExecuter.StackSize() ).ToString());
        GUILayout.Label("View executer stack = "+  (t.executer == null ? 0: t.executer.StackSize() ).ToString());
        


        GUIStyle tableStyle = new GUIStyle();
        tableStyle.padding = new RectOffset(10, 10, 10, 10);
        tableStyle.fixedWidth = 50 * t.model.Size * 4;
        tableStyle.fixedHeight = 50 * t.model.Size * 3 + 50;

        GUIStyle enumStyle = new GUIStyle();
        enumStyle.fixedHeight = 50;
        enumStyle.fixedHeight = 50;
        enumStyle.alignment = TextAnchor.MiddleCenter;

        if (t.model != null && t.model.Faces != null && t.model.Faces.Length > 0)
        {
            EditorGUILayout.Space();
            showRubikCube = EditorGUILayout.Foldout(showRubikCube, "Show or hide Rubik Cube", true);
            if (showRubikCube)
            {
                EditorGUILayout.BeginHorizontal(tableStyle);
                for (int x = 0; x < 4; x++)
                {
                    EditorGUILayout.BeginVertical();
                    for (int y = 0; y < 3; y++)
                    {
                        EditorGUILayout.BeginHorizontal();
                        DrawFace(t.model.Faces[x + y * 4], enumStyle);
                        EditorGUILayout.EndHorizontal();
                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }

    private void DrawFace(Face face, GUIStyle enumStyle)
    {
        EditorGUILayout.BeginHorizontal();
        for (int x = 0; x < face.Size; x++)
        {
            EditorGUILayout.BeginVertical();
            for (int y = 0; y < face.Size; y++)
            {
                GUIStyle cellColorStyle = new GUIStyle();

                //cellColorStyle.normal.background =  MakeTex(Color.clear);

                cellColorStyle.normal.background = face.GetCell(x, y).Material ?
                    MakeTex(face.GetCell(x, y).Material.color) : MakeTex(Color.clear);
                EditorGUILayout.BeginHorizontal(cellColorStyle);
                GUILayout.Label(face.GetCell(x, y).Name, enumStyle);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();
    }
    private Texture2D MakeTex(Color col)
    {
        Color[] pix = new Color[1];
        pix[0] = col;
        Texture2D result = new Texture2D(1, 1);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }
}
