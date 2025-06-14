using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StudyTracker.Application.Contracts;
using StudyTracker.Application.Contracts.External;
using StudyTracker.Application.Models;
using StudyTracker.Domain.Models;

namespace StudyTracker.Application.Services;

public class StudentService(IStudentProvider studentProvider, IOptions<Admin> admin, ILogger<StudentService> logger) : IStudentService
{
    public async Task<ResponseModel<Student>> Authenticate(string login, string password)
    {
        try
        {
            var student = await studentProvider.Authenticate(login, CancellationToken.None);

            if  (student.StudentId == new Guid(admin.Value.AdminId))
            {
                student.IsAdmin = true;
            }

            if (student.Password.Equals(password))
            {
                return new ResponseModel<Student>(student, true, null);
            }
            return new ResponseModel<Student>(new Student(),
                    false,
                    new ActionErrorModel(200, "Данные пользователя не совпадают с текущими"));
        }
        catch (Exception ex)
        {
            return new ResponseModel<Student>(new Student(),
                false,
                new ActionErrorModel(400, "Непредвиденная ошибка" + ex.Message));
        }
    }

    public async Task<ResponseModel<Student>> Registration(string login, string password)
    {
        try
        {
            var curHash = CreateMd5(password + login, Encoding.GetEncoding(866));
            var student = await studentProvider.Registration(login, curHash.ToLower(), CancellationToken.None);

            if  (student.StudentId == new Guid(admin.Value.AdminId))
            {
                student.IsAdmin = true;
            }

            if (student.StudentId != Guid.Empty)
            {
                return new ResponseModel<Student>(student, true, null);
            }
            return new ResponseModel<Student>(new Student(),
                false,
                new ActionErrorModel(200, "Данные пользователя не совпадают с текущими"));
        }
        catch (Exception ex)
        {
            return new ResponseModel<Student>(new Student(),
                false,
                new ActionErrorModel(400, "Непредвиденная ошибка" + ex.Message));
        }
    }

    public async Task<ResponseModel<List<Student?>>> GetUsers()
    {
        try
        {
            var students = await studentProvider.GetUsers(CancellationToken.None);

            List<Student?> newStudents = new List<Student?>();

            if  (students.Any(s => s.StudentId == new Guid(admin.Value.AdminId)))
            {
                newStudents.AddRange(students);
                newStudents.RemoveAt(newStudents.IndexOf(newStudents.FirstOrDefault(s => s.StudentId == new Guid(admin.Value.AdminId)))) ;
            }

            if (newStudents.Count > 0)
            {
                return new ResponseModel<List<Student?>>(newStudents,
                    true, null);
            }
            return new ResponseModel<List<Student?>>(newStudents,
                true,
                new ActionErrorModel(200, "Пользователи не найдены"));
        }
        catch (Exception ex)
        {
            return new ResponseModel<List<Student?>>(new List<Student?>(),
                false,
                new ActionErrorModel(400, "Непредвиденная ошибка" + ex.Message));
        }
    }

    public async Task<ResponseModel<List<Student?>>> GetUsersByCourse(Guid courseId)
    {
        try
        {
            var students = await studentProvider.GetUsersByCourse(courseId, CancellationToken.None);

            List<Student?> newStudents = students.ToList()!;

            // if  (students.Any(s => s.StudentId == new Guid(admin.Value.AdminId)))
            // {
            //     newStudents.AddRange(students);
            //     newStudents.Remove(newStudents.FirstOrDefault(s => s.StudentId == new Guid(admin.Value.AdminId)));
            //     logger.LogInformation(newStudents.Count.ToString());
            // }
            
            logger.LogInformation("Количество студентов {x}", newStudents.Count.ToString());
            if (newStudents.Count > 0)
            {
                return new ResponseModel<List<Student?>>(newStudents,
                    true, null);
            }
            return new ResponseModel<List<Student?>>(newStudents,
                true,
                new ActionErrorModel(200, "Пользователи не найдены"));
        }
        catch (Exception ex)
        {
            return new ResponseModel<List<Student?>>(new List<Student?>(),
                false,
                new ActionErrorModel(400, "Непредвиденная ошибка" + ex.Message));
        }
    }
    
    private static string CreateMd5(string text, Encoding encoding)
    {
        var inputBytes = encoding.GetBytes(text);

        var hashBytes = MD5.HashData(inputBytes);

        return Convert.ToHexString(hashBytes);
    }
}