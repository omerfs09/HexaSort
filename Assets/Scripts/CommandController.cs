using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandController : MonoBehaviour
{
    public static CommandController Instance;
    public Queue<ClearSlotCommand> clearQue = new();
    public Queue<AddToSlotCommand> addToSlotQue = new();
    void Awake()
    {
        Instance = this;
    }

    
    void Update()
    {
        if (IsDropable())
        {
            RunAddToSlotQue();
            
        }
    }
    public  bool IsDropable()
    {
        
        return HexagonSlot.addToSlotEnabled;
    }
    public void EnqueAddToSlotCommand(AddToSlotCommand command)
    {
       addToSlotQue.Enqueue(command);
    }
    public void RunAddToSlotQue()
    {
        if(addToSlotQue.Count > 0)
        {

            addToSlotQue.Dequeue().RunCommand(); Debug.Log("QueRuned");
        }
        
    }
    public void EnqueueClearCommand(ClearSlotCommand clearSlotCommand)
    {
        clearQue.Enqueue(clearSlotCommand);
    }
    public void RunClearQueue()
    {
        while (clearQue.Count > 0)
        {
            clearQue.Dequeue().RunCommand();
        }
        
    }
}
