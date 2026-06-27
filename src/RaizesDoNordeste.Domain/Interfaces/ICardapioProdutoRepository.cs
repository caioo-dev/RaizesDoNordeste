using RaizesDoNordeste.Domain.Entities;

namespace RaizesDoNordeste.Domain.Interfaces;

public interface ICardapioProdutoRepository
{
    Task<CardapioProduto?> ObterPorId(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<CardapioProduto>> ObterPorCardapio(Guid cardapioId, CancellationToken cancellationToken);
    Task<bool> ExisteVinculo(Guid cardapioId, Guid produtoId, CancellationToken cancellationToken);
    Task Criar(CardapioProduto cardapioProduto, CancellationToken cancellationToken);
    Task Atualizar(CardapioProduto cardapioProduto, CancellationToken cancellationToken);
    Task Excluir(CardapioProduto cardapioProduto, CancellationToken cancellationToken);
}
