using RaizesDoNordeste.Application.DTOs.Requests.ReservaEstoque;
using RaizesDoNordeste.Application.DTOs.Responses.ReservaEstoque;

namespace RaizesDoNordeste.Application.Interfaces;

public interface IReservaEstoqueService
{
    Task<ReservaEstoqueObterPorIdResponse?> ObterPorId(Guid id, CancellationToken cancellationToken);
    Task<ReservaEstoqueObterTodosResponse> ObterTodos(CancellationToken cancellationToken);
    Task Criar(Guid usuarioId, CriarReservaEstoqueRequest request, CancellationToken cancellationToken);
    Task Confirmar(Guid id, CancellationToken cancellationToken);
    Task Cancelar(Guid id, CancellationToken cancellationToken);
}
