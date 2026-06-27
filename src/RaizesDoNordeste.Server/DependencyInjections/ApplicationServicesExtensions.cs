using FluentValidation;
using RaizesDoNordeste.Application.DTOs.Requests.Cliente;
using RaizesDoNordeste.Application.Interfaces;
using RaizesDoNordeste.Application.Services;

namespace RaizesDoNordeste.Server.DependencyInjections;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CriarClienteRequestValidator>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IClienteService, ClienteService>();
        services.AddScoped<IUnidadeService, UnidadeService>();
        services.AddScoped<IProdutoService, ProdutoService>();
        services.AddScoped<ICardapioService, CardapioService>();
        services.AddScoped<IProdutoUnidadeService, ProdutoUnidadeService>();
        services.AddScoped<IUsuarioUnidadeService, UsuarioUnidadeService>();
        services.AddScoped<IReservaEstoqueService, ReservaEstoqueService>();
        services.AddScoped<ICardapioProdutoService, CardapioProdutoService>();

        return services;
    }
}
