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

        if (GUILayout.Button("Center"))
        {
            Vector3 sum = new Vector3(0, 0, 0);
            for (int i = 0; i < levelData.collums; i++)
            {
                for (int j = 0; j < levelData.rows; j++)
                {
                    sum = sum +GameConstants.HexPosition(i, j);
                }
            }
            levelData.startPos = -sum * (1 / (((float)levelData.collums * (float)levelData.rows)));
        }
    }
}