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
        services.AddScoped<IPedidoRepository, PedidoRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IUnidadeRepository, UnidadeRepository>();
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<ICardapioRepository, CardapioRepository>();
        services.AddScoped<ILogOperacaoRepository, LogOperacaoRepository>();
        services.AddScoped<IReservaEstoqueRepository, ReservaEstoqueRepository>();
        services.AddScoped<IProdutoUnidadeRepository, ProdutoUnidadeRepository>();
        services.AddScoped<IUsuarioUnidadeRepository, UsuarioUnidadeRepository>();
        services.AddScoped<IPedidoPagamentoRepository, PedidoPagamentoRepository>();
        services.AddScoped<ICardapioProdutoRepository, CardapioProdutoRepository>();
        services.AddScoped<IMovimentacaoPontoRepository, MovimentacaoPontoRepository>();
        services.AddScoped<IClienteFidelizacaoRepository, ClienteFidelizacaoRepository>();
        services.AddScoped<IMovimentacaoEstoqueRepository, MovimentacaoEstoqueRepository>();
        services.AddScoped<IPedidoStatusHistoricoRepository, PedidoStatusHistoricoRepository>();

        services.AddScoped<IMapper, Mapper>();

        MapsterConfig.RegisterMappings();

        return services;
    }
}
