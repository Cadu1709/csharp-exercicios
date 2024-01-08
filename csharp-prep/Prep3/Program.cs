using System;

class Program
{
    static void Main()
    {
        // Initialize random number generator
        Random random = new Random();

        do
        {
            // Generate a random magic number between 1 and 100
            int magicNumber = random.Next(1, 101);
            int guess;
            int guesses = 0;

            Console.WriteLine("Welcome to the Guess My Number game!");
            
            // Game loop
            do
            {
                // Ask the user for a guess
                Console.Write("What is your guess? ");
                guess = Convert.ToInt32(Console.ReadLine());

                // Check if the guess is correct
                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine("You guessed it!");
                }

                // Increment the number of guesses
                guesses++;

            } while (guess != magicNumber);

            // Display the number of guesses
            Console.WriteLine($"It took you {guesses} guesses to find the magic number.");

            // Ask the user if they want to play again
            Console.Write("Do you want to play again? (yes/no): ");
            string playAgain = Console.ReadLine();

            // Check if the user wants to play again
            if (playAgain.ToLower() != "yes")
            {
                break; // Exit the loop if the user doesn't want to play again
            }

        } while (true); // Infinite loop for playing again
    }
}