using System.Text.Json;
using Expense_Tracer_CLI.Models;
using Expense_Tracer_CLI.Until;

namespace Expense_Tracer_CLI.Services;

public abstract class JsonService
{
    private const string FileName = "expenses.json";

    public static List<Expense> LoadExpensiveFromFile()
    {
        try
        {
            if (!File.Exists(FileName)) return [];
            var json = File.ReadAllText(FileName);
            return JsonSerializer.Deserialize<List<Expense>>(json) ?? [];
        }
        catch (Exception)
        {
            Console.WriteLine(Message.ExceptionMessage);
            throw;
        }
    }
    
    public static void SaveExpensive(List<Expense> expenses)
    {
        try
        {
            var json = JsonSerializer.Serialize(expenses);
            File.WriteAllText(FileName, json);
        }
        catch (Exception)
        {
            Console.WriteLine(Message.ExceptionMessage);
        }
    }
}