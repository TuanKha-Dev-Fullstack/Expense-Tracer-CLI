using Expense_Tracer_CLI.Services;

namespace Expense_Tracer_CLI.Controller;

public class ExpensiveController
{
    private readonly ExpensiveService _expensiveService = new();

    public void Add(string?[] commandParts) => _expensiveService.Add(commandParts);
}