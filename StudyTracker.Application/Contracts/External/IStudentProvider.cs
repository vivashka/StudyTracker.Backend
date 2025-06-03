using StudyTracker.Domain.Models;

namespace StudyTracker.Application.Contracts.External;

public interface IStudentProvider
{
    public Task<Student> Authenticate(string login, CancellationToken cancellationToken);
}