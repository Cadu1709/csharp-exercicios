using System;
using System.Collections.Generic;
using System.IO;

class Scripture
{
    private string reference;
    private string text;
    private List<string> hiddenWords;

    public Scripture(string reference, string text)
    {
        this.reference = reference;
        this.text = text;
        hiddenWords = new List<string>();
    }

    public void Display()
    {
        Console.Clear();
        string displayedText = text;

        foreach (var word in hiddenWords)
        {
            displayedText = displayedText.Replace(word, new string('_', word.Length));
        }

        Console.WriteLine($"{reference}\n\n{displayedText}\n");
    }

    public void HideRandomWord()
    {
        if (hiddenWords.Count < text.Split(' ').Length)
        {
            string[] words = text.Split(' ');

            Random random = new Random();
            int randomIndex = random.Next(words.Length);

            hiddenWords.Add(words[randomIndex]);

            Display();
        }
    }

    public bool AllWordsHidden()
    {
        return hiddenWords.Count == text.Split(' ').Length;
    }
}

class Program
{
    static void Main()
    {
        string filePath = "scriptures.txt"; // C:\Users\cadud\OneDrive\Documents\csharp-prep\csharp-prep\prove\Develop03\scriptures.txt

        List<Scripture> scriptures = LoadScripturesFromFile(filePath);

        foreach (var scripture in scriptures)
        {
            while (!scripture.AllWordsHidden())
            {
                Console.WriteLine("Press Enter to hide a word or type 'quit' to exit:");
                string input = Console.ReadLine();

                if (input.ToLower() == "quit")
                    return;

                scripture.HideRandomWord();
            }

            Console.WriteLine("All words hidden. Press Enter to continue to the next scripture.");
            Console.ReadLine();
        }
    }

    static List<Scripture> LoadScripturesFromFile(string filePath)
    {
        List<Scripture> scriptures = new List<Scripture>();

        try
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                string[] parts = line.Split('|');
                string reference = parts[0];
                string text = parts[1];
                scriptures.Add(new Scripture(reference, text));
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found. Please provide a valid file path.");
            Environment.Exit(0);
        }

        return scriptures;
    }
}
