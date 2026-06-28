using RaizesDoNordeste.Application.DTOs.Requests.PedidoPagamento;
using RaizesDoNordeste.Application.DTOs.Responses.PedidoPagamento;

namespace RaizesDoNordeste.Application.Interfaces;

public interface IPedidoPagamentoService
{
    Task<PedidoPagamentoObterPorIdResponse?> ObterPorId(Guid id, CancellationToken cancellationToken);
    Task<PedidoPagamentoObterTodosResponse> ObterPorPedido(Guid pedidoId, CancellationToken cancellationToken);
    Task Criar(Guid pedidoId, CriarPedidoPagamentoRequest request, CancellationToken cancellationToken);
    Task Confirmar(Guid id, CancellationToken cancellationToken);
    Task Estornar(Guid id, CancellationToken cancellationToken);
    Task Cancelar(Guid id, CancellationToken cancellationToken);
}
