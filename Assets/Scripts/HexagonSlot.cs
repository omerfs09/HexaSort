using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class HexagonSlot : MonoBehaviour,IPoolable
{
    public Stack<Hexagon> stack = new Stack<Hexagon>();
    float STACK_SPACE = GameConstants.STACK_SPACE;
    float stackHeight = GameConstants.STACK_SPACE;
    public List<HexagonSlot> connectedSlots;
    public bool isAvailable = true;
    public  bool isCheckAble = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void GetStackPosition()
    {
        
    }
    public void OnPut()
    {
        Colors topColor;
        if (stack.Count > 0)
            topColor = stack.Peek().color;
        else topColor = Colors.Null;
         //List<HexagonSlot> visited = new();
        //visited.Add(this);
        if(topColor != Colors.Null)
        CheckNeighbors(topColor,null);
        
    }
    public void CheckNeighbors(Colors color,HexagonSlot other)
    {
        isAvailable = false;
        
        List<HexagonSlot> available = new();
        foreach(HexagonSlot slot in connectedSlots)
        {
            
            if (slot.GetTopColor() == color)
            {

                available.Add(slot);
            }
        }
        if(available.Count > 0)
        {
            if (available.Count < 2)
            {
                
                //GameController.Instance.AddToQueue();
                new PourCommand(available[0], this,color).RunCommand();


            }
            else
            {
                //GameController.Instance.AddToQueue();
                new PourCommand(available[0], this,color).RunCommand();
                
            }
        }
        if(other!= null)
        {
            
            GameController.Instance.AddToQueue(new CheckNeighborsCommand(this, GetTopColor(), null));
        }
        
        
    }
    
    public void PourToSlot(HexagonSlot other,Colors color)
    {
        isCheckAble = false;
        isAvailable = false;
        other.isAvailable = false;
        int i = 0;
        float wait = 0;
        while (stack.Count > 0 && color == stack.Peek().color)
        {
            
           Hexagon hexagon  = stack.Pop();
           hexagon.transform.DOLocalJump(other.transform.position +(other.stackHeight+ i*GameConstants.STACK_SPACE) * Vector3.up, 1f,1,0.15f).SetDelay(i*0.15f).OnComplete(() => other.PushObject(hexagon)); ;
           i++;
        }
        wait = stack.Count * 0.15f;
        StartCoroutine(cor());
        IEnumerator cor()
        {
            yield return new WaitForSeconds(wait);
            isAvailable = true;
            other.isAvailable = true;
            isCheckAble = true;
            GameController.Instance.AddToQueue(new CheckNeighborsCommand(this, GetTopColor(),other));
            
            
            
            
        }
        stackHeight = GameConstants.STACK_SPACE;
    }
    public void CheckMatch()
    {
        
        if(TopThreeAreEqual(stack))
        {
            
        }
        else
        {
            Debug.Log("Doesnt Match!");
        }
    }
    public bool IsEmpty()
    {
        if(stack.Count <= 0)
        {
            return true;
        }
        return false;
    }
    public bool TopThreeAreEqual(Stack<Hexagon> stack)
    {
        int num = 3;
        if (stack.Count < num) return false;

        Hexagon[] topItems = new Hexagon[num];
        Stack<Hexagon> tempStack = new Stack<Hexagon>();

        // Ýlk 10 elemaný al ve tempStack'e kaydet
        for (int i = 0; i < num; i++)
        {
            Hexagon item = stack.Pop();
            topItems[i] = item;
            tempStack.Push(item);
        }

        // Ýlk elemanla diðerlerini karþýlaþtýr
        Hexagon first  = topItems[0];
        for (int i = 1; i < num; i++)
        {
            if (topItems[i].color != first.color)
            {
                // Stack'i geri yükle
                while (tempStack.Count > 0)
                    stack.Push(tempStack.Pop());
                return false;
            }
        }

        // Stack'i geri yükle
        while (tempStack.Count > 0)
            stack.Push(tempStack.Pop());

        return true;
    }

    public void ClearSlot()
    {
        if(stack.Count > 0)
        {
           stack.Pop().transform.DOScale(Vector3.zero,0.5f).SetEase(Ease.InOutBack).OnComplete(()=> ClearSlot());
        }
    }
    public void Test()
    {
        foreach(HexagonSlot slot in connectedSlots)
        {
            Hexagon hexagon = PoolManager.Instance.GetItem(ItemType.Hexagon) as Hexagon;
            hexagon.MoveTo(slot.transform.position);
        }
        
    }
    public void PushObject(Hexagon hexa)
    {
        stack.Push(hexa);
        hexa.MoveTo(transform.position+ Vector3.up*stackHeight);
        stackHeight += STACK_SPACE;
    }
    public void PopObject()
    {
        
        if (stack.Count > 0)
        {
            Destroy(stack.Pop().gameObject);
            stackHeight += -STACK_SPACE;
        }
        else
        {
            OnPopDone();
        }
    }
    public void OnPopDone()
    {
        Debug.Log("Pop Is Done!");
    }
    public Colors GetTopColor()
    {
        if (stack.Count > 0) return stack.Peek().color;
        return Colors.Null;
    }
    public void OnSpawn()
    {
        
    }

    public void OnDespawn()
    {

    }
}
