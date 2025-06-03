using StudyTracker.Application.Models;
using StudyTracker.Domain.Models;

namespace StudyTracker.Application.Contracts;

public interface ICoursesService
{
    public Task<ResponseModel<Course[]>> GetCourses(Guid studentId);
}