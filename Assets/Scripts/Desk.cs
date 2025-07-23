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
            
            Debug.Log("Filling");
            DraggableStack stack = PoolManager.Instance.GetItem(ItemType.Draggable) as DraggableStack;
            List<Colors> colors = new();
            int r =  (int)Random.RandomRange(0, 2);
            if(r == 0)
            {
                colors.Add(Colors.Red);
                colors.Add(Colors.Red);
                colors.Add(Colors.Red);
            }
            else
            {
                colors.Add(Colors.Blue);
                colors.Add(Colors.Blue);
                colors.Add(Colors.Blue);
            }
            stack.PushList(colors);
            stack.Drag(left.transform.position);
            left.FillSlot(stack);
        }
    }
}
[System.Serializable]
public class DeskOptions
{
    public Colors colors;
}
