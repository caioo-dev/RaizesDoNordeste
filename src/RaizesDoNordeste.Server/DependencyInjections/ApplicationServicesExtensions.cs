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
        services.AddHttpContextAccessor();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPedidoService, PedidoService>();
        services.AddScoped<IClienteService, ClienteService>();
        services.AddScoped<IUnidadeService, UnidadeService>();
        services.AddScoped<IProdutoService, ProdutoService>();
        services.AddScoped<ICardapioService, CardapioService>();
        services.AddScoped<ILogOperacaoService, LogOperacaoService>();
        services.AddScoped<IProdutoUnidadeService, ProdutoUnidadeService>();
        services.AddScoped<IUsuarioUnidadeService, UsuarioUnidadeService>();
        services.AddScoped<IReservaEstoqueService, ReservaEstoqueService>();
        services.AddScoped<ICardapioProdutoService, CardapioProdutoService>();
        services.AddScoped<IPedidoPagamentoService, PedidoPagamentoService>();
        services.AddScoped<IMovimentacaoEstoqueService, MovimentacaoEstoqueService>();
        services.AddScoped<IPedidoStatusHistoricoService, PedidoStatusHistoricoService>();

        return services;
    }
}
