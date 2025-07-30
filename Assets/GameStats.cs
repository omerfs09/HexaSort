using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameStats : MonoBehaviour
{
    public static GameStats Instance;
    Dictionary<Colors, int> numberOfColorsDict = new();
    List<HexagonSlot> slots = new();
    void Awake()
    {
        Instance = this;
        foreach (Colors item in Enum.GetValues(typeof(Colors)))
        {
            numberOfColorsDict.Add(item,0);
        
        }
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
    int emptySlotCount,totalSlotCount;
    public SlotsStatus(int empty,int total)
    {
        emptySlotCount = empty;
        totalSlotCount = total;
    }
    public float FillRate()
    {
        return emptySlotCount / totalSlotCount;
    }
}
