using System.Security.Cryptography;
using System.Text;
using StudyTracker.Application.Contracts;
using StudyTracker.Application.Contracts.External;
using StudyTracker.Application.Models;

namespace StudyTracker.Application.Services;

public class StudentService(IStudentProvider studentProvider) : IStudentService
{
    public async Task<ResponseModel<bool>> Authenticate(string login, string password)
    {
        try
        {
            var student = await studentProvider.Authenticate(login, CancellationToken.None);

            
            var curHash = CreateMd5(password + login, Encoding.GetEncoding(866));

            if (IsAuth(student.Login, student.Password, curHash))
            {
                return new ResponseModel<bool>(true, true, null);
            }
            return new ResponseModel<bool>(false,
                    false,
                    new ActionErrorModel(200, "Данные пользователя не совпадают с текущими"));
        }
        catch (Exception ex)
        {
            return new ResponseModel<bool>(false,
                false,
                new ActionErrorModel(400, "Непредвиденная ошибка" + ex.Message));
        }
    }
    
    public static bool IsAuth(string login, string password, string expHash)
    {
        // если изначально хэш был пуст
        if (expHash == "20202020202020202020202020202020")
        {
            return false;
        }

        var extendedLogin = login;
        
        if(extendedLogin.Length < 5)
        {
            extendedLogin += " ";
        }

        var curHash = CreateMd5(password + extendedLogin, Encoding.GetEncoding(866));        

        if(curHash != expHash)
        {
            return false;
        }

        return true;
    }
    
    private static string CreateMd5(string text, Encoding encoding)
    {
        var inputBytes = encoding.GetBytes(text);

        var hashBytes = MD5.HashData(inputBytes);

        return Convert.ToHexString(hashBytes);
    }
}