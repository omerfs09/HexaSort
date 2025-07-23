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
        if (GUILayout.Button("Ortala"))
        {
            levelData.startPos = (-(float)(levelData.collums-1) / 2) * new Vector3(1.5f, 0, -0.866f)*GameConstants.HEXAGON_R + (-(float)(levelData.rows-1) / 2) * new Vector3(0, 0, -1.73f) * GameConstants.HEXAGON_R; 
            Debug.Log(levelData.rows);
        }
    }
}