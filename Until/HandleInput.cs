namespace Expense_Tracer_CLI.Until;

public abstract class HandleInput
{
    public static string? PromtUserInput()
    {
        Console.Write(Message.FirstMessage);
        return Console.ReadLine()?.Trim();
    }
    public static void CheckCommand(string command)
    {
        if (!CommandManager.ValidCommands.Contains(command))
            Console.WriteLine(Message.WrongCommand);
    }
    
    public static string[] ParseInput(string input, string inputErrorMessage)
    {
        if ((!input.Contains(CommandManager.Add) && !input.Contains(CommandManager.Update)) || input.Count(c => c == '\"') % 2 == 0)
            return CustomRegex.RegexInput().Matches(input)
                .Select(m => m.Value.Trim('"'))
                .ToArray();
        Console.WriteLine(inputErrorMessage);
        return [];
    }
}