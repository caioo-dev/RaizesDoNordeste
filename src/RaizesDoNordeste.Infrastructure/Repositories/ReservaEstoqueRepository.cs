using Microsoft.EntityFrameworkCore;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.Infrastructure.Repositories;

public class ReservaEstoqueRepository(AppDbContext context) : IReservaEstoqueRepository
{
    public async Task<ReservaEstoque?> ObterPorId(Guid id, CancellationToken cancellationToken)
        => await context.ReservasEstoque
            .Include(r => r.Produto)
            .Include(r => r.Unidade)
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

    public async Task<IEnumerable<ReservaEstoque>> ObterTodos(CancellationToken cancellationToken)
        => await context.ReservasEstoque
            .Include(r => r.Produto)
            .Include(r => r.Unidade)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task Criar(ReservaEstoque reservaEstoque, CancellationToken cancellationToken)
    {
        await context.ReservasEstoque.AddAsync(reservaEstoque, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Atualizar(ReservaEstoque reservaEstoque, CancellationToken cancellationToken)
        => await context.SaveChangesAsync(cancellationToken);
}
