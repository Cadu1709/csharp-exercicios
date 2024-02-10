using System;
using System.Collections.Generic;

// Base class for User
public class User {
    public string Username { get; set; }
    public string Password { get; set; }

    // Constructor
    public User(string username, string password) {
        Username = username;
        Password = password;
    }
}

// Class for managing user authentication
public class Authentication {
    private List<User> users;

    // Constructor
    public Authentication() {
        users = new List<User>();
    }

    // Method to register a new user
    public void Register(string username, string password) {
        users.Add(new User(username, password));
        Console.WriteLine($"User '{username}' has been registered.");
    }

    // Method to authenticate user login
    public bool Authenticate(string username, string password) {
        foreach (var user in users) {
            if (user.Username == username && user.Password == password) {
                Console.WriteLine($"User '{username}' has been authenticated.");
                return true;
            }
        }
        Console.WriteLine("Invalid username or password.");
        return false;
    }
}

// Class for managing transactions
public class Transaction {
    public string Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }

    // Constructor
    public Transaction(string type, decimal amount, DateTime date, string category, string description) {
        Type = type;
        Amount = amount;
        Date = date;
        Category = category;
        Description = description;
    }
}

// Class for managing budgets
public class Budget {
    public string Category { get; set; }
    public decimal Amount { get; set; }

    // Constructor
    public Budget(string category, decimal amount) {
        Category = category;
        Amount = amount;
    }
}

// Class for generating reports
public class ReportGenerator {
    // Method to generate spending report
    public void GenerateSpendingReport(List<Transaction> transactions) {
        Console.WriteLine("Generating spending report...");
        // Logic to generate spending report
    }

    // Method to generate budget report
    public void GenerateBudgetReport(List<Budget> budgets) {
        Console.WriteLine("Generating budget report...");
        // Logic to generate budget report
    }
}

class Program {
    static void Main(string[] args) {
        // Create authentication instance
        Authentication authentication = new Authentication();

        // Register new users
        authentication.Register("user1", "password1");
        authentication.Register("user2", "password2");

        // Authenticate user login
        authentication.Authenticate("user1", "password1");

        // Sample transactions
        List<Transaction> transactions = new List<Transaction>() {
            new Transaction("Expense", 100.50m, DateTime.Now, "Groceries", "Weekly grocery shopping"),
            new Transaction("Income", 2000m, DateTime.Now, "Salary", "Monthly salary"),
        };

        // Sample budgets
        List<Budget> budgets = new List<Budget>() {
            new Budget("Groceries", 300m),
            new Budget("Entertainment", 100m),
        };

        // Generate reports
        ReportGenerator reportGenerator = new ReportGenerator();
        reportGenerator.GenerateSpendingReport(transactions);
        reportGenerator.GenerateBudgetReport(budgets);
    }
}
