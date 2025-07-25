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
        addToSlotQue.Dequeue().RunCommand();
        
    }
    public void EnqueueClearCommand(ClearSlotCommand clearSlotCommand)
    {
        HexagonSlot.addToSlotEnabled = false;
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
