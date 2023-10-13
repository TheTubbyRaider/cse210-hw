using System;

public class SimpleGoal : Goal
{
    public bool IsComplete { get; set; }

    public override bool IsComplete()
    {
        return IsComplete;
    }

    public override string GetDetailsString()
    {
        return $"{(IsComplete ? "[X]" : "[ ]")} {Name} - {Description}";
    }

    public override string GetStringRepresentation()
    {
        return $"SimpleGoal:{Name},{Description},{Points},{(IsComplete ? "1" : "0")}";
    }
}