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

    public Task<Student> Registration(string login, string password, CancellationToken cancellationToken)
    {
        try
        {
            var student = studentRepository.Registration(login, password, cancellationToken);
            
            return student;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<Student[]> GetUsers(CancellationToken cancellationToken)
    {
        try
        {
            var student = studentRepository.GetUsers(cancellationToken);
            
            return student;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<Student[]> GetUsersByCourse(Guid courseId, CancellationToken cancellationToken)
    {
        try
        {
            var student = studentRepository.GetUsersByCourse(courseId, cancellationToken);
            
            return student;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}