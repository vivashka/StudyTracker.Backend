using Microsoft.Extensions.Options;
using StudyTracker.Application.Contracts;
using StudyTracker.Application.Contracts.External;
using StudyTracker.Application.Models;
using StudyTracker.Domain.Models;

namespace StudyTracker.Application.Services;

public class CoursesService(
    ICoursesProvider coursesProvider,
    IOptions<Admin> admin) : ICoursesService
{
    public async Task<ResponseModel<Course[]>> GetCourses(Guid studentId)
    {
        try
        {
            Course[] courses;
            if (studentId == new Guid(admin.Value.AdminId))
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

    public async Task<ResponseModel<Course>> CreateCourse(Course course, Guid studentId)
    {
        try
        {
            if (studentId == new Guid(admin.Value.AdminId))
            {
                var newCourse = await coursesProvider.CreateCourse(course, CancellationToken.None);

                if (newCourse.Name != course.Name)
                {
                    return new ResponseModel<Course>(newCourse, true,
                        new ActionErrorModel(200, "Не удалось добавить курс. Возможно такой курс уже существует."));
                }
                return new ResponseModel<Course>(newCourse, true, null);
            }

            return new ResponseModel<Course>(new Course(), true,
                new ActionErrorModel(200, "Не удалось добавить курс. Пользователь не является администратором"));
        }
        catch (Exception ex)
        {
            return new ResponseModel<Course>(new Course(),
                false,
                new ActionErrorModel(400, "Непредвиденная ошибка " + ex.Message));
        }
    }

    public async Task<ResponseModel<Guid>> AssignCourse(Guid courseId, Guid studentId)
    {
        try
        {
            if (studentId == new Guid(admin.Value.AdminId))
            {
                var student = await coursesProvider.AssignCourse(studentId, courseId, CancellationToken.None);
                
                return new ResponseModel<Guid>(student, true, null);
            }
            return new ResponseModel<Guid>(new Guid(), true,
                new ActionErrorModel(200, "Не удалось назначить студента на курс. Пользователь не является администратором"));
        }
        catch (Exception ex)
        {
            return new ResponseModel<Guid>(new Guid(),
                false,
                new ActionErrorModel(400, "Непредвиденная ошибка " + ex.Message));
        }
    }

    public async Task<ResponseModel<bool>> DeleteCourse(Guid courseId, Guid studentId)
    {
        try
        {
            if (studentId == new Guid(admin.Value.AdminId))
            {
                var student = await coursesProvider.DeleteCourse(courseId, CancellationToken.None);
                if (student.CourseId == null)
                {
                    return new ResponseModel<bool>(false, true, null);
                }
                
                return new ResponseModel<bool>(true, true, null);
            }
            return new ResponseModel<bool>(false, true,
                new ActionErrorModel(200, "Не удалось удалить курс. Пользователь не является администратором"));
        }
        catch (Exception ex)
        {
            return new ResponseModel<bool>(false,
                false,
                new ActionErrorModel(400, "Непредвиденная ошибка " + ex.Message));
        }
    }
}