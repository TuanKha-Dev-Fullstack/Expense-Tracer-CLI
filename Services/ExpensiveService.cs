using Expense_Tracer_CLI.Models;
using Expense_Tracer_CLI.Until;

namespace Expense_Tracer_CLI.Services;

public class ExpensiveService
{
    private readonly List<Expense> _expenses;

    public ExpensiveService()
    {
        _expenses = JsonService.LoadExpensiveFromFile();
        Console.WriteLine(_expenses);
    }
    public void Add(string?[] commandParts)
    {
        if (!CheckParameters(commandParts))
        {
            Console.WriteLine(Message.InputErrorMessage);
        }
        else
        {
            var values =GetValueOfAddCommand(commandParts);
            try
            {
                var newId = _expenses.Count == 0 ? 1 : _expenses.Max(expense => expense.Id) + 1;
                var newExpense = new Expense
                {
                    Id = newId,
                    Description = values[0],
                    Amount = decimal.Parse(values[1] ?? string.Empty),
                    Date = DateTime.Now
                };
                _expenses.Add(newExpense);
                JsonService.SaveExpensive(_expenses);
                Console.WriteLine(Message.ItemAddedSuccessfully(newId));
            } catch (Exception)
            {
                Console.WriteLine(Message.ExceptionMessage);
            }
        }
    }

    private static bool CheckParameters(string?[] commandParts)
    {
        return commandParts.Contains(Flags.Description) && commandParts.Contains(Flags.Amount);
    }
    
    private static string?[] GetValueOfAddCommand(string?[] commandParts)
    {
        var values = new string?[2];
        values[0] = string.Join(" ", commandParts[2..^2]);
        values[1] = commandParts[^1];
        return values;
    }
}