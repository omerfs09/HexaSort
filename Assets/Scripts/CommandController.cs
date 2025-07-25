using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandController : MonoBehaviour
{
    public static CommandController Instance;
    public Queue<ClearSlotCommand> clearQue = new();
    void Awake()
    {
        Instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
