namespace Expense_Tracer_CLI.Until;

public static class Message
{
    public const string TitleProgram = "=========================Expense Tracer CLI=========================";
    public const string FirstMessage = "expense-tracker ";
    public const string InputErrorMessage = "\nWrong input format. Use \"help\" for more information.\n";
    public const string ExceptionMessage = "\nAn error occurred, please try again. If the error persists, please contact support.\n";
    public const string WrongCommand = "Wrong command. Use \"help\" for more information.\n";
    public const string ValidationAddErrorMessage = "Error: The amount cannot be negative, and the description cannot be empty. Please try again.\n";
    
    public static string ItemAddedSuccessfully(int itemId)
    {
        return $"Expense added successfully (ID: {itemId})\n";
    }
}