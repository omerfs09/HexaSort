using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface Command 
{
    public void RunCommand();
}
public class ClearSlotCommand : Command
{
    HexagonSlot runner;
    Action action;

    public ClearSlotCommand(HexagonSlot runner, Action action)
    {
        this.runner = runner;
        this.action = action;
    }
    public void RunCommand()
    {
        runner.ClearSlot(runner.GetTopColor(), action);
    }
}