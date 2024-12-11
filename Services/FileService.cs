namespace Expense_Tracer_CLI.Services;

public class FileService
{
    private readonly string _fileName;
    private readonly string _folderName;
    
    public FileService(string folderName, string fileName) 
    {
        _folderName = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        _fileName = fileName;
        CheckCreateNewFolder();
    }

    private void CheckCreateNewFolder()
    {
        if (!Directory.Exists(_folderName))
        {
            Directory.CreateDirectory(_folderName);
        }
    }
    
    public string GetFilePath() => Path.Combine(_folderName, _fileName);
}