using RaizesDoNordeste.Domain.Entities;

namespace RaizesDoNordeste.Domain.Interfaces;

public interface IPedidoStatusHistoricoRepository
{
    Task<IEnumerable<PedidoStatusHistorico>> ObterPorPedido(Guid pedidoId, CancellationToken cancellationToken);
    Task Registrar(PedidoStatusHistorico historico, CancellationToken cancellationToken);
}
