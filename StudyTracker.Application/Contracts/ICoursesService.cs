using StudyTracker.Application.Models;
using StudyTracker.Domain.Models;

namespace StudyTracker.Application.Contracts;

public interface ICoursesService
{
    public Task<ResponseModel<Course[]>> GetCourses(Guid studentId);
    
    public Task<ResponseModel<Course>> CreateCourse(Course course, Guid studentId);
    
    public Task<ResponseModel<Guid>> AssignCourse(Guid courseId, Guid studentId);
    
    public Task<ResponseModel<bool>> DeleteCourse(Guid courseId, Guid studentId);
}