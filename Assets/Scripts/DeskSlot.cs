using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskSlot : MonoBehaviour
{
    Desk desk;
    public DraggableStack stack;
    
    public bool IsEmpty()
    {
        return stack == null;
    }
    public void ClearSlot()
    {
        if (stack == null) return;
        
        PoolManager.Instance.ReturnItem(ItemType.Draggable, stack);
        stack = null;
        
    }
    public void FillSlot(DraggableStack stack)
    {
        this.stack = stack;
    }
    public void SetDesk(Desk desk)
    {
        this.desk = desk;
    }
    public Desk GetDesk()
    {
        return desk;
    }
}
