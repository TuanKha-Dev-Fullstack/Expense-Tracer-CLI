namespace Expense_Tracer_CLI.Until
{
    public abstract class CommandManager
    {
        public const string Add = "add";
        public const string Update = "update";
        public const string List = "list";
        public const string Delete = "delete";
        public const string Summary = "summary";
        public const string Help = "help";
        public const string Exit = "exit";

        public static readonly HashSet<string> ValidCommands =
        [
            Add,
            Update,
            List,
            Delete,
            Summary,
            Help,
            Exit
        ];
    }
}