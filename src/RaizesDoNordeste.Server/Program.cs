using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using RaizesDoNordeste.Application.DTOs.Requests.Cliente;
using RaizesDoNordeste.Application.Interfaces;
using RaizesDoNordeste.Application.MapsterConfig;
using RaizesDoNordeste.Application.Services;
using RaizesDoNordeste.Domain.Interfaces;
using RaizesDoNordeste.Infrastructure;
using RaizesDoNordeste.Infrastructure.Repositories;
using RaizesDoNordeste.Server.ExceptionHandlers;
using RaizesDoNordeste.Server.Filters;

namespace RaizesDoNordeste.Server;

internal sealed class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>());
        builder.Services.AddValidatorsFromAssemblyContaining<CriarClienteRequestValidator>();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddValidatorsFromAssemblyContaining<CriarClienteRequestValidator>();

        builder.Services.AddScoped<IClienteService, ClienteService>();
        builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
        builder.Services.AddScoped<IUnidadeRepository, UnidadeRepository>();
        builder.Services.AddScoped<IUnidadeService, UnidadeService>();
        builder.Services.AddScoped<IMapper, Mapper>();

        builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
        builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

        builder.Services.AddProblemDetails();

        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        MapsterConfig.RegisterMappings();

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        { 
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseExceptionHandler();

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
