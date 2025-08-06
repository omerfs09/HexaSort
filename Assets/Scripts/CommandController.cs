using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandController : MonoBehaviour
{
    public static CommandController Instance;
    public Queue<ClearSlotCommand> clearQue = new();
    public Queue<AddToSlotCommand> addToSlotQue = new();
    public Queue<OnDropCommand> onDropQue = new();
    void Awake()
    {
        Instance = this;
    }

    
    void Update()
    {
        if (IsDropable())
        {
            RunOnDropQueue();
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
    public void EnqueueOnDropCommand(OnDropCommand command)
    {
        onDropQue.Enqueue(command);
    }
    public void RunOnDropQueue()
    {
        if(onDropQue.Count > 0)
        {
            onDropQue.Dequeue().RunCommand();
        }
    }
    public void ClearAllQues()
    {
        clearQue.Clear();
        addToSlotQue.Clear();
        onDropQue.Clear();
    }
}
