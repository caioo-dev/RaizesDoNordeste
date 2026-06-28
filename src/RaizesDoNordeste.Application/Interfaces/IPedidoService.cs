using RaizesDoNordeste.Application.DTOs.Requests.Pedido;
using RaizesDoNordeste.Application.DTOs.Responses.Pedido;

namespace RaizesDoNordeste.Application.Interfaces;

public interface IPedidoService
{
    Task<PedidoObterPorIdResponse?> ObterPorId(Guid id, CancellationToken cancellationToken);
    Task<PedidoObterTodosResponse> ObterTodos(CancellationToken cancellationToken);
    Task Criar(Guid usuarioId, CriarPedidoRequest request, CancellationToken cancellationToken);
    Task Confirmar(Guid id, Guid usuarioId, CancellationToken cancellationToken);
    Task SairParaEntrega(Guid id, Guid usuarioId, CancellationToken cancellationToken);
    Task Entregar(Guid id, Guid usuarioId, CancellationToken cancellationToken);
    Task Cancelar(Guid id, Guid usuarioId, CancellationToken cancellationToken);
}
