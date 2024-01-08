using System;

class Program
{
    static void Main()
    {
        // Ask the user for their grade percentage
        Console.Write("Enter your grade percentage: ");
        int gradePercentage = Convert.ToInt32(Console.ReadLine());

        // Determine the letter grade
        char grade;
        if (gradePercentage >= 90)
        {
            grade = 'A';
        }
        else if (gradePercentage >= 80)
        {
            grade = 'B';
        }
        else if (gradePercentage >= 70)
        {
            grade = 'C';
        }
        else if (gradePercentage >= 60)
        {
            grade = 'D';
        }
        else
        {
            grade = 'F';
        }

        // Determine if the user passed the course
        bool passed = gradePercentage >= 70;

        // Display the letter grade
        Console.Write($"Your letter grade is {grade}");

        // Stretch Challenge: Determine the sign (+, -)
        if (grade != 'F' && (gradePercentage % 10 >= 7))
        {
            Console.WriteLine("+");
        }
        else if (grade != 'F' && (gradePercentage % 10 < 3))
        {
            Console.WriteLine("-");
        }
        else
        {
            Console.WriteLine();

            // Additional logic to handle exceptional cases
            if (grade == 'A' && gradePercentage == 100)
            {
                Console.WriteLine("Congratulations on the perfect score!");
            }
            else if (grade == 'F' && gradePercentage == 0)
            {
                Console.WriteLine("You need to work on this!");
            }
        }

        // Display a message based on whether the user passed
        if (passed)
        {
            Console.WriteLine("Congratulations! You passed the course.");
        }
        else
        {
            Console.WriteLine("Don't worry; you'll do better next time!");
        }
    }
}