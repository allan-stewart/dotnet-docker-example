using Microsoft.Extensions.Configuration;

namespace ToDo.IntegrationTests
{
    public class TestSettings
    {
        static readonly IConfigurationRoot ConfigurationRoot;

        static TestSettings()
        {
            ConfigurationRoot = new ConfigurationBuilder()
                .AddEnvironmentVariables(prefix: "TODO_")
                .Build();
        }

        public PostgresSettings PostgresSettings => new PostgresSettings
        {
            Host = ConfigurationRoot.GetValue<string>("POSTGRES_HOST"),
            Database = ConfigurationRoot.GetValue<string>("POSTGRES_DATABASE"),
            Username = ConfigurationRoot.GetValue<string>("POSTGRES_USERNAME"),
            Password = ConfigurationRoot.GetValue<string>("POSTGRES_PASSWORD")
        };
    }
}
