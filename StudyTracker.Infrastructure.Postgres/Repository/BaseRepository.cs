using Dapper;
using Npgsql;

namespace StudyTracker.Infrastructure.Postgres.Repository;

public class BaseRepository
{
    private const string DbConnectionString = "POSTGRES_CONNECTION_STRING";
    
    //Взять список объектов
    protected async Task<TModel[]> ExecuteQueryAsync<TModel>(
        string sql,
        object param,
        CancellationToken token)
    {
        var command = new CommandDefinition(sql, param, cancellationToken: token);

        await using var connection = GetConnection();
        var result = await connection.QueryAsync<TModel>(command);
        return result.ToArray();
    }
    
    //Взять один объект
    protected async Task<TModel> ExecuteQuerySingleAsync<TModel>(
        string sql,
        object param,
        CancellationToken token)
    {
        var command = new CommandDefinition(sql, param, cancellationToken: token);

        await using var connection = GetConnection();
        var result = await connection.QuerySingleAsync<TModel>(command);
        return result;
    }
    
    protected async Task<TResult?> ExecuteNonQueryAsync<TResult>(
        string sql,
        object param,
        CancellationToken token)
    {
        var command = new CommandDefinition(
            sql, param,
            commandTimeout: CommandTimeout.Medium,
            cancellationToken: token);

        await using var connection = GetConnection();

        return await connection.ExecuteScalarAsync<TResult?>(command);
    }
    
    // Вспомогательный метод для выполнения скалярных запросов
    protected async Task<T> ExecuteScalarAsync<T>(string sql, object param, CancellationToken cancellationToken)
    {
        await using var connection = GetConnection();
        return await connection.ExecuteScalarAsync<T>(sql, param);
    }
    
    private NpgsqlConnection GetConnection()
    {
        var connectionString = Environment.GetEnvironmentVariable(DbConnectionString);
        return new NpgsqlConnection(connectionString);
    }
    
    private static class CommandTimeout
    {
        public static int Medium => 5;
        
        public static int Long => 30;
    }
}