﻿namespace Expense_Tracer_CLI.Models;

public class Expense
{
    public int Id { get; init; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}