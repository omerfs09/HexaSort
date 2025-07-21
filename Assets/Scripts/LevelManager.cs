using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelManager Instance;
    public float HEXAGON_R = 0.32f;
    public List< LevelData> levelDatas;
    
    
    void Awake()
    {
        Instance = this;
        LoadLevel(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadLevel(int i)
    {
        if (i < levelDatas.Count || i < 0)
            LoadLevel(levelDatas[i]);
        else Debug.Log("EndOfLevels");
                
    }
    public void LoadLevel(LevelData levelData)
    {
        Vector3 startPos = levelData.startPos;
        for (int j = 0; j < levelData.rows; j++)
        {
            Vector3 rowStartPos = startPos;
            for (int i = 0; i < levelData.collums; i++)
            {
                HexagonSlot slot = (HexagonSlot)PoolManager.Instance.GetItem(ItemType.HexagonSlot);
                slot.transform.position = (rowStartPos);
                rowStartPos = NextRightHexagon(rowStartPos);
            }
            startPos = NextDownHexagon(startPos);
        }

    }
    bool up = false;
    public Vector3 NextRightHexagon(Vector3 pos)
    {
        Vector3 nextPos;
        if (up) nextPos = pos + new Vector3(1.5f, 0,0.866f) * HEXAGON_R;
        else nextPos = pos + new Vector3(1.5f,0,-0.866f) * HEXAGON_R;
        up = !up;
        return nextPos;
    }
    bool up2 = false;
    public Vector3 NextDownHexagon(Vector3 pos)
    {
        Vector3 nextPos;
        if (up2) nextPos = pos + new Vector3(0, 0, 2) * HEXAGON_R;
        else nextPos = pos + new Vector3(0, 0, 2) * HEXAGON_R;
        up2 = !up2;
        return nextPos;
    }
}

