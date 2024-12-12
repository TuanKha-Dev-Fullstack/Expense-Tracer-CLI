using Expense_Tracer_CLI.Models;
using Expense_Tracer_CLI.Until;

namespace Expense_Tracer_CLI.Services;

public static class ExpensiveService
{
    private static readonly List<Expense> Expenses = JsonService.LoadExpensiveFromFile();

    public static void Add(string?[] commandParts)
    {
        if (!CheckParameters(commandParts))
        {
            Console.WriteLine(Message.InputErrorMessage);
        }
        else
        {
            var values = GetValueOfAddCommand(commandParts);
            if (values.Length == 0)
            {
                Console.WriteLine(Message.InputErrorMessage);
            }
            else
            {
                var description = values[0];
                var amount = decimal.Parse(values[1] ?? string.Empty);
                if (amount < 0 || string.IsNullOrEmpty(description))
                    Console.WriteLine(Message.ValidationAddErrorMessage);
                else
                {
                    AddNew(description, amount);
                }
            }
        }
    }

    public static void List(string?[] commandParts)
    {
        if (commandParts.Length != 1)
            Console.WriteLine(Message.InputErrorMessage);
        else
            DisplayList(Expenses);
    }
    
    public static void Summary(string?[] commandParts)
    {
        switch (commandParts.Length)
        {
            case 1:
            {
                var totalAmount = Expenses.Sum(expense => expense.Amount);
                Console.WriteLine(Message.SummaryMessage(totalAmount));
                break;
            }
            case 3 when commandParts[1] == Flags.Month && short.TryParse(commandParts[2], out var month):
            {
                var totalAmount = Expenses
                    .Where(expense => expense.Date.Month == month && expense.Date.Year == DateTime.Now.Year)
                    .Sum(expense => expense.Amount);
                Console.WriteLine(Message.SummaryMonthlyMessage(totalAmount, month));
                break;
            }
            default:
                Console.WriteLine(Message.InputErrorMessage);
                break;
        }
    }

    private static void AddNew(string description, decimal amount)
    {
        try
        {
            var newId = Expenses.Count == 0 ? 1 : Expenses.Max(expense => expense.Id) + 1;
            var newExpense = new Expense
            {
                Id = newId,
                Description = description,
                Amount = amount,
                Date = DateTime.Now
            };
            Expenses.Add(newExpense);
            JsonService.SaveExpensive(Expenses);
            Console.WriteLine(Message.ItemAddedSuccessfully(newId));
        }
        catch (Exception)
        {
            Console.WriteLine(Message.ExceptionMessage);
        }
    }

    private static bool CheckParameters(string?[] commandParts)
    {
        return commandParts.Contains(Flags.Description) && commandParts.Contains(Flags.Amount);
    }

    private static string?[] GetValueOfAddCommand(string?[] commandParts)
    {
        if (commandParts.Length < 5 || commandParts[1] != Flags.Description || commandParts[^2] != Flags.Amount ||
            string.IsNullOrEmpty(commandParts[^1]) || !decimal.TryParse(commandParts[^1], out _))
            return [];
        var values = new string?[2];
        values[0] = string.Join(" ", commandParts[2..^2]);
        values[1] = commandParts[^1];
        return values;
    }

    private static void DisplayList(List<Expense> expenses)
    {
        if (expenses.Count == 0)
        {
            Console.WriteLine("\nNo expenses found.\n");
            return;
        }
        // Define padding for each column
        const int padding = 5;
        // Define column headers
        const string idHeader = "ID";
        const string dateHeader = "Date";
        const string descriptionHeader = "Description";
        const string amountHeader = "Amount";
        // Calculate the maximum width for each column
        var idWidth = Math.Max(idHeader.Length, expenses.Max(expense => expense.Id.ToString().Length));
        var dateWidth = Math.Max(dateHeader.Length, expenses.Max(expense => expense.Date.Date.ToString("yyyy-MM-dd").Length));
        var descriptionWidth = Math.Max(descriptionHeader.Length, expenses.Max(expense => expense.Description.Length));
        var amountWidth = Math.Max(amountHeader.Length, expenses.Max(expense => expense.Amount.ToString("C0").Length));
        // Print the header
        Console.WriteLine(
            $"\n{idHeader.PadRight(idWidth + padding)}" +
            $"{dateHeader.PadRight(dateWidth + padding)}" +
            $"{descriptionHeader.PadRight(descriptionWidth + padding)}" +
            $"{amountHeader.PadRight(amountWidth + padding)}");
        // Print the separator line
        const int paddingContents = 4;
        Console.WriteLine(new string('-',
            idWidth + dateWidth + descriptionWidth + amountWidth +
            paddingContents * padding)); // Adjust for padding
        // Print each task
        foreach (var expense in expenses)
        {
            Console.WriteLine(
                $"{expense.Id.ToString().PadRight(idWidth + padding)}" +
                $"{expense.Date.Date.ToString("yyyy-MM-dd").PadRight(dateWidth + padding)}" +
                $"{expense.Description.PadRight(descriptionWidth + padding)}" +
                $"{expense.Amount.ToString("C0").PadRight(amountWidth)}");
        }
        Console.WriteLine();
    }
}