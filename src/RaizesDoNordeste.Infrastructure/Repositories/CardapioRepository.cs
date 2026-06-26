using Microsoft.EntityFrameworkCore;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.Infrastructure.Repositories;

public class CardapioRepository(AppDbContext context) : ICardapioRepository
{
    public async Task<Cardapio?> ObterPorId(Guid id, CancellationToken cancellationToken)
        => await context.Cardapios
            .FirstOrDefaultAsync(c => c.Id == id && c.DataExclusao == null, cancellationToken);

    public async Task<IEnumerable<Cardapio>> ObterTodos(CancellationToken cancellationToken)
        => await context.Cardapios
            .Where(c => c.DataExclusao == null)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task Criar(Cardapio cardapio, CancellationToken cancellationToken)
    {
        await context.Cardapios.AddAsync(cardapio, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Salvar(Cardapio cardapio, CancellationToken cancellationToken)
        => await context.SaveChangesAsync(cancellationToken);
}
