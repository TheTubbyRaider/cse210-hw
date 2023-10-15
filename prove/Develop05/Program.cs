using System;
using System.Collections.Generic;
using System.IO;

public class SimpleGoal : Goal
{
    public bool IsComplete { get; set; }

    public override bool IsGoalComplete()
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

public class ChecklistGoal : Goal
{
    public int AmountCompleted { get; set; }
    public int Target { get; set; }
    public int Bonus { get; set; }

    public override bool IsGoalComplete()
    {
        return AmountCompleted >= Target;
    }

    public override string GetDetailsString()
    {
        return $"{(IsGoalComplete() ? "[X]" : "[ ]")} {Name} - {Description} (Completed {AmountCompleted}/{Target})";
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal:{Name},{Description},{Points},{AmountCompleted},{Target},{Bonus}";
    }
}

public class EternalGoal : Goal
{
    public override bool IsGoalComplete()
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

    public abstract bool IsGoalComplete();

    public virtual string GetDetailsString()
    {
        return $"{Name} - {Description}";
    }

    public abstract string GetStringRepresentation();
}

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

            if (goals[goalIndex].IsGoalComplete())
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

    public void SaveGoalsToFile(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (Goal goal in goals)
            {
                outputFile.WriteLine(goal.GetStringRepresentation());
            }
        }
    }

    public void LoadGoalsFromFile(string filename)
    {
        goals.Clear(); // Clear existing goals before loading
        try
        {
            string[] lines = System.IO.File.ReadAllLines(filename);

            foreach (string line in lines)
            {
                Goal newGoal = CreateGoalFromData(line);
                if (newGoal != null)
                {
                    goals.Add(newGoal);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading goals from the file: " + e.Message);
        }
    }

    private Goal CreateGoalFromData(string data)
    {
        string[] parts = data.Split(':');
        if (parts.Length < 2)
        {
            return null; // Invalid data
        }

        string goalType = parts[0];
        string details = parts[1];

        string[] detailParts = details.Split(',');
        if (detailParts.Length < 3)
        {
            return null; // Invalid data
        }

        string name = detailParts[0];
        string description = detailParts[1];
        int points = int.Parse(detailParts[2]);

        Goal newGoal = null;

        if (goalType == "SimpleGoal")
        {
            bool isComplete = detailParts.Length > 3 && detailParts[3] == "1";
            newGoal = new SimpleGoal
            {
                Name = name,
                Description = description,
                Points = points,
                IsComplete = isComplete
            };
        }
        else if (goalType == "ChecklistGoal")
        {
            if (detailParts.Length < 6)
            {
                return null; // Invalid data
            }
            int amountCompleted = int.Parse(detailParts[3]);
            int target = int.Parse(detailParts[4]);
            int bonus = int.Parse(detailParts[5]);
            newGoal = new ChecklistGoal
            {
                Name = name,
                Description = description,
                Points = points,
                AmountCompleted = amountCompleted,
                Target = target,
                Bonus = bonus
            };
        }
        else if (goalType == "EternalGoal")
        {
            newGoal = new EternalGoal
            {
                Name = name,
                Description = description,
                Points = points
            };
        }

        return newGoal;
    }

    public void Run()
    {
        GoalManager goalManager = new GoalManager();

        while (true)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Create Goal");
            Console.WriteLine("2. Record Event");
            Console.WriteLine("3. List Goals");
            Console.WriteLine("4. Display Score");
            Console.WriteLine("5. Save Goals to File");
            Console.WriteLine("6. Load Goals from File");
            Console.WriteLine("7. Exit");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter goal type (Simple, Eternal, or Checklist): ");
                    string goalTypeString = Console.ReadLine();
                    if (Enum.TryParse(goalTypeString, true, out GoalType goalType))
                    {
                        Console.Write("Enter goal name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter goal description: ");
                        string description = Console.ReadLine();
                        Console.Write("Enter goal points: ");
                        int points = int.Parse(Console.ReadLine());
                        goalManager.CreateGoal(goalType, name, description, points);
                        Console.WriteLine("Goal created!");
                    }
                    break;
                case "2":
                    Console.Write("Enter goal index to record an event: ");
                    int goalIndex = int.Parse(Console.ReadLine());
                    goalManager.RecordEvent(goalIndex);
                    Console.WriteLine("Event recorded!");
                    break;
                case "3":
                    goalManager.ListGoals();
                    break;
                case "4":
                    goalManager.DisplayScore();
                    break;
                case "5":
                    Console.Write("Enter the filename to save goals: ");
                    string saveFilename = Console.ReadLine();
                    goalManager.SaveGoalsToFile(saveFilename);
                    Console.WriteLine("Goals saved to the file!");
                    break;
                case "6":
                    Console.Write("Enter the filename to load goals from: ");
                    string loadFilename = Console.ReadLine();
                    goalManager.LoadGoalsFromFile(loadFilename);
                    Console.WriteLine("Goals loaded from the file!");
                    break;
                case "7":
                    return;
            }
        }
    }

    public static void Main(string[] args)
    {
        GoalManager goalManager = new GoalManager();
        goalManager.Run();
    }
}
