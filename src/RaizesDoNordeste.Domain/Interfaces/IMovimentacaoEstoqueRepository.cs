using RaizesDoNordeste.Domain.Entities;

namespace RaizesDoNordeste.Domain.Interfaces;

public interface IMovimentacaoEstoqueRepository
{
    Task<MovimentacaoEstoque?> ObterPorId(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<MovimentacaoEstoque>> ObterTodos(CancellationToken cancellationToken);
    Task Criar(MovimentacaoEstoque movimentacao, CancellationToken cancellationToken);
}
