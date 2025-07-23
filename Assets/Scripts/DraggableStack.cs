using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableStack : MonoBehaviour,IPoolable
{
    public List<Hexagon> hexaList = new();
    float STACK_SPACE = GameConstants.STACK_SPACE;
    float stackHeight = GameConstants.STACK_SPACE;
    void Start()
    {
        
    }
    
    public void PushList(List<Colors> hexagonColors)
    {
        foreach (Colors color in hexagonColors)
        {
            Hexagon hexagon = (Hexagon)PoolManager.Instance.GetItem(ItemType.Hexagon);
            hexagon.SetColor(color);
            hexagon.MoveTo(transform.position + Vector3.up * stackHeight);
            hexaList.Add(hexagon);
            hexagon.transform.SetParent(this.transform);
            stackHeight += STACK_SPACE;
        }
    }
    public void AddToSlot(HexagonSlot slot)
    {
        foreach (Hexagon item in hexaList)
        {
            item.transform.SetParent(null);
            slot.PushObject(item);
        }
        PoolManager.Instance.ReturnItem(ItemType.Draggable, this);
        slot.OnPut();
    }
    public void Drag(Vector3 pos)
    {
        transform.position = pos;
    }

    public void OnSpawn()
    {
        
    }

    public void OnDespawn()
    {

    }
}
