using RaizesDoNordeste.Application.DTOs.Requests.MovimentacaoEstoque;
using RaizesDoNordeste.Application.DTOs.Responses.MovimentacaoEstoque;

namespace RaizesDoNordeste.Application.Interfaces;

public interface IMovimentacaoEstoqueService
{
    Task<MovimentacaoEstoqueObterPorIdResponse?> ObterPorId(Guid id, CancellationToken cancellationToken);
    Task<MovimentacaoEstoqueObterTodosResponse> ObterTodos(CancellationToken cancellationToken);
    Task Registrar(Guid usuarioId, CriarMovimentacaoEstoqueRequest request, CancellationToken cancellationToken);
}
