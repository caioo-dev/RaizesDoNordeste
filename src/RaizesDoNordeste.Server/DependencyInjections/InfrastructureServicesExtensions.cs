using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using RaizesDoNordeste.Application.Interfaces;
using RaizesDoNordeste.Application.MapsterConfig;
using RaizesDoNordeste.Domain.Interfaces;
using RaizesDoNordeste.Infrastructure;
using RaizesDoNordeste.Infrastructure.Repositories;
using RaizesDoNordeste.Infrastructure.Services;

namespace RaizesDoNordeste.Server.DependencyInjections;

public static class InfrastructureServicesExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IUnidadeRepository, UnidadeRepository>();
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<ICardapioRepository, CardapioRepository>();
        services.AddScoped<ICardapioProdutoRepository, CardapioProdutoRepository>();
        services.AddScoped<IProdutoUnidadeRepository, ProdutoUnidadeRepository>();
        services.AddScoped<IUsuarioUnidadeRepository, UsuarioUnidadeRepository>();

        services.AddScoped<IMapper, Mapper>();

        MapsterConfig.RegisterMappings();

        return services;
    }
}
