using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        List<double> numbers = new List<double>();
        double sum = 0;
        double average = 0;
        double largest = double.MinValue;
        double smallestPositive = double.MaxValue;

        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        while (true)
        {
            Console.Write("Enter number: ");
            double number = Convert.ToDouble(Console.ReadLine());

            if (number == 0)
            {
                break;
            }

            numbers.Add(number);
            sum += number;

            if (number > largest)
            {
                largest = number;
            }

            if (number > 0 && number < smallestPositive)
            {
                smallestPositive = number;
            }
        }

        if (numbers.Count > 0)
        {
            average = sum / numbers.Count;

            Console.WriteLine($"The sum is: {sum}");
            Console.WriteLine($"The average is: {average}");
            Console.WriteLine($"The largest number is: {largest}");

            if (smallestPositive != double.MaxValue)
            {
                Console.WriteLine($"The smallest positive number is: {smallestPositive}");
            }

            Console.WriteLine("The sorted list is:");
            List<double> sortedNumbers = numbers.OrderBy(n => n).ToList();
            foreach (var number in sortedNumbers)
            {
                Console.WriteLine(number);
            }
        }
        else
        {
            Console.WriteLine("No numbers entered.");
        }
    }
}