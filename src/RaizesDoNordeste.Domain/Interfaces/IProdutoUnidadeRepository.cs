using RaizesDoNordeste.Domain.Entities;

namespace RaizesDoNordeste.Domain.Interfaces;

public interface IProdutoUnidadeRepository
{
    Task<ProdutoUnidade?> ObterPorId(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<ProdutoUnidade>> ObterPorUnidade(Guid unidadeId, CancellationToken cancellationToken);
    Task<bool> ExisteVinculo(Guid produtoId, Guid unidadeId, CancellationToken cancellationToken);
    Task Criar(ProdutoUnidade produtoUnidade, CancellationToken cancellationToken);
    Task Atualizar(ProdutoUnidade produtoUnidade, CancellationToken cancellationToken);
    Task Excluir(ProdutoUnidade produtoUnidade, CancellationToken cancellationToken);
}
