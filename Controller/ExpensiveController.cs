﻿using Expense_Tracer_CLI.Services;

namespace Expense_Tracer_CLI.Controller;

public static class ExpensiveController
{
    public static void Add(string?[] commandParts) => ExpensiveService.Add(commandParts);

    public static void List(string?[] commandParts) => ExpensiveService.List(commandParts);

    public static void Summary(string?[] commandParts) => ExpensiveService.Summary(commandParts);
    
    public static void Delete(string?[] commandParts) => ExpensiveService.Delete(commandParts);
}