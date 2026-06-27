using Microsoft.EntityFrameworkCore;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.Infrastructure.Repositories;

public class ProdutoRepository(AppDbContext context) : IProdutoRepository
{
    public async Task<Produto?> ObterPorId(Guid id, CancellationToken cancellationToken)
        => await context.Produtos
            .FirstOrDefaultAsync(p => p.Id == id && p.DataExclusao == null, cancellationToken);

    public async Task<IEnumerable<Produto>> ObterTodos(CancellationToken cancellationToken)
        => await context.Produtos
            .Where(p => p.DataExclusao == null)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task<bool> ExistePorId(Guid id, CancellationToken cancellationToken)
        => await context.Produtos
            .AnyAsync(p => p.Id == id && p.DataExclusao == null, cancellationToken);

    public async Task Criar(Produto produto, CancellationToken cancellationToken)
    {
        await context.Produtos.AddAsync(produto, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Atualizar(Produto produto, CancellationToken cancellationToken)
        => await context.SaveChangesAsync(cancellationToken);

    public async Task Excluir(Produto produto, CancellationToken cancellationToken)
        => await context.SaveChangesAsync(cancellationToken);
}
