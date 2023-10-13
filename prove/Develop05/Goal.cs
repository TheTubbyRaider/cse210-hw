using System;

public enum GoalType
{
    Simple,
    Eternal,
    Checklist
}

public abstract class Goal
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Points { get; set; }

    public virtual void RecordEvent()
    {
        // Implement recording event for goals
    }

    public abstract bool IsComplete();

    public virtual string GetDetailsString()
    {
        return $"{Name} - {Description}";
    }

    public abstract string GetStringRepresentation();
}