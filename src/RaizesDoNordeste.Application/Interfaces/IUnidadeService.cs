using RaizesDoNordeste.Application.DTOs.Requests.Unidade;
using RaizesDoNordeste.Application.DTOs.Responses.Unidade;

namespace RaizesDoNordeste.Application.Interfaces;

public interface IUnidadeService
{
    Task<UnidadeObterPorIdResponse?> ObterPorId(Guid id, CancellationToken cancellationToken);
    Task<UnidadeObterTodosResponse> ObterTodos(CancellationToken cancellationToken);
    Task Criar(CriarUnidadeRequest request, CancellationToken cancellationToken);
    Task Atualizar(Guid id, AtualizarUnidadeRequest request, CancellationToken cancellationToken);
    Task Excluir(Guid id, CancellationToken cancellationToken);
}
