using RaizesDoNordeste.Application.DTOs.Requests.Produto;
using RaizesDoNordeste.Application.DTOs.Responses.Produto;

namespace RaizesDoNordeste.Application.Interfaces;

public interface IProdutoService
{
    Task<ProdutoObterPorIdResponse?> ObterPorId(Guid id, CancellationToken cancellationToken);
    Task<ProdutoObterTodosResponse> ObterTodos(CancellationToken cancellationToken);
    Task Criar(ProdutoRequest request, CancellationToken cancellationToken);
    Task Atualizar(Guid id, ProdutoRequest request, CancellationToken cancellationToken);
    Task Excluir(Guid id, CancellationToken cancellationToken);
}
