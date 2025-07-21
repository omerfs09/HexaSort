using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(LevelData))]
public class EditorTest : Editor
{
    public override void OnInspectorGUI()
    {
        // Normal Inspector'� �iz
        DrawDefaultInspector();

        // Hedef script referans�
        LevelData levelData = (LevelData)target;

        // Buton
        if (GUILayout.Button("Blok Ad�n� Yazd�r"))
        {
            Debug.Log(levelData.rows);
        }
    }
}