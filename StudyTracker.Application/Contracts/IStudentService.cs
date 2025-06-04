using StudyTracker.Application.Models;
using StudyTracker.Domain.Models;

namespace StudyTracker.Application.Contracts;

public interface IStudentService
{
    public Task<ResponseModel<Student>> Authenticate(string login, string password);
    
    public Task<ResponseModel<Student>> Registration(string login, string password);
    
    public Task<ResponseModel<List<Student?>>> GetUsers();
    
    public Task<ResponseModel<List<Student?>>> GetUsersByCourse(Guid courseId);
}