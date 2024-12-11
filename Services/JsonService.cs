using System.Text.Json;
using Expense_Tracer_CLI.Models;
using Expense_Tracer_CLI.Until;

namespace Expense_Tracer_CLI.Services;

public class JsonService
{
    private const string FileName = "expenses.json";
    private const string FolderName = "Data";
    private static FileService? _fileService;

    public JsonService() => _fileService = new FileService(FolderName, FileName);

    public static List<Expense> LoadExpensiveFromFile()
    {
        try
        {
            if (!File.Exists(_fileService?.GetFilePath())) return [];
            var json = File.ReadAllText(_fileService.GetFilePath());
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
            File.WriteAllText(_fileService?.GetFilePath() ?? string.Empty, json);
        }
        catch (Exception)
        {
            Console.WriteLine(Message.ExceptionMessage);
        }
    }
}