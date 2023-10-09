using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Mindfulness Program");

        while (true)
        {
            Console.WriteLine("Choose an activity:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");

            int choice = int.Parse(Console.ReadLine());

            if (choice == 4)
            {
                Console.WriteLine("Goodbye!");
                break;
            }

            int duration = GetDuration();

            switch (choice)
            {
                case 1:
                    StartBreathingActivity(duration);
                    break;
                case 2:
                    // Implement the Reflection Activity here
                    break;
                case 3:
                    // Implement the Listing Activity here
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select a valid activity.");
                    break;
            }
        }
    }

    static int GetDuration()
    {
        Console.Write("Enter the duration (in seconds): ");
        return int.Parse(Console.ReadLine());
    }

    static void StartBreathingActivity(int duration)
    {
        Console.WriteLine("Breathing Activity");
        Console.WriteLine("This activity will help you relax by walking you through breathing in and out slowly.");
        Console.WriteLine($"Clear your mind and focus on your breathing for {duration} seconds.");
        Console.WriteLine("Prepare to begin...");
        Thread.Sleep(3000); // Pause for 3 seconds

        for (int i = 0; i < duration; i++)
        {
            Console.WriteLine(i % 2 == 0 ? "Breathe in..." : "Breathe out...");
            Thread.Sleep(1000); // Pause for 1 second
        }

        Console.WriteLine("Well done! You've completed the Breathing Activity.");
        Console.WriteLine($"Duration: {duration} seconds");
        Thread.Sleep(5000); // Pause for 5 seconds before exiting the activity
    }
}
