using System;

namespace YourNamespace // Replace with your actual namespace if you have one
{
    class Program
    {
        static void Main(string[] args)
        {
            GoalManager goalManager = new GoalManager();

            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Create Goal");
                Console.WriteLine("2. Record Event");
                Console.WriteLine("3. List Goals");
                Console.WriteLine("4. Display Score");
                Console.WriteLine("5. Exit");
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
                        return;
                }
            }
        }
    }
}
