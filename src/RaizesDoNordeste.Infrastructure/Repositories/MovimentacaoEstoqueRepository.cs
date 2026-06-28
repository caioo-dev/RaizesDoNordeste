using Microsoft.EntityFrameworkCore;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.Infrastructure.Repositories;

public class MovimentacaoEstoqueRepository(AppDbContext context) : IMovimentacaoEstoqueRepository
{
    public async Task<MovimentacaoEstoque?> ObterPorId(Guid id, CancellationToken cancellationToken)
        => await context.MovimentacoesEstoque
            .Include(m => m.Produto)
            .Include(m => m.Unidade)
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

    public async Task<IEnumerable<MovimentacaoEstoque>> ObterTodos(CancellationToken cancellationToken)
        => await context.MovimentacoesEstoque
            .Include(m => m.Produto)
            .Include(m => m.Unidade)
            .AsNoTracking()
            .OrderByDescending(m => m.DataMovimentacao)
            .ToListAsync(cancellationToken);

    public async Task Criar(MovimentacaoEstoque movimentacao, CancellationToken cancellationToken)
    {
        await context.MovimentacoesEstoque.AddAsync(movimentacao, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
