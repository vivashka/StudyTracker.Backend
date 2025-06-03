using StudyTracker.Domain.Models;

namespace StudyTracker.Infrastructure.Postgres.Contracts;

public interface IStudentRepository
{
    public Task<Student> Authenticate(string login, CancellationToken cancellationToken);
}