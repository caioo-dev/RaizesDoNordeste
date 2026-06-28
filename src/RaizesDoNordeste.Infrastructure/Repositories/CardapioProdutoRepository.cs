using Microsoft.EntityFrameworkCore;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.Infrastructure.Repositories;

public class CardapioProdutoRepository(AppDbContext context) : ICardapioProdutoRepository
{
    public async Task<CardapioProduto?> ObterPorId(Guid id, CancellationToken cancellationToken)
        => await context.CardapiosProdutos
            .Include(cp => cp.Produto)
            .FirstOrDefaultAsync(cp => cp.Id == id, cancellationToken);

    public async Task<IEnumerable<CardapioProduto>> ObterPorCardapio(Guid cardapioId, CancellationToken cancellationToken)
        => await context.CardapiosProdutos
            .Include(cp => cp.Produto)
            .Where(cp => cp.CardapioId == cardapioId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task<bool> ExisteVinculo(Guid cardapioId, Guid produtoId, CancellationToken cancellationToken)
        => await context.CardapiosProdutos
            .AnyAsync(cp => cp.CardapioId == cardapioId && cp.ProdutoId == produtoId, cancellationToken);

    public async Task Criar(CardapioProduto cardapioProduto, CancellationToken cancellationToken)
    {
        await context.CardapiosProdutos.AddAsync(cardapioProduto, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Atualizar(CardapioProduto cardapioProduto, CancellationToken cancellationToken)
        => await context.SaveChangesAsync(cancellationToken);

    public async Task Excluir(CardapioProduto cardapioProduto, CancellationToken cancellationToken)
    {
        context.CardapiosProdutos.Remove(cardapioProduto);
        await context.SaveChangesAsync(cancellationToken);
    }
    public async Task<CardapioProduto?> ObterPorCardapioEProduto(Guid cardapioId, Guid produtoId, CancellationToken cancellationToken)
    => await context.CardapiosProdutos
        .FirstOrDefaultAsync(cp => cp.CardapioId == cardapioId && cp.ProdutoId == produtoId, cancellationToken);
}
