using Microsoft.AspNetCore.Builder;
using Serilog;

namespace StudyTracker.Backend;

public class Startup(IConfiguration configuration)
{
    private readonly IConfiguration _configuration = configuration;

    public void ConfigureServices(IServiceCollection service)
    {
        service.AddCors();
        service.AddSerilog();
        service.AddRouting();
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