using Microsoft.EntityFrameworkCore;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.Infrastructure.Repositories;

public class ProdutoUnidadeRepository(AppDbContext context) : IProdutoUnidadeRepository
{
    public async Task<ProdutoUnidade?> ObterPorId(Guid id, CancellationToken cancellationToken)
        => await context.ProdutosUnidades
            .Include(pu => pu.Produto)
            .FirstOrDefaultAsync(pu => pu.Id == id && pu.DataExclusao == null, cancellationToken);

    public async Task<IEnumerable<ProdutoUnidade>> ObterPorUnidade(Guid unidadeId, CancellationToken cancellationToken)
        => await context.ProdutosUnidades
            .Include(pu => pu.Produto)
            .Where(pu => pu.UnidadeID == unidadeId && pu.DataExclusao == null)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task<bool> ExisteVinculo(Guid produtoId, Guid unidadeId, CancellationToken cancellationToken)
        => await context.ProdutosUnidades
            .AnyAsync(pu => pu.ProdutoID == produtoId && pu.UnidadeID == unidadeId && pu.DataExclusao == null, cancellationToken);

    public async Task Criar(ProdutoUnidade produtoUnidade, CancellationToken cancellationToken)
    {
        await context.ProdutosUnidades.AddAsync(produtoUnidade, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Atualizar(ProdutoUnidade produtoUnidade, CancellationToken cancellationToken)
        => await context.SaveChangesAsync(cancellationToken);

    public async Task Excluir(ProdutoUnidade produtoUnidade, CancellationToken cancellationToken)
        => await context.SaveChangesAsync(cancellationToken);

    public async Task<ProdutoUnidade?> ObterPorProdutoEUnidade(Guid produtoId, Guid unidadeId, CancellationToken cancellationToken)
    => await context.ProdutosUnidades
        .FirstOrDefaultAsync(pu => pu.ProdutoID == produtoId && pu.UnidadeID == unidadeId && pu.DataExclusao == null, cancellationToken);
}
