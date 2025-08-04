using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(LevelManager))]
public class PlayerPrefsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Normal Inspector'ý çiz
        DrawDefaultInspector();

        // Hedef script referansý
        LevelManager levelData = (LevelManager)target;
        
        // Buton
        if (GUILayout.Button("ResetLevelData"))
        {
            levelData.LevelNo = 0;
        }
        if (GUILayout.Button("Debug Current Level"))
        {
            Debug.Log("Current Level Index Is " + levelData.LevelNo);
        }
        if (GUILayout.Button("Add Skills"))
        {
            levelData.ClearSkillCount++;
            levelData.MoveSkillCount++;
            levelData.RefreshDeskCount++;
        }
        if (GUILayout.Button("Reset Skills"))
        {
            levelData.ClearSkillCount = 0;
            levelData.MoveSkillCount = 0;
            levelData.RefreshDeskCount = 0;
        }
        if (GUILayout.Button("Reset LevelNO"))
        {
            levelData.LevelNo = 0;
        }
    }
}
