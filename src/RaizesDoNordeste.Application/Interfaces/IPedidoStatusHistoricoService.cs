using RaizesDoNordeste.Application.DTOs.Responses.PedidoStatusHistorico;
using RaizesDoNordeste.Domain.Enums;

namespace RaizesDoNordeste.Application.Interfaces;

public interface IPedidoStatusHistoricoService
{
    Task<IEnumerable<PedidoStatusHistoricoResponse>> ObterPorPedido(Guid pedidoId, CancellationToken cancellationToken);
    Task Registrar(Guid pedidoId, PedidoStatus statusAnterior, PedidoStatus statusNovo, Guid usuarioId, string? observacao, CancellationToken cancellationToken);
}
