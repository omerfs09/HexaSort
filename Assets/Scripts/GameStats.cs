using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameStats : MonoBehaviour
{
    public static GameStats Instance;
    public int moves;
    private Dictionary<Colors, int> numberOfColorsDict = new();
    private List<HexagonSlot> slots = new();
    private Dictionary<Colors, int> topColors = new();
    private float progress = 0;
    private float progressAim = 100;
    public void ResetStats()
    {
        moves = 0;
        slots.Clear();
        topColors.Clear();
        progress = 0;
    }
    public override string ToString()
    {
        return "Moves" + moves.ToString() + "Number Of Slots" + slots.Count.ToString() + "Top Colors\n" + topColors.ToString(); 
    }
    void Awake()
    {
        Instance = this;
        foreach (Colors item in Enum.GetValues(typeof(Colors)))
        {
            numberOfColorsDict.Add(item,0);
        
        }
    }
    public void PrintExistingColors()
    {
        foreach (var item in ColorSeriesIterator())
        {
            Debug.Log(item.Key +" = " + item.Value);
        }
    }
    public List<Colors> ExistingColorsIterator()
    {
        List<Colors> temp = new();
        foreach (Colors item in Enum.GetValues(typeof(Colors)))
        {

            int count = GameStats.Instance.GetColorCount(item);
            if (count > 0)
            {
                temp.Add(item);
            }
        }
        return temp;
    }
    public void CheckGameOver()
    {
        bool gameOver = true;

        foreach (var item in slots)
        {
            if (item.IsEmpty())
            {
                gameOver = false;
                break; 
            }
        }
        if (gameOver)
        {
            UIManager.HideAllPanels();
            UIManager.ShowPanel(PanelType.GameOverPanel);
            Debug.LogWarning("Game Over!!!!");
        }
    }
    public bool CheckLevelComplete()
    {
        if(progress >= progressAim)
        {
            UIManager.ShowLevelCompletePanel();
            return true;
        }
        else
        {
            //Do Nothing
            return false;
        }
    }
    public Dictionary<Colors,int> GetSlotColors()
    {
        Dictionary<Colors, int> dict = new();
        foreach (var item in slots)
        {
            Colors color = item.GetTopColor();
            if (color != Colors.Null)
            {

                if (dict.ContainsKey(color))
                {
                    dict[color]++;
                }
                else
                dict.Add(item.GetTopColor(), 1);
            }
        }
        return dict;
    }
    public List<Colors> NonExistingColorsIterator()
    {
        List<Colors> temp = new();
        foreach (Colors item in Enum.GetValues(typeof(Colors)))
        {

            int count = GameStats.Instance.GetColorCount(item);
            if (count <= 0 && item != Colors.Null)
            {
                temp.Add(item);
            }
        }
        return temp;
    }
    public Dictionary<Colors,int> ColorSeriesIterator()
    {
        foreach (HexagonSlot slot in  slots)
        {
            Colors topColor = slot.GetTopColor();
            if (topColors.ContainsKey(topColor))
            {
                topColors[topColor] += slot.GetColorSeries();
            }
            else
            {
                topColors.Add(topColor, slot.GetColorSeries());
            }
        }
        return topColors;
    }
    public void printNumbers()
    {
        foreach(var item in numberOfColorsDict)
        {
            Debug.Log(item.Key.ToString() + item.Value.ToString());
        }
        Debug.Log(ToString());
    }
    public SlotsStatus GetSlotsStatus()
    {
        int i = 0;
        int totalSlotsCount = 0;
        foreach (HexagonSlot slot in slots)
        {
            if (slot.IsEmpty())
            {
                i++;
            }
            totalSlotsCount++;
        }
        return new SlotsStatus(i,totalSlotsCount);
    }
    
    public void AddSlot(HexagonSlot slot)
    {
        slots.Add(slot);
    }
    public void AddColor(Colors color,int count)
    {
        if (numberOfColorsDict.ContainsKey(color))
        {
            numberOfColorsDict[color] += count;

        }
        else
        {
            numberOfColorsDict.Add(color, count);
        }
    }
    public int GetColorCount(Colors color)
    {
        if (numberOfColorsDict.ContainsKey(color))
            return numberOfColorsDict[color];
        else return 0;
    }
    public void SetColorCount(Colors color,int value)
    {
        if (numberOfColorsDict.ContainsKey(color))
            numberOfColorsDict[color] = value;
        else AddColor(color,value);
    }
    public void ChangeProggress(float diff)
    {
        progress += diff;
        
    }
    public float GetProggress()
    {
        return progress;
    }
    public float GetProggressAim()
    {
        return progressAim;
    }
    public void SetProggressAim(int aim)
    {
        progressAim = aim;
    }
    public void DebugProggres()
    {
        Debug.Log( progress);
    }
}
public class SlotsStatus
{
    public int emptySlotCount,totalSlotCount;
    public SlotsStatus(int empty,int total)
    {
        emptySlotCount = empty;
        totalSlotCount = total;
    }
    
}
