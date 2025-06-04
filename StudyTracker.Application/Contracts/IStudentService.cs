using StudyTracker.Application.Models;
using StudyTracker.Domain.Models;

namespace StudyTracker.Application.Contracts;

public interface IStudentService
{
    public Task<ResponseModel<Student>> Authenticate(string login, string password);
}