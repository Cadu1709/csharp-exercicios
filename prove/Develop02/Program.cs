using System;
using System.Collections.Generic;
using System.IO;

// Define a class to represent a journal entry
class JournalEntry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }
}

// Define a class to manage the journal
class JournalManager
{
    private List<JournalEntry> entries;

    public JournalManager()
    {
        entries = new List<JournalEntry>();
    }

    public void WriteNewEntry()
    {
        // Select a random prompt from the list
        string randomPrompt = GetRandomPrompt();

        Console.WriteLine($"Prompt: {randomPrompt}");

        // Get user's response
        Console.Write("Response: ");
        string response = Console.ReadLine();

        // Get the current date
        string currentDate = DateTime.Now.ToString("yyyy-MM-dd");

        // Create a new journal entry
        JournalEntry entry = new JournalEntry
        {
            Prompt = randomPrompt,
            Response = response,
            Date = currentDate
        };

        // Add the entry to the list
        entries.Add(entry);

        Console.WriteLine("Entry saved successfully!");
    }

    public void DisplayJournal()
    {
        // Iterate through all entries and display them
        foreach (var entry in entries)
        {
            Console.WriteLine($"Date: {entry.Date}");
            Console.WriteLine($"Prompt: {entry.Prompt}");
            Console.WriteLine($"Response: {entry.Response}\n");
        }
    }

    public void SaveToFIle(string fileName)
    {
        // Save the current journal to a file
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (var entry in entries)
            {
                // Write entry details to the file
                writer.WriteLine($"{entry.Date},{entry.Prompt},{entry.Response}");
            }
        }

        Console.WriteLine($"Journal saved to {fileName} successfully!");
    }

    public void LoadFromFile(string fileName)
    {
        // Load entries from a file and replace current entries
        entries.Clear();

        using (StreamReader reader = new StreamReader(fileName))
        {
            while (!reader.EndOfStream)
            {
                // Read entry details from the file
                string[] entryDetails = reader.ReadLine().Split(',');

                // Create a new entry and add it to the list
                JournalEntry entry = new JournalEntry
                {
                    Date = entryDetails[0],
                    Prompt = entryDetails[1],
                    Response = entryDetails[2]
                };

                entries.Add(entry);
            }
        }

        Console.WriteLine($"Journal loaded from {fileName} successfully!");
    }

    private string GetRandomPrompt()
    {
        // Add your list of prompts here
        List<string> prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?"
        };

        // Select a random prompt
        Random random = new Random();
        int index = random.Next(prompts.Count);
        return prompts[index];
    }
}

// Define the main program class
class Program
{
    static void Main()
    {
        JournalManager journalManager = new JournalManager();

        while (true)
        {
            // Display menu options
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Exit");

            // Get user input
            Console.Write("Select an option: ");
            int choice = int.Parse(Console.ReadLine());

            // Perform the selected action
            switch (choice)
            {
                case 1:
                    journalManager.WriteNewEntry();
                    break;
                case 2:
                    journalManager.DisplayJournal();
                    break;
                case 3:
                    Console.Write("Enter the file name to save: ");
                    string saveFileName = Console.ReadLine();
                    journalManager.SaveToFIle(saveFileName);
                    break;
                case 4:
                    Console.Write("Enter the file name to load: ");
                    string loadFileName = Console.ReadLine();
                    journalManager.LoadFromFile(loadFileName);
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}