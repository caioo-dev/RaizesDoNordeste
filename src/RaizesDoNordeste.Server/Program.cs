using RaizesDoNordeste.Server.DependencyInjections;
using RaizesDoNordeste.Server.Filters;

namespace RaizesDoNordeste.Server;

internal sealed class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>());
        builder.Services.AddSwagger();
        builder.Services.AddJwtAuthentication(builder.Configuration);
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddExceptionHandlers();

        WebApplication app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseExceptionHandler();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
