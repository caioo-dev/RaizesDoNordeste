using Microsoft.EntityFrameworkCore;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.Infrastructure.Repositories;

public class PedidoPagamentoRepository(AppDbContext context) : IPedidoPagamentoRepository
{
    public async Task<PedidoPagamento?> ObterPorId(Guid id, CancellationToken cancellationToken)
        => await context.PedidosPagamentos
            .FirstOrDefaultAsync(pp => pp.Id == id, cancellationToken);

    public async Task<IEnumerable<PedidoPagamento>> ObterPorPedido(Guid pedidoId, CancellationToken cancellationToken)
        => await context.PedidosPagamentos
            .Where(pp => pp.PedidoId == pedidoId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task Criar(PedidoPagamento pagamento, CancellationToken cancellationToken)
    {
        await context.PedidosPagamentos.AddAsync(pagamento, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Atualizar(PedidoPagamento pagamento, CancellationToken cancellationToken)
        => await context.SaveChangesAsync(cancellationToken);
}
