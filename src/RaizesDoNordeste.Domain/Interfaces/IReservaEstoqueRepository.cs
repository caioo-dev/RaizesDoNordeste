using RaizesDoNordeste.Domain.Entities;

namespace RaizesDoNordeste.Domain.Interfaces;

public interface IReservaEstoqueRepository
{
    Task<ReservaEstoque?> ObterPorId(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<ReservaEstoque>> ObterTodos(CancellationToken cancellationToken);
    Task Criar(ReservaEstoque reservaEstoque, CancellationToken cancellationToken);
    Task Atualizar(ReservaEstoque reservaEstoque, CancellationToken cancellationToken);
}
