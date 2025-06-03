using Microsoft.AspNetCore.Builder;
using Serilog;
using StudyTracker.Application.Extensions;
using StudyTracker.Infrastructure.Postgres.Extensions;
using StudyTracker.Presentation.Extensions;

namespace StudyTracker.Backend;

public class Startup(IConfiguration configuration)
{
    private readonly IConfiguration _configuration = configuration;

    public void ConfigureServices(IServiceCollection service)
    {
        service.AddCors();
        service.AddSerilog();
        service.AddRouting();
        
        service.ConfigurePostgresInfrastructure();
        service.ConfigureApplicationExtensions(_configuration);
        service.ConfigurePresentationLayer();
    }
    
    public void Configure(IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseRouting();
        
        applicationBuilder.UseSwagger();
        applicationBuilder.UseSwaggerUI();
        applicationBuilder.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader());
        applicationBuilder.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        
        applicationBuilder.UseAuthorization();
    }
}