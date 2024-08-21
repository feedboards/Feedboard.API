namespace Feedboard.API.Helplers;

public static class ConnectionStringHelper
{
    public static string GetDatabaseConnectionString()
    {
        var host = Environment.GetEnvironmentVariable(EnvConstants.DATABASE_HOST)
            ?? throw new InvalidOperationException($"{EnvConstants.DATABASE_HOST} is required");

        var port = Environment.GetEnvironmentVariable(EnvConstants.DATABASE_PORT)
            ?? "1433";

        var username = Environment.GetEnvironmentVariable(EnvConstants.DATABASE_USERNANE)
            ?? throw new InvalidOperationException($"{EnvConstants.DATABASE_USERNANE} is required");

        var password = Environment.GetEnvironmentVariable(EnvConstants.DATABASE_PASSWORD)
            ?? throw new InvalidOperationException($"{EnvConstants.DATABASE_PASSWORD} is required");

        var databaseName = Environment.GetEnvironmentVariable(EnvConstants.DATABASE_DATABASE_NAME)
            ?? throw new InvalidOperationException($"{EnvConstants.DATABASE_DATABASE_NAME} is required");

        var connectionString =
            $"Data Source={host},{port};" +
            $"Initial Catalog={databaseName};" +
            $"Persist Security Info=True;" +
            $"User ID={username};" +
            $"Password={password};" +
            $"TrustServerCertificate=True;";

        Console.WriteLine(connectionString);

        return connectionString;
    }
}
