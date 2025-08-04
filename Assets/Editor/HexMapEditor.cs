using UnityEditor;
using UnityEngine;

public class HexMapEditor : EditorWindow
{
    public GameObject hexPrefab;
    public int width = 10;
    public int height = 10;
    public float hexWidth = 1f;
    public float hexHeight = 1f;

    [MenuItem("Tools/Hex Map Editor")]
    public static void ShowWindow()
    {
        GetWindow<HexMapEditor>("Hex Map Editor");
    }

    void OnGUI()
    {
        hexPrefab = (GameObject)EditorGUILayout.ObjectField("Hex Prefab", hexPrefab, typeof(GameObject), false);
        width = EditorGUILayout.IntField("Width", width);
        height = EditorGUILayout.IntField("Height", height);
        hexWidth = EditorGUILayout.FloatField("Hex Width", hexWidth);
        hexHeight = EditorGUILayout.FloatField("Hex Height", hexHeight);

        if (GUILayout.Button("Generate Map"))
        {
            GenerateHexMap();
        }
    }

    void GenerateHexMap()
    {
        if (hexPrefab == null)
        {
            Debug.LogError("Hex prefab is not assigned!");
            return;
        }

        GameObject mapParent = new GameObject("HexMap");

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector2 pos = HexToWorldPosition(x, y, hexWidth, hexHeight);
                GameObject hex = (GameObject)PrefabUtility.InstantiatePrefab(hexPrefab);
                hex.transform.position = new Vector3(pos.x, 0, pos.y);
                hex.transform.SetParent(mapParent.transform);
            }
        }
    }

    Vector2 HexToWorldPosition(int x, int y, float width, float height)
    {
        float offset = (y % 2 == 0) ? 0 : width * 0.5f;
        float worldX = x * width + offset;
        float worldY = y * (height * 0.75f);
        return new Vector2(worldX, worldY);
    }
}
