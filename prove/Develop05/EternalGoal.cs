using System;

public class EternalGoal : Goal
{
    public override bool IsComplete()
    {
        return false;
    }

    public override string GetDetailsString()
    {
        return $"[Eternal] {Name} - {Description}";
    }

    public override string GetStringRepresentation()
    {
        return $"EternalGoal:{Name},{Description},{Points}";
    }
}