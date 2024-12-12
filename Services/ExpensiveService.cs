using Expense_Tracer_CLI.Models;
using Expense_Tracer_CLI.Until;

namespace Expense_Tracer_CLI.Services;

public class ExpensiveService
{
    private readonly List<Expense> _expenses = JsonService.LoadExpensiveFromFile();

    public void Add(string?[] commandParts)
    {
        if (!CheckParameters(commandParts))
        {
            Console.WriteLine(Message.InputErrorMessage);
        }
        else
        {
            var values =GetValueOfAddCommand(commandParts);
            if (values.Length == 0)
            {
                Console.WriteLine(Message.InputErrorMessage);
            }
            else
            {
                var description = values[0];
                var amount = decimal.Parse(values[1] ?? string.Empty);
                if(amount < 0 || string.IsNullOrEmpty(description))
                    Console.WriteLine(Message.ValidationAddErrorMessage);
                else
                { 
                    AddNew(description, amount);
                }
            }
        }
    }

    private void AddNew(string description, decimal amount)
    {
        try
        {
            var newId = _expenses.Count == 0 ? 1 : _expenses.Max(expense => expense.Id) + 1;
            var newExpense = new Expense
            {
                Id = newId,
                Description = description,
                Amount = amount,
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

    private static bool CheckParameters(string?[] commandParts)
    {
        return commandParts.Contains(Flags.Description) && commandParts.Contains(Flags.Amount);
    }
    
    private static string?[] GetValueOfAddCommand(string?[] commandParts)
    {
        if(commandParts.Length < 5 || commandParts[1] != Flags.Description || commandParts[^2] != Flags.Amount || string.IsNullOrEmpty(commandParts[^1]) || !decimal.TryParse(commandParts[^1], out _))
            return [];
        var values = new string?[2];
        values[0] = string.Join(" ", commandParts[2..^2]);
        values[1] = commandParts[^1];
        return values;
    }
}