using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : MonoBehaviour
{
    
    public DeskSlot left,right,middle;
    public void Awake()
    {
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
            
            //Debug.Log("Filling");
            DraggableStack stackl = PoolManager.Instance.GetItem(ItemType.Draggable) as DraggableStack;
            DraggableStack stackm = PoolManager.Instance.GetItem(ItemType.Draggable) as DraggableStack;
            DraggableStack stackr = PoolManager.Instance.GetItem(ItemType.Draggable) as DraggableStack;
            List<Colors> colors = new();
            colors.Add(Colors.Red);
            colors.Add(Colors.Red);
            colors.Add(Colors.Red);
           
            stackl.PushList(colors);
            stackl.Drag(left.transform.position);
            colors.Clear();
            colors.Add(Colors.Blue);
            colors.Add(Colors.Blue);
            colors.Add(Colors.Blue);
            stackm.PushList(colors);
            stackm.Drag(middle.transform.position);
            colors.Clear();
            colors.Add(Colors.Blue);
            colors.Add(Colors.Blue);
            colors.Add(Colors.Red);
            stackr.PushList(colors);
            stackr.Drag(right.transform.position);
            colors.Clear();
            left.FillSlot(stackl);
            middle.FillSlot(stackm);
            right.FillSlot(stackr);
        }
    }
}
[System.Serializable]
public class DeskOptions
{
    public Colors colors;
}
