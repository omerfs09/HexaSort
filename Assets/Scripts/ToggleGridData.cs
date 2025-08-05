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

    public bool Get(int col, int row)
    {
        return gridValues[row * width + col];
    }

    public void Set(int col, int row, bool value)
    {
        gridValues[row * width + col] = value;
    }
}
