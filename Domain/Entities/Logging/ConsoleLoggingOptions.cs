namespace CommonSolution.Domain.Entities.Logging
{
    public class ConsoleLoggingOptions
    {
        public bool EnableConsole { get; set; } = true;
        public string Level { get; set; } = "Information";
    }
}
