using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
public class DeskSlot : MonoBehaviour
{
    Desk desk;
    public DraggableStack stack;
    
    public bool IsEmpty()
    {
        return stack == null;
    }
    public void ClearSlotAnimated(Action onComplete)
    {
        if (stack == null)
        {
            onComplete?.Invoke();
            return;
        }
        stack.transform.DOKill();
        stack.transform.DOLocalMoveX(-5,0.5f).OnComplete(() => end());
        void end()
        {
            PoolManager.Instance.ReturnItem(ItemType.Draggable, stack);
            stack = null;
            onComplete?.Invoke();
        }

    }
    public void ClearSlot()
    {
        if (stack == null)
        {
            return;
        }
        PoolManager.Instance.ReturnItem(ItemType.Draggable, stack);
        stack = null;
    }
    public void FillSlot(DraggableStack stack)
    {
        stack.transform.SetParent(this.transform);
        stack.Drag(transform.position);
        this.stack = stack;
    }
    public void FillSlotAnimated(DraggableStack stack,float delay)
    {
        stack.transform.SetParent(this.transform);
        stack.transform.rotation = transform.rotation;
        stack.transform.position = transform.position + transform.right * 5;
        stack.transform.DOLocalMove(Vector3.zero, 0.14f).SetDelay(delay);
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
