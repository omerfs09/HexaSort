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
    public int colorSeries = 0;
    
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
    public void OnDrop()
    {
        Colors topColor;
        if (stack.Count > 0)
            topColor = stack.Peek().color;
        else topColor = Colors.Null;
         
        if(topColor != Colors.Null)
        CheckNeighbors(topColor,()=> CommandController.Instance.RunClearQueue());
        
    }
    public void CheckNeighbors(Colors color1, Action onComplete)
    {
        if(color1 == Colors.Null)
        {
            color1 = GetTopColor();
        }
        Colors color = color1;
        isAvailable = false;

        List<HexagonSlot> available = new();
        foreach (HexagonSlot slot in connectedSlots)
        {

            if (slot.GetTopColor() == color && slot.GetTopColor() != Colors.Null && slot.isAvailable)
            {

                available.Add(slot);
            }
        }
        if (available.Count > 0)
        {
            if (available.Count < 2)
            {
                if (available[0].IsSingleColor() && !IsSingleColor())
                {
                    Debug.Log("SingleColorMatch");
                    available[0].CheckNeighbors(color, onComplete);
                    isAvailable = true;
                    onComplete?.Invoke();
                }
                else
                {
                    Debug.Log("Tekli");
                    available[0].isAvailable = false;
                    available[0].PourToSlot(this, color, () => CheckNeighbors(GetTopColor(), () => available[0].CheckNeighbors(Colors.Null, () => end())));

                }
            }
            else
            {
                Debug.Log("Ciftli");
                available[0].isAvailable = false;
                available[0].PourToSlot(this, color, () => CheckNeighbors(GetTopColor(), () => available[0].CheckNeighbors(Colors.Null, () => end())));
            }
            

        }
        else
        {
            Debug.Log("No Avaliable");
            //CheckMatch(null);
            isAvailable = true;
            onComplete?.Invoke();
        }
        void end()
        {
            //CheckMatch(()=> { isAvailable = true;onComplete?.Invoke(); });
            CheckMatch(null);
            isAvailable = true;
            onComplete?.Invoke();

        }


    }
    
    
    public void PourToSlot(HexagonSlot other, Colors color, Action onComplete)
    {

        isAvailable = false;
        other.isAvailable = false;
        int i = 0;
        float wait = 0;
        while (stack.Count > 0 && color == stack.Peek().color)
        {
            Hexagon hexagon = stack.Pop();
            hexagon.transform.DOJump(other.transform.position + (other.stackHeight + i * GameConstants.STACK_SPACE) * Vector3.up, 0.1f, 1, 0.15f).SetDelay(i * 0.15f).OnComplete(() => other.PushObject(hexagon,false)); ;
            stackHeight -= GameConstants.STACK_SPACE;
            i++;
        }
        wait = stack.Count * 0.15f + 1.5f;
        StartCoroutine(cor());
        IEnumerator cor()
        {
            yield return new WaitForSeconds(wait);
            isAvailable = true;
            other.isAvailable = true;
            //stackHeight = GameConstants.STACK_SPACE;
            onComplete?.Invoke();
        }

    }
    
    public void CheckMatch(Action action)
    {
        
        if(TopThreeAreEqual(stack))
        {
            CommandController.Instance.clearQue.Enqueue(new ClearSlotCommand(this,action));
            
            //ClearSlot(GetTopColor(),action);
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
        return  10 <= GetColorSeries();
        
    }

    public void ClearSlot(Colors color = Colors.Null,Action onComplete = null)
    {
        isAvailable = false;
        float movePeriod = 0.25f;
        int i = 0;
        string clearString = "";
        while(stack.Count > 0 && stack.Peek().color == color)
        {
            clearString += stack.Peek().color.ToString()+",";
            stack.Pop().transform.DOScale(Vector3.zero,movePeriod).SetDelay(i*0.15f);
            stackHeight += -GameConstants.STACK_SPACE;
            i++;
        }
        StartCoroutine(wait());
        IEnumerator wait()
        {
            yield return new WaitForSeconds((i+1)*0.15f+movePeriod);
            //CheckNeighbors(GetTopColor(),null);
            Debug.Log(clearString + i.ToString());
            isAvailable = true;
            onComplete?.Invoke();
            
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
    public int GetColorSeries()
    {
        
        int num = 0;
        Colors firstColor = GetTopColor();
        Stack<Hexagon> tempList = new();
        if (firstColor == Colors.Null) return 0;
        while(stack.Count > 0 && firstColor == stack.Peek().color ) {
            Hexagon poped = stack.Pop();
            if(poped.color == firstColor)
            {
                tempList.Push(poped);
                num++;
            }
            else
            {
                break;
            }
        }
        foreach (Hexagon item in tempList)
        {
            stack.Push(item);
        }
        return num;
       
    }
    public bool IsSingleColor()
    {
        if (GetColorSeries() == stack.Count ) return true;
        else return false;
    }
    public void PushObject(Hexagon hexa,bool move = true)
    {
        
        stack.Push(hexa);
        if(move)
        hexa.MoveTo(transform.position+ Vector3.up*stackHeight,false);
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
        if (stack.Count > 0 ) return stack.Peek().color;
        return Colors.Null;
    }
    
    public void OnSpawn()
    {
        
    }

    public void OnDespawn()
    {

    }
}
