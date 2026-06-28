using RaizesDoNordeste.Domain.Entities;

namespace RaizesDoNordeste.Domain.Interfaces;

public interface IPedidoPagamentoRepository
{
    Task<PedidoPagamento?> ObterPorId(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<PedidoPagamento>> ObterPorPedido(Guid pedidoId, CancellationToken cancellationToken);
    Task Criar(PedidoPagamento pagamento, CancellationToken cancellationToken);
    Task Atualizar(PedidoPagamento pagamento, CancellationToken cancellationToken);
}
