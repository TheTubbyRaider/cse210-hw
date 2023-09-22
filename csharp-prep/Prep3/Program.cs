using System;

class Program
{
    static void Main(string[] args)
    {
        // Initialize a random number generator
        Random random = new Random();

        // Initialize variables
        int magicNumber = random.Next(1, 101); // Generate a random number between 1 and 100
        int userGuess;
        int numberOfGuesses = 0;
        bool playAgain = true;

        while (playAgain)
        {
            // Reset the number of guesses for a new game
            numberOfGuesses = 0;

            Console.WriteLine("Welcome to the Guess My Number game!");
            Console.WriteLine("I'm thinking of a number between 1 and 100.");
            
            // Main game loop
            while (true)
            {
                Console.Write("What is your guess? ");
                userGuess = Convert.ToInt32(Console.ReadLine());
                numberOfGuesses++;

                if (userGuess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (userGuess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine($"You guessed it in {numberOfGuesses} attempts!");
                    break; // Exit the inner loop when the guess is correct
                }
            }

            // Ask if the user wants to play again
            Console.Write("Do you want to play again? (yes/no): ");
            string playAgainInput = Console.ReadLine().ToLower();
            playAgain = (playAgainInput == "yes");
        }

        Console.WriteLine("Thanks for playing!");
    }
}