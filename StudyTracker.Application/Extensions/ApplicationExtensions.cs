using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudyTracker.Application.Contracts;
using StudyTracker.Application.Models;
using StudyTracker.Application.Services;

namespace StudyTracker.Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection ConfigureApplicationExtensions(this IServiceCollection service, IConfiguration configuration)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        service.AddScoped<IStudentService, StudentService>();
        service.AddScoped<ICoursesService, CoursesService>();
        
        service.Configure<Admin>(configuration.GetSection(nameof(Admin)));

        return service;
    }
}