using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonSlot : MonoBehaviour,IPoolable
{
    public Stack<Hexagon> stack = new Stack<Hexagon>();
    public const float STACK_SPACE = 0.25f;
    public float stackHeight = 0;
    
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
    public void PushObject(Hexagon hexa)
    {
        stack.Push(hexa);
        hexa.MoveTo(hexa.transform.position+ Vector3.up*stackHeight);
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

    public void OnSpawn()
    {
        
    }

    public void OnDespawn()
    {

    }
}
