using System.Security.Cryptography;
using System.Text;
using StudyTracker.Application.Contracts;
using StudyTracker.Application.Contracts.External;
using StudyTracker.Application.Models;
using StudyTracker.Domain.Models;

namespace StudyTracker.Application.Services;

public class StudentService(IStudentProvider studentProvider) : IStudentService
{
    public async Task<ResponseModel<Student>> Authenticate(string login, string password)
    {
        try
        {
            var student = await studentProvider.Authenticate(login, CancellationToken.None);
            

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
    
    private static string CreateMd5(string text, Encoding encoding)
    {
        var inputBytes = encoding.GetBytes(text);

        var hashBytes = MD5.HashData(inputBytes);

        return Convert.ToHexString(hashBytes);
    }
}