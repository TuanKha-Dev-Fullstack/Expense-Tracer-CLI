using System.Text.RegularExpressions;

namespace Expense_Tracer_CLI.Until;

public static partial class CustomRegex
{
    [GeneratedRegex("""
                    "(?:[^"\\]|\\.)*"|[^ ]+
                    """)]
    public static partial Regex RegexInput();
}