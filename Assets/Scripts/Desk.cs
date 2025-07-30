using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Desk : MonoBehaviour
{
    public static Desk Instance;
    public DeskSlot left,right,middle;
    public void Awake()
    {
        Instance = this;
        left.SetDesk(this);
        right.SetDesk(this);
        middle.SetDesk(this);
    }
    public bool IsDeskEmpty()
    {
        if(left.IsEmpty() && right.IsEmpty() && middle.IsEmpty())
        {
            return true;
        }
        return false;
    }
    public void OnAStackRemoved()
    {
        if (IsDeskEmpty())
        {
            SlotsStatus status = GameStats.Instance.GetSlotsStatus();
            
            FillDesk(Diffuculty.Medium,status);
        }
    }
    public void RefreshDesk()
    {
        ClearDesk();
        FillDesk();
    }
    public void ClearDesk()
    {
        left.ClearSlot();
        middle.ClearSlot();
        right.ClearSlot();
    }
    public void FillDesk()
    {
        
        DraggableStack stackl = PoolManager.Instance.GetItem(ItemType.Draggable) as DraggableStack;
        DraggableStack stackm = PoolManager.Instance.GetItem(ItemType.Draggable) as DraggableStack;
        DraggableStack stackr = PoolManager.Instance.GetItem(ItemType.Draggable) as DraggableStack;
        List<Colors> colors = new();
        colors.Add(Colors.Red);
        colors.Add(Colors.Red);
        colors.Add(Colors.Red);
        colors.Add(Colors.Red);
        colors.Add(Colors.Red);
        colors.Add(Colors.Green);
        colors.Add(Colors.Green);
        colors.Add(Colors.Green);
        colors.Add(Colors.Green);
        colors.Add(Colors.Green);

        stackl.PushList(colors);
        stackl.Drag(left.transform.position);
        colors.Clear();

        colors.Add(Colors.Red);
        colors.Add(Colors.Red);
        colors.Add(Colors.Red);
        colors.Add(Colors.Red);
        colors.Add(Colors.Red);
        colors.Add(Colors.Blue);
        colors.Add(Colors.Blue);
        colors.Add(Colors.Blue);
        colors.Add(Colors.Blue);
        colors.Add(Colors.Blue);

        stackm.PushList(colors);
        stackm.Drag(middle.transform.position);
        colors.Clear();
        colors.Add(Colors.Blue);
        colors.Add(Colors.Blue);
        colors.Add(Colors.Blue);
        colors.Add(Colors.Blue);
        colors.Add(Colors.Blue);
        colors.Add(Colors.Red);
        colors.Add(Colors.Red);
        colors.Add(Colors.Red);
        colors.Add(Colors.Red);
        colors.Add(Colors.Red);
        stackr.PushList(colors);
        stackr.Drag(right.transform.position);
        colors.Clear();
        left.FillSlot(stackl);
        middle.FillSlot(stackm);
        right.FillSlot(stackr);
    }
    public void FillDesk(Diffuculty diffuculty, SlotsStatus status)
    {
        float fillRate = status.FillRate();
        DraggableStack stackl = PoolManager.Instance.GetItem(ItemType.Draggable) as DraggableStack;
        //DraggableStack stackm = PoolManager.Instance.GetItem(ItemType.Draggable) as DraggableStack;
        //DraggableStack stackr = PoolManager.Instance.GetItem(ItemType.Draggable) as DraggableStack;
        List<Colors> colors = new();
        Colors rarest = Colors.Blue;
        foreach (Colors item in Enum.GetValues(typeof(Colors)))
        {
           if(GameStats.Instance.GetColorCount(item) < GameStats.Instance.GetColorCount(rarest) && item != Colors.Null)
            {
                rarest = item;
            } 
        }
        if (fillRate < 0.2f)
        {
            
            GameStats.Instance.GetColorCount(Colors.Blue);
            AddColorsToList(colors, rarest, 5);
            //AddColorsToList(colors, Colors.Green, 5);
            stackl.PushList(colors);
            stackl.Drag(left.transform.position);
            colors.Clear();
            
        }
        else if(fillRate < 0.8f)
        {

        }
        else
        {

        }
        
        
        
        left.FillSlot(stackl);
        //middle.FillSlot(stackm);
        //right.FillSlot(stackr);
    }
    public void AddColorsToList(List<Colors> list, Colors color,int count)
    {
        for(int i = 0; i < count; i++)
        {
            list.Add(color);
        }
    }
}
[System.Serializable]
public class DeskOptions
{
    public Colors colors;
}
public enum Diffuculty
{
    Hard,
    Medium,
    Easy
}
