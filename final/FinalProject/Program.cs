using System;
using System.Collections.Generic;

// Base class for User
public class User {
    public string Username { get; private set; }
    private string Password { get; set; }

    // Constructor
    public User(string username, string password) {
        Username = username;
        Password = password;
    }

    // Method to authenticate user login
    public bool Authenticate(string password) {
        return Password == password;
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
            if (user.Username == username && user.Authenticate(password)) {
                Console.WriteLine($"User '{username}' has been authenticated.");
                return true;
            }
        }
        Console.WriteLine("Invalid username or password.");
        return false;
    }
}

// Abstract class for managing transactions
public abstract class Transaction {
    public string Type { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime Date { get; private set; }
    public string Category { get; private set; }
    public string Description { get; private set; }

    // Constructor
    protected Transaction(string type, decimal amount, DateTime date, string category, string description) {
        Type = type;
        Amount = amount;
        Date = date;
        Category = category;
        Description = description;
    }

    // Abstract method for generating transaction report
    public abstract void GenerateReport();
}

// Class for managing income
public class Income : Transaction {
    public Income(decimal amount, DateTime date, string category, string description) 
        : base("Income", amount, date, category, description) {
    }

    // Override method for generating transaction report
    public override void GenerateReport() {
        Console.WriteLine($"Income: Amount {Amount}, Category: {Category}, Description: {Description}");
    }
}

// Class for managing expenses
public class Expense : Transaction {
    public Expense(decimal amount, DateTime date, string category, string description) 
        : base("Expense", amount, date, category, description) {
    }

    // Override method for generating transaction report
    public override void GenerateReport() {
        Console.WriteLine($"Expense: Amount {Amount}, Category: {Category}, Description: {Description}");
    }
}

// Class for managing budgets
public class Budget {
    public string Category { get; private set; }
    public decimal Amount { get; private set; }

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
        foreach (var transaction in transactions) {
            transaction.GenerateReport();
        }
    }

    // Method to generate budget report
    public void GenerateBudgetReport(List<Budget> budgets) {
        Console.WriteLine("Generating budget report...");
        foreach (var budget in budgets) {
            Console.WriteLine($"Budget: Category {budget.Category}, Amount {budget.Amount}");
        }
    }
}

// Class for managing savings goals
public class SavingsGoal {
    public string Name { get; private set; }
    public decimal TargetAmount { get; private set; }
    public decimal CurrentAmount { get; private set; }

    // Constructor
    public SavingsGoal(string name, decimal targetAmount) {
        Name = name;
        TargetAmount = targetAmount;
        CurrentAmount = 0;
    }

    // Method to deposit money into the savings goal
    public void Deposit(decimal amount) {
        CurrentAmount += amount;
        Console.WriteLine($"Deposited {amount} into savings goal '{Name}'. Current amount: {CurrentAmount}");
    }

    // Method to withdraw money from the savings goal
    public void Withdraw(decimal amount) {
        if (amount <= CurrentAmount) {
            CurrentAmount -= amount;
            Console.WriteLine($"Withdrew {amount} from savings goal '{Name}'. Current amount: {CurrentAmount}");
        } else {
            Console.WriteLine("Insufficient funds.");
        }
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
            new Expense(100.50m, DateTime.Now, "Groceries", "Weekly grocery shopping"),
            new Income(2000m, DateTime.Now, "Salary", "Monthly salary"),
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

        // Test savings goal
        SavingsGoal savingsGoal = new SavingsGoal("Vacation", 500);
        savingsGoal.Deposit(100);
        savingsGoal.Withdraw(50);
    }
}
