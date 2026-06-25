using System.Text;
using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RaizesDoNordeste.Application.DTOs.Requests.Cliente;
using RaizesDoNordeste.Application.Interfaces;
using RaizesDoNordeste.Application.MapsterConfig;
using RaizesDoNordeste.Application.Services;
using RaizesDoNordeste.Domain.Interfaces;
using RaizesDoNordeste.Infrastructure;
using RaizesDoNordeste.Infrastructure.Repositories;
using RaizesDoNordeste.Infrastructure.Services;
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

        IConfigurationSection jwtSection = builder.Configuration.GetSection("Jwt");
        string secretKey = jwtSection["SecretKey"]!;

        builder.Services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSection["Issuer"],
                ValidAudience = jwtSection["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            });

        builder.Services.AddAuthorization();

        builder.Services.AddScoped<IClienteService, ClienteService>();
        builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
        builder.Services.AddScoped<IUnidadeRepository, UnidadeRepository>();
        builder.Services.AddScoped<IUnidadeService, UnidadeService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        builder.Services.AddScoped<IMapper, Mapper>();

        builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
        builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
        builder.Services.AddExceptionHandler<UnauthorizedExceptionHandler>();
        builder.Services.AddExceptionHandler<ConflictExceptionHandler>();
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
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
