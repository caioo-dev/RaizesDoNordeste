using RaizesDoNordeste.Application.DTOs.Requests.Cardapio;
using RaizesDoNordeste.Application.DTOs.Responses.Cardapio;

namespace RaizesDoNordeste.Application.Interfaces;

public interface ICardapioService
{
    Task<CardapioObterPorIdResponse?> ObterPorId(Guid id, CancellationToken cancellationToken);
    Task<CardapioObterTodosResponse> ObterTodos(CancellationToken cancellationToken);
    Task Criar(CardapioRequest request, CancellationToken cancellationToken);
    Task Atualizar(Guid id, CardapioRequest request, CancellationToken cancellationToken);
    Task Excluir(Guid id, CancellationToken cancellationToken);
}
