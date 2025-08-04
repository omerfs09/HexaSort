using UnityEngine;

[CreateAssetMenu(menuName = "Grid/Toggle Grid")]
public class ToggleGridData : ScriptableObject
{
    public int width = 5;
    public int height = 5;

    public bool[] gridValues;

    public void Init()
    {
        gridValues = new bool[width * height];
    }

    public bool Get(int x, int y)
    {
        return gridValues[y * width + x];
    }

    public void Set(int x, int y, bool value)
    {
        gridValues[y * width + x] = value;
    }
}
