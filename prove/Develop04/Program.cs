using System;
using System.IO;
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
            Console.WriteLine("4. View Log");
            Console.WriteLine("5. Exit");

            int choice = int.Parse(Console.ReadLine());

            if (choice == 5)
            {
                Console.WriteLine("Goodbye!");
                break;
            }
            else if (choice == 4)
            {
                ViewLog();
                continue;
            }

            int duration = GetDuration();
            Activity activity;

            switch (choice)
            {
                case 1:
                    activity = new BreathingActivity(duration);
                    break;
                case 2:
                    activity = new ReflectionActivity(duration);
                    break;
                case 3:
                    activity = new ListingActivity(duration);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select a valid activity.");
                    continue;
            }

            LogActivity(activity);
            activity.Start();
        }
    }

    static int GetDuration()
    {
        Console.Write("Enter the duration (in seconds): ");
        return int.Parse(Console.ReadLine());
    }

    static void LogActivity(Activity activity)
    {
        using (StreamWriter sw = File.AppendText("activityLog.txt"))
        {
            sw.WriteLine(activity.ToString());
        }
    }

    static void ViewLog()
    {
        Console.WriteLine("Activity Log:");
        string logContent = File.ReadAllText("activityLog.txt");
        Console.WriteLine(logContent);
    }
}

class Activity
{
    protected int Duration;

    public Activity(int duration)
    {
        Duration = duration;
    }

    public virtual void Start()
    {
        Console.WriteLine("Activity has started...");
        Thread.Sleep(Duration * 1000);
        Console.WriteLine("Activity has ended.");
    }

    public override string ToString()
    {
        return $"{GetType().Name} - Duration: {Duration} seconds";
    }
}

class BreathingActivity : Activity
{
    public BreathingActivity(int duration) : base(duration) { }

    public override void Start()
    {
        Console.WriteLine("Breathing Activity");
        Console.WriteLine("This activity will help you relax by walking you through breathing in and out slowly.");
        Console.WriteLine($"Clear your mind and focus on your breathing for {Duration} seconds.");
        Console.WriteLine("Prepare to begin...");
        Thread.Sleep(3000);

        for (int i = 0; i < Duration; i++)
        {
            Console.WriteLine(i % 2 == 0 ? "Breathe in..." : "Breathe out...");
            Thread.Sleep(1000);
        }

        Console.WriteLine("Well done! You've completed the Breathing Activity.");
        Console.WriteLine($"Duration: {Duration} seconds");
        Thread.Sleep(5000);
    }
}

class ReflectionActivity : Activity
{
    public ReflectionActivity(int duration) : base(duration) { }

    public override void Start()
    {
        Console.WriteLine("Reflection Activity");
        Console.WriteLine("This activity will help you reflect on times in your life when you have shown strength and resilience.");
        Console.WriteLine("This will help you recognize the power you have and how you can use it in other aspects of your life.");
        Console.WriteLine($"Think about these experiences for the next {Duration} seconds...");
        Console.WriteLine("Prepare to begin...");
        Thread.Sleep(3000);

        string[] prompts = {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        Random random = new Random();
        for (int i = 0; i < Duration; i++)
        {
            string prompt = prompts[random.Next(prompts.Length)];
            Console.WriteLine(prompt);
            Thread.Sleep(1000);
        }

        Console.WriteLine("Well done! You've completed the Reflection Activity.");
        Console.WriteLine($"Duration: {Duration} seconds");
        Thread.Sleep(5000);
    }
}

class ListingActivity : Activity
{
    public ListingActivity(int duration) : base(duration) { }

    public override void Start()
    {
        Console.WriteLine("Listing Activity");
        Console.WriteLine("This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.");
        Console.WriteLine($"Think about these things for the next {Duration} seconds...");
        Console.WriteLine("Prepare to begin...");
        Thread.Sleep(3000);

        string[] prompts = {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        Random random = new Random();
        int itemsListed = 0;
        for (int i = 0; i < Duration; i++)
        {
            string prompt = prompts[random.Next(prompts.Length)];
            Console.WriteLine(prompt);
            itemsListed++;
            Thread.Sleep(1000);
        }

        Console.WriteLine($"You listed {itemsListed} items in the Listing Activity.");
        Console.WriteLine($"Duration: {Duration} seconds");
        Thread.Sleep(5000);
    }
}
