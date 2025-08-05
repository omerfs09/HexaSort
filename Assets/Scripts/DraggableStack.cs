using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DraggableStack : MonoBehaviour,IPoolable
{
    private List<Hexagon> hexaList = new();
    static float STACK_SPACE = GameConstants.STACK_SPACE;
    float stackHeight = GameConstants.STACK_SPACE;
    void Start()
    {
        
    }
    
    public void PushHexagon(Hexagon hexagon)
    {
        hexagon.MoveTo(transform.position + Vector3.up * stackHeight);
        hexaList.Add(hexagon);
        hexagon.transform.SetParent(this.transform);
        stackHeight += STACK_SPACE;
    }
    public void PushList(List<Colors> hexagonColors)
    {
        foreach (Colors color in hexagonColors)
        {
            Hexagon hexagon = (Hexagon)PoolManager.Instance.GetItem(ItemType.Hexagon);
            hexagon.SetColor(color);
            hexagon.SetGetShadows(false);
            PushHexagon(hexagon);
        }
    }
    public void PushList(List<Hexagon> hexagons)
    {
        foreach(Hexagon hex in hexagons)
        {
            PushHexagon(hex);
        }
    }
    public void AddToSlot(HexagonSlot slot)
    {
        foreach (Hexagon item in hexaList)
        {
            item.transform.SetParent(PoolManager.Instance.transform);
            item.SetGetShadows(true);
            slot.PushObject(item);
            item.transform.rotation = slot.transform.rotation;
            GameStats.Instance.AddColor(item.color, 1);
        }
        hexaList.Clear();
        PoolManager.Instance.ReturnItem(ItemType.Draggable, this);
        slot.OnDrop();
    }

    public void Drag(Vector3 pos)
    {
        transform.position = pos;
        transform.localScale = Vector3.one;
    }

    public void DragAnimated(Vector3 pos)
    {
        transform.position = Vector3.Lerp(transform.position, pos, 0.3f*Time.deltaTime*60);
    }
    
    public void MoveTo(Vector3 position)
    {
        transform.DOMove(position, 0.1f);
    }
    public void OnSpawn()
    {
    
    }

    public void OnDespawn()
    {
        foreach (Hexagon item in hexaList)
        {
            PoolManager.Instance.ReturnItem(ItemType.Hexagon, item);
            item.transform.SetParent(PoolManager.Instance.transform);
        }
        hexaList.Clear();
        stackHeight = GameConstants.STACK_SPACE;

    }
}
