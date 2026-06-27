using RaizesDoNordeste.Application.DTOs.Requests.ProdutoUnidade;
using RaizesDoNordeste.Application.DTOs.Responses.ProdutoUnidade;

namespace RaizesDoNordeste.Application.Interfaces;

public interface IProdutoUnidadeService
{
    Task<ProdutoUnidadeObterPorIdResponse?> ObterPorId(Guid id, CancellationToken cancellationToken);
    Task<ProdutoUnidadeObterTodosResponse> ObterPorUnidade(Guid unidadeId, CancellationToken cancellationToken);
    Task Criar(Guid unidadeId, ProdutoUnidadeRequest request, CancellationToken cancellationToken);
    Task Atualizar(Guid id, ProdutoUnidadeRequest request, CancellationToken cancellationToken);
    Task Excluir(Guid id, CancellationToken cancellationToken);
}
