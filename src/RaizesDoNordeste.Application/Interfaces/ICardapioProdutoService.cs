using RaizesDoNordeste.Application.DTOs.Requests.CardapioProduto;
using RaizesDoNordeste.Application.DTOs.Responses.CardapioProduto;

namespace RaizesDoNordeste.Application.Interfaces;

public interface ICardapioProdutoService
{
    Task<CardapioProdutoObterPorIdResponse?> ObterPorId(Guid id, CancellationToken cancellationToken);
    Task<CardapioProdutoObterTodosResponse> ObterPorCardapio(Guid cardapioId, CancellationToken cancellationToken);
    Task Criar(Guid cardapioId, CriarCardapioProdutoRequest request, CancellationToken cancellationToken);
    Task Atualizar(Guid id, AtualizarCardapioProdutoRequest request, CancellationToken cancellationToken);
    Task Excluir(Guid id, CancellationToken cancellationToken);
}
