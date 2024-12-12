namespace Expense_Tracer_CLI.Models;

public class Expense
{
    public int Id { get; init; }
    public required string Description { get; init; }
    public decimal Amount { get; init; }
    public DateTime Date { get; init; }
}