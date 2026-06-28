using Microsoft.EntityFrameworkCore;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.Infrastructure.Repositories;

public class PedidoRepository(AppDbContext context) : IPedidoRepository
{
    public async Task<Pedido?> ObterPorId(Guid id, CancellationToken cancellationToken)
        => await context.Pedidos
            .Include(p => p.Cliente)
            .Include(p => p.Unidade)
            .Include(p => p.PedidosProdutos)
                .ThenInclude(pp => pp.Produto)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public async Task<IEnumerable<Pedido>> ObterTodos(CancellationToken cancellationToken)
        => await context.Pedidos
            .Include(p => p.Cliente)
            .Include(p => p.Unidade)
            .AsNoTracking()
            .OrderByDescending(p => p.DataInclusao)
            .ToListAsync(cancellationToken);

    public async Task Criar(Pedido pedido, CancellationToken cancellationToken)
    {
        await context.Pedidos.AddAsync(pedido, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Atualizar(Pedido pedido, CancellationToken cancellationToken)
        => await context.SaveChangesAsync(cancellationToken);

    public async Task<bool> ExistePorId(Guid id, CancellationToken cancellationToken)
    => await context.Pedidos
        .AnyAsync(p => p.Id == id, cancellationToken);
}
