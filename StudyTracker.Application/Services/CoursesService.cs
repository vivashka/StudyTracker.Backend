using StudyTracker.Application.Contracts;
using StudyTracker.Application.Contracts.External;
using StudyTracker.Application.Models;
using StudyTracker.Domain.Models;

namespace StudyTracker.Application.Services;

public class CoursesService(ICoursesProvider coursesProvider,
    Admin admin) : ICoursesService
{
    
    public async Task<ResponseModel<Course[]>> GetCourses(Guid studentId)
    {
        try
        {
            Course[] courses;
            if (studentId == admin.AdminId)
            {
                courses = await coursesProvider.GetCourses(CancellationToken.None);
            }
            else
            {
                courses = await coursesProvider.GetCoursesByStudent(studentId, CancellationToken.None);
            }
            
            if (courses.Length > 0)
            {
                return new ResponseModel<Course[]>(courses, true, null);
            }

            return new ResponseModel<Course[]>(courses,
                true,
                new ActionErrorModel(200, "Курсов не найдено"));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new ResponseModel<Course[]>([],
                false,
                new ActionErrorModel(400, "Непредвиденная ошибка " + ex.Message));
        }
    }
}