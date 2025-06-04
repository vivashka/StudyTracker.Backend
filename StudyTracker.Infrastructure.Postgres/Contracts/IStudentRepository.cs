using StudyTracker.Domain.Models;

namespace StudyTracker.Infrastructure.Postgres.Contracts;

public interface IStudentRepository
{
    public Task<Student> Authenticate(string login, CancellationToken cancellationToken);
    
    public Task<Student> Registration(string login, string password, CancellationToken cancellationToken);
    
    public Task<Student[]> GetUsers(CancellationToken cancellationToken);

    public Task<Student[]> GetUsersByCourse(Guid courseId, CancellationToken cancellationToken);
}