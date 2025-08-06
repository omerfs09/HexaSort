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
public class OnDropCommand : Command
{
    HexagonSlot runner;
    public OnDropCommand(HexagonSlot runner)
    {
        this.runner = runner;
    }
    public void RunCommand()
    {
        runner.OnDrop();
    }
}
public class AddToSlotCommand : Command
{
    DraggableStack runner;
    HexagonSlot slot;
    DeskSlot deskSlot;
    public AddToSlotCommand(DraggableStack runner, HexagonSlot slot,DeskSlot deskSlot)
    {
        this.runner = runner;
        this.slot = slot;
        this.deskSlot = deskSlot;
    }

    public void RunCommand()
    {
        runner.AddToSlot(slot);
        if (deskSlot != null)
        {
            deskSlot.stack = null;
            deskSlot.GetDesk().OnAStackRemoved();
        }
    }
}