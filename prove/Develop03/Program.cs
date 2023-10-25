using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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

class Reference
{
    public string Text { get; }

    public Reference(string text)
    {
        Text = text;
    }

    public override string ToString()
    {
        return Text;
    }
}

class Scripture
{
    public Reference Reference { get; }
    public List<Word> Words { get; }

    public Scripture(Reference reference, string text)
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

    public string GetOriginalText()
    {
        return $"{Reference}\n\n{string.Join(" ", Words.Select(word => word.Text))}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Scripture> scriptures = new List<Scripture>();
        scriptures.Add(new Scripture(new Reference("John 3:16"), "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life."));
        scriptures.Add(new Scripture(new Reference("Genesis 1:1"), "In the beginning God created the heavens and the earth."));
        scriptures.Add(new Scripture(new Reference("John 1:1"), "In the beginning was the Word, and the Word was with God, and the Word was God."));
        scriptures.Add(new Scripture(new Reference("Psalm 23:1"), "The Lord is my shepherd; I shall not want."));
        scriptures.Add(new Scripture(new Reference("Proverbs 3:5-6"), "Trust in the Lord with all your heart, and do not lean on your own understanding. In all your ways acknowledge him, and he will make straight your paths."));
        scriptures.Add(new Scripture(new Reference("Matthew 5:3-10"), "Blessed are the poor in spirit, for theirs is the kingdom of heaven. Blessed are those who mourn, for they shall be comforted. Blessed are the meek, for they shall inherit the earth. Blessed are those who hunger and thirst for righteousness, for they shall be satisfied. Blessed are the merciful, for they shall receive mercy. Blessed are the pure in heart, for they shall see God. Blessed are the peacemakers, for they shall be called sons of God. Blessed are those who are persecuted for righteousness' sake, for theirs is the kingdom of heaven."));
        scriptures.Add(new Scripture(new Reference("Romans 8:28"), "And we know that in all things God works for the good of those who love him, who have been called according to his purpose."));
        scriptures.Add(new Scripture(new Reference("1 Corinthians 13:4-7"), "Love is patient, love is kind. It does not envy, it does not boast, it is not proud. It does not dishonor others, it is not self-seeking, it is not easily angered, it keeps no record of wrongs. Love does not delight in evil but rejoices with the truth. It always protects, always trusts, always hopes, always perseveres."));
        scriptures.Add(new Scripture(new Reference("Isaiah 40:31"), "But those who hope in the Lord will renew their strength. They will soar on wings like eagles; they will run and not grow weary, they will walk and not be faint."));
        scriptures.Add(new Scripture(new Reference("Ephesians 2:8-9"), "For it is by grace you have been saved, through faith—and this is not from yourselves, it is the gift of God—not by works, so that no one can boast."));
        scriptures.Add(new Scripture(new Reference("Philippians 4:13"), "I can do all things through Christ who strengthens me."));
        scriptures.Add(new Scripture(new Reference("Jeremiah 29:11"), "For I know the plans I have for you, declares the Lord, plans for welfare and not for evil, to give you a future and a hope."));
        scriptures.Add(new Scripture(new Reference("Colossians 3:23"), "Whatever you do, work at it with all your heart, as working for the Lord, not for human masters."));
        scriptures.Add(new Scripture(new Reference("Proverbs 16:9"), "In their hearts humans plan their course, but the Lord establishes their steps."));
        scriptures.Add(new Scripture(new Reference("James 1:5"), "If any of you lacks wisdom, you should ask God, who gives generously to all without finding fault, and it will be given to you."));
        scriptures.Add(new Scripture(new Reference("Matthew 28:19-20"), "Go therefore and make disciples of all nations, baptizing them in the name of the Father and of the Son and of the Holy Spirit, teaching them to observe all that I have commanded you. And behold, I am with you always, to the end of the age."));
        scriptures.Add(new Scripture(new Reference("Psalm 119:105"), "Your word is a lamp to my feet and a light to my path."));
        scriptures.Add(new Scripture(new Reference("Proverbs 4:23"), "Above all else, guard your heart, for everything you do flows from it."));
        scriptures.Add(new Scripture(new Reference("John 14:6"), "Jesus answered, 'I am the way and the truth and the life. No one comes to the Father except through me.'"));


        Console.WriteLine("Welcome to Scripture Memorizer!");

        do
        {
            Console.Clear();
            Console.WriteLine("Select a scripture to memorize:");

            for (int i = 0; i < scriptures.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {scriptures[i].Reference}");
            }

            Console.WriteLine("Enter the number of the scripture you want to memorize or type 'quit' to exit.");

            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
            {
                break;
            }

            if (int.TryParse(input, out int selection) && selection >= 1 && selection <= scriptures.Count)
            {
                MemorizeScripture(scriptures[selection - 1]);
            }
            else
            {
                Console.WriteLine("Invalid input. Please select a valid scripture.");
            }

        } while (true);

        Console.WriteLine("Goodbye!");
    }

    static void MemorizeScripture(Scripture scripture)
    {
        int totalWords = scripture.Words.Count;
        int wordsToHide = totalWords / 4; // Example: Hide 25% of the words initially
        scripture.HideRandomWords(wordsToHide);

        while (!scripture.AllWordsHidden())
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine($"Progress: {scripture.Words.Count(word => word.IsHidden)} / {totalWords}");
            Console.WriteLine("\nOptions:\n1. Reveal hidden words\n2. Hide more words\n3. Quit");

            string input = Console.ReadLine();

            if (input == "1")
            {
                RevealWords(scripture);
            }
            else if (input == "2")
            {
                scripture.HideRandomWords(3); // Change 3 to the number of words you want to hide per press
            }
            else if (input == "3")
            {
                break;
            }
        }

        Console.WriteLine("All words are hidden. Good job!");
    }

    static void RevealWords(Scripture scripture)
    {
        Console.Clear();
        Console.WriteLine(scripture.GetOriginalText());
        Console.WriteLine("Press Enter to continue.");
        Console.ReadLine();
    }
}
