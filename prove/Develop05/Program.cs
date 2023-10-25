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

    public void SaveGoalsToFile(string fileName)
    {
        using (StreamWriter sw = new StreamWriter(fileName))
        {
            foreach (Goal goal in goals)
            {
                sw.WriteLine(goal.GetStringRepresentation());
            }
        }
    }

    public void LoadGoalsFromFile(string fileName)
    {
        if (File.Exists(fileName))
        {
            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                string[] parts = line.Split(':');
                if (parts.Length >= 2)
                {
                    GoalType type;
                    Enum.TryParse(parts[0], out type);

                    string[] data = parts[1].Split(',');
                    if (data.Length >= 4)
                    {
                        string name = data[0];
                        string description = data[1];
                        int points = int.Parse(data[2]);
                        int isComplete = int.Parse(data[3]);

                        if (type == GoalType.Simple)
                        {
                            SimpleGoal simpleGoal = new SimpleGoal
                            {
                                Name = name,
                                Description = description,
                                Points = points,
                                IsComplete = (isComplete == 1)
                            };
                            goals.Add(simpleGoal);
                        }
                        else if (type == GoalType.Eternal)
                        {
                            EternalGoal eternalGoal = new EternalGoal
                            {
                                Name = name,
                                Description = description,
                                Points = points
                            };
                            goals.Add(eternalGoal);
                        }
                        else if (type == GoalType.Checklist && data.Length >= 6)
                        {
                            int amountCompleted = int.Parse(data[4]);
                            int target = int.Parse(data[5]);
                            int bonus = int.Parse(data[6]);

                            ChecklistGoal checklistGoal = new ChecklistGoal
                            {
                                Name = name,
                                Description = description,
                                Points = points,
                                AmountCompleted = amountCompleted,
                                Target = target,
                                Bonus = bonus
                            };
                            goals.Add(checklistGoal);
                        }
                    }
                }
            }
        }
    }

    public void Run()
    {
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
                        CreateGoal(goalType, name, description, points);
                        Console.WriteLine("Goal created!");
                    }
                    break;
                case "2":
                    Console.Write("Enter goal index to record an event: ");
                    int goalIndex = int.Parse(Console.ReadLine());
                    RecordEvent(goalIndex);
                    Console.WriteLine("Event recorded!");
                    break;
                case "3":
                    ListGoals();
                    break;
                case "4":
                    DisplayScore();
                    break;
                case "5":
                    Console.Write("Enter the filename to save goals: ");
                    string saveFileName = Console.ReadLine();
                    SaveGoalsToFile(saveFileName);
                    Console.WriteLine("Goals saved to file!");
                    break;
                case "6":
                    Console.Write("Enter the filename to load goals from: ");
                    string loadFileName = Console.ReadLine();
                    LoadGoalsFromFile(loadFileName);
                    Console.WriteLine("Goals loaded from file!");
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
