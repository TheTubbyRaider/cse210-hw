using System;
using System.Collections.Generic;

public class GoalManager
{
    private List<Goal> goals;
    private int score;

    public GoalManager()
    {
        goals = new List<Goal>();
        score = 0;
    }

    public void CreateGoal(GoalType type, string name, string description, int points)
    {
        Goal newGoal;

        switch (type)
        {
            case GoalType.Simple:
                newGoal = new SimpleGoal
                {
                    Name = name,
                    Description = description,
                    Points = points
                };
                break;
            case GoalType.Eternal:
                newGoal = new EternalGoal
                {
                    Name = name,
                    Description = description,
                    Points = points
                };
                break;
            case GoalType.Checklist:
                Console.Write("Enter the target for the Checklist Goal: ");
                int target = int.Parse(Console.ReadLine());
                Console.Write("Enter the bonus for achieving the target: ");
                int bonus = int.Parse(Console.ReadLine());
                newGoal = new ChecklistGoal
                {
                    Name = name,
                    Description = description,
                    Points = points,
                    Target = target,
                    Bonus = bonus
                };
                break;
            default:
                return;
        }

        goals.Add(newGoal);
    }

    public void RecordEvent(int goalIndex)
    {
        if (goalIndex >= 0 && goalIndex < goals.Count)
        {
            goals[goalIndex].RecordEvent();
            int points = goals[goalIndex].Points;

            if (goals[goalIndex].IsComplete())
            {
                points += (goals[goalIndex] as ChecklistGoal)?.Bonus ?? 0;
            }

            score += points;
        }
    }

    public void ListGoals()
    {
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i}. {goals[i].GetDetailsString()}");
        }
    }

    public void DisplayScore()
    {
        Console.WriteLine($"Your current score is: {score}");
    }
}
