using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(LevelData))]
public class EditorTest : Editor
{
    public override void OnInspectorGUI()
    {
        // Normal Inspector'ý çiz
        DrawDefaultInspector();

        // Hedef script referansý
        LevelData levelData = (LevelData)target;

        // Buton
        if (GUILayout.Button("Blok Adýný Yazdýr"))
        {
            Debug.Log(levelData.rows);
        }
    }
}