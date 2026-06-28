using RaizesDoNordeste.Domain.Entities;

namespace RaizesDoNordeste.Domain.Interfaces;

public interface IPedidoRepository
{
    Task<Pedido?> ObterPorId(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Pedido>> ObterTodos(CancellationToken cancellationToken);
    Task Criar(Pedido pedido, CancellationToken cancellationToken);
    Task Atualizar(Pedido pedido, CancellationToken cancellationToken);
    Task<bool> ExistePorId(Guid id, CancellationToken cancellationToken);

}
