using System;

class Program
{
    static void Main()
    {
        // Create an instance of MathAssignment
        MathAssignment mathAssignment = new MathAssignment("Roberto Rodriguez", "Fractions", "7.3", "8-19");

        // Call the GetSummary and GetHomeworkList methods and display the results
        string mathSummary = mathAssignment.GetSummary();
        string mathHomeworkList = mathAssignment.GetHomeworkList();

        Console.WriteLine("Math Assignment:");
        Console.WriteLine(mathSummary);
        Console.WriteLine(mathHomeworkList);
        Console.WriteLine();

        // Create an instance of WritingAssignment
        WritingAssignment writingAssignment = new WritingAssignment("Mary Waters", "European History", "The Causes of World War II");

        // Call the GetSummary and GetWritingInformation methods and display the results
        string writingSummary = writingAssignment.GetSummary();
        string writingInformation = writingAssignment.GetWritingInformation();

        Console.WriteLine("Writing Assignment:");
        Console.WriteLine(writingSummary);
        Console.WriteLine(writingInformation);
    }
}