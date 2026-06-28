using Microsoft.EntityFrameworkCore;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.Infrastructure.Repositories;

public class PedidoStatusHistoricoRepository(AppDbContext context) : IPedidoStatusHistoricoRepository
{
    public async Task<IEnumerable<PedidoStatusHistorico>> ObterPorPedido(Guid pedidoId, CancellationToken cancellationToken)
        => await context.PedidosStatusHistorico
            .Where(h => h.PedidoId == pedidoId)
            .AsNoTracking()
            .OrderByDescending(h => h.DataAlteracao)
            .ToListAsync(cancellationToken);

    public async Task Registrar(PedidoStatusHistorico historico, CancellationToken cancellationToken)
    {
        await context.PedidosStatusHistorico.AddAsync(historico, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
