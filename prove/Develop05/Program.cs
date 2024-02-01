using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace EternalQuest
{
    // Base class for all types of goals
    [Serializable]
   class Goal
{
    protected internal string Name { get; private set; }
    protected internal int Points { get; protected set; }  // Change the set accessor to protected

    public Goal(string name)
    {
        Name = name;
        Points = 0;
    }

    public virtual void MarkComplete()
    {
        Console.WriteLine($"Goal '{Name}' marked as complete!");
        Points += CalculatePoints();
    }

    protected virtual int CalculatePoints()
    {
        // Default implementation for simple goals
        return 100;
    }

    public int GetPoints()
    {
        return Points;
    }
}

    // Class for eternal goals
    [Serializable]
    class EternalGoal : Goal
    {
        public EternalGoal(string name) : base(name) { }

        // Override CalculatePoints for eternal goals
        protected override int CalculatePoints()
        {
            // Points for eternal goals
            return 100;
        }
    }

    // Class for checklist goals
    [Serializable]
    class ChecklistGoal : Goal
    {
        private readonly int targetCount;
        private int completedCount;

        public ChecklistGoal(string name, int targetCount) : base(name)
        {
            this.targetCount = targetCount;
            completedCount = 0;
        }

        public override void MarkComplete()
        {
            completedCount++;
            Console.WriteLine($"Completed {completedCount}/{targetCount} times for goal '{Name}'.");
            if (completedCount == targetCount)
            {
                base.MarkComplete(); // Calls the base class method for bonus points
            }
        }

        // Override CalculatePoints for checklist goals
        protected override int CalculatePoints()
        {
            int points = 50;
            if (completedCount == targetCount)
            {
                points += 500; // Bonus points for completing the checklist
                Console.WriteLine($"Checklist goal '{Name}' completed!");
            }
            return points;
        }

        public string GetProgress()
        {
            return $"Completed {completedCount}/{targetCount} times";
        }
    }

    // Class for leveling goals
    [Serializable]
    class LevelingGoal : Goal
    {
        public int Level { get; private set; }

        public LevelingGoal(string name) : base(name)
        {
            Level = 1;
        }

        public override void MarkComplete()
        {
            base.MarkComplete(); // Calls the base class method for points
            LevelUp();
        }

        private void LevelUp()
        {
            Level++;
            Console.WriteLine($"Level up! {Name} is now at Level {Level}.");
        }
    }

    // Class for bonus goals
    [Serializable]
    class BonusGoal : Goal
    {
        public BonusGoal(string name) : base(name) { }

        public override void MarkComplete()
        {
            base.MarkComplete(); // Calls the base class method for points
            EarnBonus();
        }

        private void EarnBonus()
        {
            Points += 200; // Bonus points for completing the goal
            Console.WriteLine($"Bonus points earned! Total points for {Name}: {Points}.");
        }
    }

    // Class for negative goals
    [Serializable]
    class NegativeGoal : Goal
    {
        public NegativeGoal(string name) : base(name) { }

        public override void MarkComplete()
        {
            base.MarkComplete(); // Calls the base class method for points
            LosePoints();
        }

       private void LosePoints()
{
    Points -= 150; // Corrected penalty for completing the negative goal
    Console.WriteLine($"Oops! Points deducted for {Name}. Total points: {Points}.");
}
    }

    class Program
    {
        private static List<Goal> goals = new List<Goal>();
        private static int userScore = 0;

        static void Main()
        {
            LoadGoals();
            DisplayGoals();

            // Sample usage of the Eternal Quest program
            RecordEvent(new Goal("Run a Marathon"));
            RecordEvent(new EternalGoal("Read Scriptures"));
            RecordEvent(new ChecklistGoal("Attend Temple", 3));
            RecordEvent(new LevelingGoal("Learn a new skill"));
            RecordEvent(new BonusGoal("Complete a challenging task"));
            RecordEvent(new NegativeGoal("Skip a workout"));

            SaveGoals();
            DisplayGoals();
        }

        static void RecordEvent(Goal goal)
        {
            goals.Add(goal);
            goal.MarkComplete();
            userScore += goal.GetPoints();
        }

        static void DisplayGoals()
        {
            Console.WriteLine("Current Goals:");
            foreach (Goal goal in goals)
            {
                string completionStatus = goal is ChecklistGoal ? ((ChecklistGoal)goal).GetProgress() : goal.GetPoints() > 0 ? "[X]" : "[ ]";
                Console.WriteLine($"{goal.Name}: {completionStatus}");
            }
            Console.WriteLine($"User Score: {userScore}\n");
        }

        static void SaveGoals()
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(goals);
                File.WriteAllText("goals.json", jsonString);
                Console.WriteLine("Goals saved successfully.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving goals: {ex.Message}\n");
            }
        }

        static void LoadGoals()
        {
            try
            {
                if (File.Exists("goals.json"))
                {
                    string jsonString = File.ReadAllText("goals.json");
                    goals = JsonSerializer.Deserialize<List<Goal>>(jsonString);
                    Console.WriteLine("Goals loaded successfully.\n");
                }
                else
                {
                    Console.WriteLine("No saved goals found.\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading goals: {ex.Message}\n");
            }
        }
    }
}
