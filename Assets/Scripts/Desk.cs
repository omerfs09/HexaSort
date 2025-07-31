using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Desk : MonoBehaviour
{
    public static Desk Instance;
    public DeskSlot left,right,middle;
    public DeskOptions deskOptions;  
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
        ClearDeskAnimated(()=>FillDesk(Diffuculty.Medium,GameStats.Instance.GetSlotsStatus()));
    }
    public void ClearDeskAnimated(Action onComplete)
    {
        left.ClearSlotAnimated(()=> middle.ClearSlotAnimated(()=> right.ClearSlotAnimated(() => onComplete?.Invoke())));
        
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
        colors.Add(Colors.Blue);
        colors.Add(Colors.Blue);
        
        colors.Add(Colors.Blue);

        stackl.PushList(colors);
        colors.Clear();

        colors.Add(Colors.Red);
        colors.Add(Colors.Red);
        colors.Add(Colors.Red);
        
        colors.Add(Colors.Blue);
        colors.Add(Colors.Blue);
        colors.Add(Colors.Blue);
        

        stackm.PushList(colors);
        colors.Clear();
        colors.Add(Colors.Blue);
        colors.Add(Colors.Blue);
        
        colors.Add(Colors.Blue);
        colors.Add(Colors.Blue);
        colors.Add(Colors.Blue);
        colors.Add(Colors.Blue);
        
        stackr.PushList(colors);
        colors.Clear();
        left.FillSlot(stackl);
        middle.FillSlot(stackm);
        right.FillSlot(stackr);
    }
    public DraggableStack GetRandomDraggable(Diffuculty diffuculty,SlotsStatus status)
    {
        DraggableStack draggable = PoolManager.Instance.GetItem(ItemType.Draggable) as DraggableStack;
        List<Colors> colors = new();
        float top = UnityEngine.Random.Range(0, 1);
        float exist = UnityEngine.Random.Range(0, 1);
        float nonexist = UnityEngine.Random.Range(0, 1);
        if (status.emptySlotCount < 5)
        {
            if(top < 0.7f)
            {
                Colors color = GetRandomTopColor();
                if (color != Colors.Null)
                    for (int i = 0; i < UnityEngine.Random.Range(1, 3); i++)
                    {
                        colors.Add(color);
                    }
            }
            if (exist < 0.2f)
            {
                Colors color = GetRandomExistingColor();
                if (color != Colors.Null)
                    for (int i = 0; i < UnityEngine.Random.Range(1, 3); i++)
                    {
                    colors.Add(color);
                    }
            }
            if (nonexist < 0.1f)
            {
                Colors color = GetRandomNonExistingColor();
                if (color != Colors.Null)
                    for (int i = 0; i < UnityEngine.Random.Range(1, 3); i++)
                    {
                        colors.Add(color);
                    }
            }
        }
        else if(status.emptySlotCount < 10)
        {
            if (top < 0.5f)
            {
                Colors color = GetRandomTopColor();
                if (color != Colors.Null)
                    for (int i = 0; i < UnityEngine.Random.Range(1, 3); i++)
                {
                    colors.Add(color);
                }
            }
            if (exist < 0.5f)
            {
                Colors color = GetRandomExistingColor();
                if (color != Colors.Null)
                    for (int i = 0; i < UnityEngine.Random.Range(1, 3); i++)
                {
                    colors.Add(color);
                }
            }
            if (nonexist < 0.2f)
            {
                Colors color = GetRandomNonExistingColor();
                if (color != Colors.Null)
                    for (int i = 0; i < UnityEngine.Random.Range(1, 3); i++)
                {
                    colors.Add(color);
                }
            }
        }
        else 
        {
            if (top < 0.1f)
            {
                Colors color = GetRandomTopColor();
                if (color != Colors.Null)
                    for (int i = 0; i < UnityEngine.Random.Range(1, 3); i++)
                {
                    colors.Add(color);
                }
            }
            if (exist < 0.5f)
            {
                Colors color = GetRandomExistingColor();
                if (color != Colors.Null)
                    for (int i = 0; i < UnityEngine.Random.Range(1, 3); i++)
                {
                    colors.Add(color);
                }
            }
            if (nonexist < 0.7f)
            {
                Colors color = GetRandomNonExistingColor();
                if (color != Colors.Null)
                    for (int i = 0; i < UnityEngine.Random.Range(1, 3); i++)
                {
                    colors.Add(color);
                }
            }
        }
        
        draggable.PushList(colors);
        return draggable;
    }
    public Colors GetRandomTopColor()
    {
        List<Colors> colors = new();
        foreach (var item in GameStats.Instance.ColorSeriesIterator())
        {
            if(item.Value > 0)
            {
                colors.Add(item.Key);
            }
        }
        if (colors.Count > 0)
            return colors[UnityEngine.Random.Range(0, colors.Count - 1)];
        else return Colors.Null;
    }
    public Colors GetRandomExistingColor()
    {
        List<Colors> colors = GameStats.Instance.ExistingColorsIterator();
        if (colors.Count > 0)
            return colors[UnityEngine.Random.Range(0, colors.Count - 1)];
        else return Colors.Null;
    }
    public Colors GetRandomNonExistingColor()
    {
        List<Colors> colors = new();
        foreach (var item in GameStats.Instance.NonExistingColorsIterator())
        {
            if (deskOptions.colors.Contains(item))
            {
                colors.Add(item);
            }
        }
        if (colors.Count > 0)
            return colors[UnityEngine.Random.Range(0, colors.Count - 1)];
        else return Colors.Null;
    }
    public void FillDesk(Diffuculty diffuculty, SlotsStatus status)
    {
        DraggableStack leftD = GetRandomDraggable(diffuculty,status);
        DraggableStack middleD = GetRandomDraggable(diffuculty,status);
        DraggableStack rightD = GetRandomDraggable(diffuculty,status);
        left.FillSlotAnimated(leftD,0.15f);
        middle.FillSlotAnimated(middleD,0.25f);
        right.FillSlotAnimated(rightD,0.35f);
            
        
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
    public List<Colors> colors;
}
public enum Diffuculty
{
    Hard,
    Medium,
    Easy
}
