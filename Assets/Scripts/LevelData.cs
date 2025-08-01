using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "HexaSort/LevelData")]
[System.Serializable]
public class LevelData: ScriptableObject
{
    public int rows;
    public int collums;
    public Vector3 startPos;
    public float cameraSize;
    public Vector3 deskPos;
    public DeskOptions deskOptions;
    public string levelName;
    public int progressAim;
}
