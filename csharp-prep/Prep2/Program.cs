using System;

class Program
{
    static void Main(string[] args)
    {
        // Ask the user for their grade percentage
        Console.Write("Enter your grade percentage: ");
        double gradePercentage = Convert.ToDouble(Console.ReadLine());

        // Initialize variables for letter grade and pass status
        string letter = "";
        bool passed = false;

        // Determine the letter grade
        if (gradePercentage >= 90)
        {
            letter = "A";
        }
        else if (gradePercentage >= 80)
        {
            letter = "B";
        }
        else if (gradePercentage >= 70)
        {
            letter = "C";
        }
        else if (gradePercentage >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        // Check if the user passed the course (grade >= 70)
        if (gradePercentage >= 70)
        {
            passed = true;
        }

        // Display the letter grade and pass status
        Console.WriteLine("Your letter grade is: " + letter);

        if (passed)
        {
            Console.WriteLine("Congratulations, you passed the course!");
        }
        else
        {
            Console.WriteLine("You can do better next time. Keep it up!");
        }
    }
}




