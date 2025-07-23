using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Command 
{
    public void RunCommand();
}
public class AddToSlotCommand : Command
{
    HexagonSlot hexagonSlot;
    DraggableStack draggableStack;
    public AddToSlotCommand(HexagonSlot hexagonSlot,DraggableStack draggableStack)
    {
        this.hexagonSlot = hexagonSlot;
        this.draggableStack = draggableStack;
    }
    public void RunCommand()
    {
        draggableStack.AddToSlot(hexagonSlot);
    }
}
public class PourCommand : Command
{
    HexagonSlot from, to;
    Colors color;
    public PourCommand(HexagonSlot from, HexagonSlot to,Colors color)
    {
        this.from = from;
        this.to = to;
        this.color = color;
    }
    public void RunCommand()
    {
        from.PourToSlot(to,color);
        Debuger();
    }
    public void Debuger()
    {
        Debug.Log(from.name + ">"+to.name);

    }
}
public class CheckNeighborsCommand : Command
{
    HexagonSlot checker,other;
    Colors color;

    public CheckNeighborsCommand(HexagonSlot checker, Colors color,HexagonSlot other)
    {
        this.checker = checker;
        this.color = color;
        this.other = other;
    }

    public void RunCommand()
    {
        checker.CheckNeighbors(color,other);
        Debug.Log("Checking",checker);
    }
    public bool IsRunnable()
    {
        return checker.isCheckAble;
    }
}