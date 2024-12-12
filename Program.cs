using Expense_Tracer_CLI.Controller;
using Expense_Tracer_CLI.Until;

namespace Expense_Tracer_CLI;

public static class Program
{
    private static void Main()
    {
        Console.WriteLine(Message.TitleProgram);
        string? userInput;
        do
        {
            userInput = HandleInput.PromtUserInput();
            if (string.IsNullOrEmpty(userInput)) continue;
            var commandParts = HandleInput.ParseInput(userInput, Message.InputErrorMessage);
            HandleInput.CheckCommand(commandParts[0]);
            ChooseAction(commandParts);
        } while (userInput != CommandManager.Exit);
    }

    private static void ChooseAction(string?[] commandParts)
    {
        switch (commandParts[0])
        {
            case CommandManager.Add:
                ExpensiveController.Add(commandParts);
                break;
            case CommandManager.List:
                ExpensiveController.List(commandParts);
                break;
            case CommandManager.Delete:
                ExpensiveController.Delete(commandParts);
                break;
            case CommandManager.Summary:
                ExpensiveController.Summary(commandParts);
                break;
            case CommandManager.Help:
                break;
            case CommandManager.Exit:
                break;
        }
    }
}