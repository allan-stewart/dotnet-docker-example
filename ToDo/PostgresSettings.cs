namespace ToDo
{
    public class PostgresSettings
    {
        public string Host { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int CommandTimeoutSeconds { get; set; } = 30;
        public int MaxPoolSize { get; set; } = 50;
        public int TimeoutSeconds { get; set; } = 15;
    }
}
