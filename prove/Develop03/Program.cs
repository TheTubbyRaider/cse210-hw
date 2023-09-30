using System;
using System.Collections.Generic;
using System.Linq;

class Word
{
    public string Text { get; set; }
    public bool IsHidden { get; set; }

    public Word(string text)
    {
        Text = text;
        IsHidden = false;
    }

    public string GetDisplayText()
    {
        return IsHidden ? new string('_', Text.Length) : Text;
    }
}

class Scripture
{
    public string Reference { get; }
    private List<Word> Words { get; }

    public Scripture(string reference, string text)
    {
        Reference = reference;
        Words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    public bool AllWordsHidden()
    {
        return Words.All(word => word.IsHidden);
    }

    public void HideRandomWords(int numToHide)
    {
        Random random = new Random();
        List<Word> wordsToHide = Words.Where(word => !word.IsHidden).ToList();

        for (int i = 0; i < numToHide && wordsToHide.Count > 0; i++)
        {
            int indexToHide = random.Next(0, wordsToHide.Count);
            wordsToHide[indexToHide].IsHidden = true;
            wordsToHide.RemoveAt(indexToHide);
        }
    }

    public string GetDisplayText()
    {
        return $"{Reference}\n\n{string.Join(" ", Words.Select(word => word.GetDisplayText()))}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create a scripture
        Scripture scripture = new Scripture("John 3:16", "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life.");

        Console.WriteLine("Welcome to Scripture Memorizer!");

        do
        {
            Console.Clear(); // Attempt to clear the console

            // Clear the console manually by printing empty lines
            Console.WriteLine(new string('\n', Console.WindowHeight));

            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine("\nPress Enter to hide more words or type 'quit' to exit.");

            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
            {
                break;
            }

            scripture.HideRandomWords(3); // Change 3 to the number of words you want to hide per press
        } while (!scripture.AllWordsHidden());

        Console.WriteLine("\nAll words are hidden. Goodbye!");
    }
}