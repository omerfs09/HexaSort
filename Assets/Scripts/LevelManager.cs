using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    float HEXAGON_R = GameConstants.HEXAGON_R;
    public List< LevelData> levelDatas;
    public List<HexagonSlot> slots;
    public Desk desk;
    public GameObject hexagonSlotParent;
    public TextMeshProUGUI levelNameTitle;
    public int ClearSkillCount
    {
        get => PlayerPrefs.GetInt("ClearSkill",0);
        set => PlayerPrefs.SetInt("ClearSkill", value);
    }
    public int MoveSkillCount
    {
        get => PlayerPrefs.GetInt("MoveSkill",0);
        set => PlayerPrefs.SetInt("MoveSkill", value);
    }
    public int RefreshDeskCount
    {
        get => PlayerPrefs.GetInt("RefreshDesk",0);
        set => PlayerPrefs.SetInt("RefreshDesk", value);
    }
    public int LevelNo
    {
        get => PlayerPrefs.GetInt("Level", 0);
        set => PlayerPrefs.SetInt("Level", value);
    }
    void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        LoadLevel(LevelNo);
    }
    public void SetPrefs(int val)
    {
        ClearSkillCount = val;
        MoveSkillCount = val;
        RefreshDeskCount = val;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClearLevel()
    {
        foreach (HexagonSlot hexSlot in slots)
        {
            foreach(Hexagon hex in hexSlot.stack)
            {
                PoolManager.Instance.ReturnItem(ItemType.Hexagon,hex);
            }
            PoolManager.Instance.ReturnItem(ItemType.HexagonSlot,hexSlot);
        }
        desk.ClearDesk();
        desk.deskOptions = null;
    }
    public void LoadLevel(int i)
    {
        
        if (i < levelDatas.Count || i < 0)
            LoadLevel(levelDatas[i]);
        else Debug.Log("EndOfLevels");
                
    }
    
    private Dictionary<Vector2Int, HexagonSlot> createdSlots = new();
    public void LoadLevelRecursive(LevelData levelData)
    {
        Vector3 startPos = levelData.startPos;
        Vector2Int startIndex = new Vector2Int(0, 0);
        CreateSlot(null,startPos, startIndex);

        HexagonSlot CreateSlot(HexagonSlot pre,Vector3 worldPos, Vector2Int index)
        {
            // Sýnýr kontrolü
            if (index.x < 0 || index.x >= levelData.rows || index.y < 0 || index.y >= levelData.collums)
                return null;

            // Daha önce oluþturulmuþsa tekrar oluþturma
            if (createdSlots.ContainsKey(index))
                return null;

            // Slot oluþtur
            HexagonSlot slot = (HexagonSlot)PoolManager.Instance.GetItem(ItemType.HexagonSlot);
            slot.transform.position = worldPos;
            createdSlots.Add(index, slot);

            // Komþu slotlara ilerle (3 yönlü, istersen 6'ya çýkar)
            CreateSlot(slot,worldPos + GetOffset(-1, 1), index + new Vector2Int(-1, 1));
            CreateSlot(slot,worldPos + GetOffset(1, 1), index + new Vector2Int(1, 1));
            CreateSlot(slot,worldPos + GetOffset(1, 0), index + new Vector2Int(1, 0));
            return slot;
        }

        // Hexagonal grid için pozisyon offseti (pointy-top)
        Vector3 GetOffset(int dx, int dy)
        {
            float x = HEXAGON_R * 1.5f * dx;
            float z = HEXAGON_R * Mathf.Sqrt(3) * (dy - dx * 0.5f);
            return new Vector3(x, 0, z);
        }
    }
    public void LoadLevel(LevelData levelData)
    {
        levelNameTitle.text = "Level " + levelData.levelName;
        desk.deskOptions = levelData.deskOptions;
        List<HexagonSlot> slotList = new();
        bool[,] adjacency = new bool[levelData.rows * levelData.collums,levelData.rows * levelData.collums];
        Vector3 startPos = levelData.startPos;
        for (int j = 0; j < levelData.rows; j++)
        {
            Camera.main.orthographicSize = levelData.cameraSize;
            Vector3 rowStartPos = startPos;
            
            for (int i = 0; i < levelData.collums; i++)
            {
                int slotIndex = levelData.collums * j + i; ;
                Vector2Int slotPoint = new Vector2Int(j, i);
                
                HexagonSlot slot = (HexagonSlot)PoolManager.Instance.GetItem(ItemType.HexagonSlot);
                slot.transform.parent = hexagonSlotParent.transform;
                slot.transform.position = (rowStartPos);
                GameStats.Instance.AddSlot(slot);
                slotList.Add(slot);
                
                rowStartPos = NextRightHexagon(rowStartPos);
                if(i % 2  == 1)
                {
                    Vector2Int point = slotPoint + new Vector2Int(-1, 0);
                    if (isInBounds(point)) adjacency[slotIndex, vectorToIndex(point)] = true;
                    point = slotPoint + new Vector2Int(0, 1);
                    if (isInBounds(point)) adjacency[slotIndex, vectorToIndex(point)] = true;
                    point = slotPoint + new Vector2Int(1, 1);
                    if (isInBounds(point)) adjacency[slotIndex, vectorToIndex(point)] = true;
                    point = slotPoint + new Vector2Int(1, 0);
                    if (isInBounds(point)) adjacency[slotIndex, vectorToIndex(point)] = true;
                    point = slotPoint + new Vector2Int(1, -1);
                    if (isInBounds(point)) adjacency[slotIndex, vectorToIndex(point)] = true;
                    point = slotPoint + new Vector2Int(0, -1);
                    if (isInBounds(point)) adjacency[slotIndex, vectorToIndex(point)] = true;

                    
                }
                else
                {
                    Vector2Int point = slotPoint + new Vector2Int(-1, 0);
                    if (isInBounds(point)) adjacency[slotIndex, vectorToIndex(point)] = true;
                    point = slotPoint + new Vector2Int(-1, 1);
                    if (isInBounds(point)) adjacency[slotIndex, vectorToIndex(point)] = true;
                    point = slotPoint + new Vector2Int(0, 1);
                    if (isInBounds(point)) adjacency[slotIndex, vectorToIndex(point)] = true;
                    point = slotPoint + new Vector2Int(1, 0);
                    if (isInBounds(point)) adjacency[slotIndex, vectorToIndex(point)] = true;
                    point = slotPoint + new Vector2Int(0, -1);
                    if (isInBounds(point)) adjacency[slotIndex, vectorToIndex(point)] = true;
                    point = slotPoint + new Vector2Int(-1, -1);
                    if (isInBounds(point)) adjacency[slotIndex, vectorToIndex(point)] = true;

                }




            }
            startPos = NextDownHexagon(startPos);
        }
        for (int x = 0; x < adjacency.GetLength(0); x++) // satýrlar
        {
            for (int y = 0; y < adjacency.GetLength(1); y++) // sütunlar
            {
                if(adjacency[x,y])
                slotList[x].connectedSlots.Add(slotList[y]);
                
            }
        }
        foreach(HexagonSlot slot in slotList)
        {
            
            int numOfHexs = (int)Random.Range(0, 4);
            //for (int i = 0; i < numOfHexs; i++)
            //{
            //    Hexagon hexa = (Hexagon)PoolManager.Instance.GetItem(ItemType.Hexagon);
            //    hexa.SetColor(Colors.Blue);
            //    slot.PushObject(hexa);
            //}
        }
        desk.transform.position = levelData.deskPos;
        
        DraggableStack draggable = (DraggableStack)PoolManager.Instance.GetItem(ItemType.Draggable);
        List<Colors> startColors = new();
        startColors.Add(Colors.Red);
        startColors.Add(Colors.Red);
        startColors.Add(Colors.Blue);
        draggable.PushList(startColors);
        draggable.Drag(desk.middle.transform.position);
        desk.middle.FillSlot(draggable);
        this.slots = slotList;
        int vectorToIndex(Vector2Int vector2Int)
        {
            return vector2Int.x * levelData.collums + vector2Int.y;
        }
        bool isInBounds(Vector2Int point)
        {
            if (point.x >= 0 && point.x < levelData.rows && point.y >= 0 && point.y < levelData.collums)
            {
                return true;
            }
            else return false;
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
        if (up2) nextPos = pos + new Vector3(0, 0, -1.73f) * HEXAGON_R;
        else nextPos = pos + new Vector3(0, 0, -1.73f) * HEXAGON_R;
        up2 = !up2;
        return nextPos;
    }
}

