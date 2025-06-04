using Microsoft.Extensions.DependencyInjection;
using StudyTracker.Application.Contracts;
using StudyTracker.Application.Contracts.External;
using StudyTracker.Infrastructure.Migrator.Extensions;
using StudyTracker.Infrastructure.Postgres.Contracts;
using StudyTracker.Infrastructure.Postgres.Repository;
using StudyTracker.Infrastructure.Postgres.Services;

namespace StudyTracker.Infrastructure.Postgres.Extensions;

public static class PostgresExtensions
{
    private const string DbConnectionString = "POSTGRES_CONNECTION_STRING";
    
    public static IServiceCollection ConfigurePostgresInfrastructure(this IServiceCollection service)
    {
        string? connectionString = Environment.GetEnvironmentVariable(DbConnectionString);
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                $"Отсутствует переменная окружения {DbConnectionString}. Заполните ее и перезапустите приложение");
        }
        string connectionStrings = GetConnectionString();

        service.ConfigureRepositories();
        service.ConfigureServices();

        service.AddMigration(connectionStrings);
        return service;
    }

    private static void ConfigureRepositories(this IServiceCollection service)
    {
        service.AddScoped<IStudentRepository, StudentRepository>();
        service.AddScoped<ICoursesRepository, CoursesRepository>();
        service.AddScoped<IAssignmentsRepository, AssignmentsRepository>();
    }
    
    private static void ConfigureServices(this IServiceCollection service)
    {
        service.AddScoped<IStudentProvider, StudentProvider>();
        service.AddScoped<ICoursesProvider, CoursesProvider>();
        service.AddScoped<IAssignmentProvider, AssignmentProvider>();
    }
    
    private static string GetConnectionString()
    {
        string? connectionString = Environment.GetEnvironmentVariable(DbConnectionString);

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                $"Отсутствует переменная окружения {DbConnectionString}. Заполните ее и перезапустите приложение");
        }

        return connectionString;
    }
}