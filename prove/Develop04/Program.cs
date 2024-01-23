using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

class MindfulnessActivity
{
    protected static Dictionary<string, int> activityCounts = new Dictionary<string, int>();
    
    protected string activityName;
    protected string description;
    protected HashSet<string> usedPrompts = new HashSet<string>();

    public MindfulnessActivity(string activityName, string description)
    {
        this.activityName = activityName;
        this.description = description;
        if (!activityCounts.ContainsKey(activityName))
            activityCounts[activityName] = 0;
    }

    public virtual void StartActivity(int duration)
    {
        ShowStartingMessage();
        Pause(3);  // Pause for preparation
        PerformActivity(duration);
        ShowEndingMessage(duration);
        LogActivityCount();
        ResetUsedPrompts();
    }

    protected void ShowStartingMessage()
    {
        Console.WriteLine($"\n{activityName} Activity - {description}");
        Console.WriteLine("Please enter the duration in seconds.");
    }

    protected virtual void PerformActivity(int duration)
    {
        throw new NotImplementedException("Subclasses must implement the PerformActivity method.");
    }

    protected void ShowEndingMessage(int duration)
    {
        Console.WriteLine($"Good job! You have completed the {activityName} activity for {duration} seconds.");
    }

    protected void Pause(int seconds)
    {
        // Implement animation or countdown timer during pause
        Console.WriteLine($"Pause for {seconds} seconds");
        Thread.Sleep(seconds * 1000);
    }

    protected void LogActivityCount()
    {
        activityCounts[activityName]++;
        Console.WriteLine($"Total times {activityName} activity has been performed: {activityCounts[activityName]}");
    }

    protected void ResetUsedPrompts()
    {
        usedPrompts.Clear();
    }
}

class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity() : base("Breathing", "Deep breathing exercise") { }

    public override void PerformActivity(int duration)
    {
        int breathsPerMinute = 12;  // Adjust as needed
        int breathDuration = 60 / breathsPerMinute;

        int breathCount = 0;
        while (breathCount < duration)
        {
            Console.WriteLine("Breathe in...");
            GrowTextAnimation(10);
            Pause(breathDuration / 2);
            Console.WriteLine("Breathe out...");
            GrowTextAnimation(10);
            Pause(breathDuration / 2);
            breathCount += breathDuration;
        }
    }

    private void GrowTextAnimation(int maxGrowth)
    {
        for (int i = 1; i <= maxGrowth; i++)
        {
            Console.Write("█");
            Thread.Sleep(50);
        }
        Console.WriteLine();
    }
}

class VisualizationActivity : MindfulnessActivity
{
    public VisualizationActivity() : base("Visualization", "Imaginary visualization activity") { }

    public override void PerformActivity(int duration)
    {
        Console.WriteLine("Imagine a peaceful place...");
        for (int i = 1; i <= duration; i++)
        {
            Console.Write(".");
            Thread.Sleep(1000);
        }
        Console.WriteLine("\nEnd of visualization");
    }
}

class ReflectionActivity : MindfulnessActivity
{
    private string[] reflectionPrompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private string[] reflectionQuestions = {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        // Add more questions as needed
    };

    public ReflectionActivity() : base("Reflection", "Reflect on past experiences") { }

    public override void PerformActivity(int duration)
    {
        string prompt = GetRandomPrompt();
        Console.WriteLine(prompt);
        Pause(3);  // Pause for preparation

        foreach (string question in reflectionQuestions)
        {
            Console.WriteLine(question);
            Pause(3);  // Pause for reflection

            // Implement spinner animation during reflection pause
        }
    }

    private string GetRandomPrompt()
    {
        string prompt;
        do
        {
            prompt = reflectionPrompts[new Random().Next(reflectionPrompts.Length)];
        } while (usedPrompts.Contains(prompt));

        usedPrompts.Add(prompt);
        return prompt;
    }
}

class ListingActivity : MindfulnessActivity
{
    private string[] listingPrompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        // Add more prompts as needed
    };

    public ListingActivity() : base("Listing", "List positive things in your life") { }

    public override void PerformActivity(int duration)
    {
        string prompt = GetRandomPrompt();
        Console.WriteLine(prompt);
        Pause(3);  // Pause for preparation

        int itemCount = 0;
        DateTime startTime = DateTime.Now;

        while ((DateTime.Now - startTime).TotalSeconds < duration)
        {
            Console.Write("Enter an item (or 'done' to finish): ");
            string item = Console.ReadLine();
            
            if (item.ToLower() == "done")
                break;

            itemCount++;
        }

        Console.WriteLine($"You listed {itemCount} items.");
    }

    private string GetRandomPrompt()
    {
        string prompt;
        do
        {
            prompt = listingPrompts[new Random().Next(listingPrompts.Length)];
        } while (usedPrompts.Contains(prompt));

        usedPrompts.Add(prompt);
        return prompt;
    }
}

class Program
{
    static void Main()
    {
        BreathingActivity breathingActivity = new BreathingActivity();
        ReflectionActivity reflectionActivity = new ReflectionActivity();
        ListingActivity listingActivity = new ListingActivity();
        VisualizationActivity visualizationActivity = new VisualizationActivity();

        // You can customize the duration as needed
        breathingActivity.StartActivity(60);
        reflectionActivity.StartActivity(60);
        listingActivity.StartActivity(60);
        visualizationActivity.StartActivity(30);
    }
}
