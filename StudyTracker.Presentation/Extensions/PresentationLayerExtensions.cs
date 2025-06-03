using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using StudyTracker.Presentation.Controllers;

namespace StudyTracker.Presentation.Extensions;

public static class PresentationLayerExtensions
{
    public static IServiceCollection ConfigurePresentationLayer(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddControllers().AddApplicationPart(typeof(UserController).Assembly);
        
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "API CardioViewer.Backend", Version = "v1" });
        });
        return services;
    }
}