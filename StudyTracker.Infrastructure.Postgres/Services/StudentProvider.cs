using StudyTracker.Application.Contracts.External;
using StudyTracker.Domain.Models;
using StudyTracker.Infrastructure.Postgres.Contracts;

namespace StudyTracker.Infrastructure.Postgres.Services;

public class StudentProvider(IStudentRepository studentRepository) : IStudentProvider
{
    public async Task<Student> Authenticate(string login, CancellationToken cancellationToken)
    {
        try
        {
            var student = await studentRepository.Authenticate(login, cancellationToken);
            
            return student;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}