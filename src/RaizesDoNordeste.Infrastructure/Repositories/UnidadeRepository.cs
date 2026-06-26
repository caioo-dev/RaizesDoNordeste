using Microsoft.EntityFrameworkCore;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.Infrastructure.Repositories;

public class UnidadeRepository(AppDbContext context) : IUnidadeRepository
{
    public async Task<Unidade?> ObterPorId(Guid id, CancellationToken cancellationToken)
        => await context.Unidades
            .FirstOrDefaultAsync(u => u.Id == id && u.DataExclusao == null, cancellationToken);

    public async Task<IEnumerable<Unidade>> ObterTodos(CancellationToken cancellationToken)
        => await context.Unidades
            .Where(u => u.DataExclusao == null)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task Criar(Unidade unidade, CancellationToken cancellationToken)
    {
        await context.Unidades.AddAsync(unidade, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Atualizar(Unidade unidade, CancellationToken cancellationToken)
        => await context.SaveChangesAsync(cancellationToken);

    public async Task<bool> ExistePorId(Guid id, CancellationToken cancellationToken)
    => await context.Unidades
        .AnyAsync(u => u.Id == id && u.DataExclusao == null, cancellationToken);

}
