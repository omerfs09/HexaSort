using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameStats : MonoBehaviour
{
    public static GameStats Instance;
    Dictionary<Colors, int> numberOfColorsDict = new();
    List<HexagonSlot> slots = new();
    Dictionary<Colors, int> topColors = new();
    void Awake()
    {
        Instance = this;
        foreach (Colors item in Enum.GetValues(typeof(Colors)))
        {
            numberOfColorsDict.Add(item,0);
        
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
