using RaizesDoNordeste.Application.DTOs.Requests.CardapioProduto;
using RaizesDoNordeste.Application.DTOs.Responses.CardapioProduto;
using RaizesDoNordeste.Application.Interfaces;
using RaizesDoNordeste.CrossCutting.Exceptions;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.Application.Services;

public class CardapioProdutoService(
    ICardapioProdutoRepository cardapioProdutoRepository,
    ICardapioRepository cardapioRepository,
    IProdutoRepository produtoRepository) : ICardapioProdutoService
{
    public async Task<CardapioProdutoObterPorIdResponse?> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        CardapioProduto? cardapioProduto = await cardapioProdutoRepository.ObterPorId(id, cancellationToken);
        if (cardapioProduto is null)
        {
            return null;
        }
            

        return MapToObterPorIdResponse(cardapioProduto);
    }

    public async Task<CardapioProdutoObterTodosResponse> ObterPorCardapio(Guid cardapioId, CancellationToken cancellationToken)
    {
        bool cardapioExiste = await cardapioRepository.ExistePorId(cardapioId, cancellationToken);
        if (!cardapioExiste)
        {
            throw new NotFoundException(nameof(Cardapio), cardapioId);
        }

        IEnumerable<CardapioProduto> produtos = await cardapioProdutoRepository.ObterPorCardapio(cardapioId, cancellationToken);

        return new CardapioProdutoObterTodosResponse
        {
            Produtos = produtos.Select(MapToObterTodosModel)
        };
    }

    public async Task Criar(Guid cardapioId, CriarCardapioProdutoRequest request, CancellationToken cancellationToken)
    {
        bool cardapioExiste = await cardapioRepository.ExistePorId(cardapioId, cancellationToken);
        if (!cardapioExiste)
        {
            throw new NotFoundException(nameof(Cardapio), cardapioId);
        }

        bool produtoExiste = await produtoRepository.ExistePorId(request.ProdutoId, cancellationToken);
        if (!produtoExiste)
        {
            throw new NotFoundException(nameof(Produto), request.ProdutoId);
        } 

        bool vinculoExiste = await cardapioProdutoRepository.ExisteVinculo(cardapioId, request.ProdutoId, cancellationToken);
        if (vinculoExiste)
        {
            throw new ConflictException("Produto já está vinculado a este cardápio.");
        }  

        var cardapioProduto = new CardapioProduto(cardapioId, request.ProdutoId, request.PrecoVenda);

        await cardapioProdutoRepository.Criar(cardapioProduto, cancellationToken);
    }

    public async Task Atualizar(Guid id, AtualizarCardapioProdutoRequest request, CancellationToken cancellationToken)
    {
        CardapioProduto cardapioProduto = await cardapioProdutoRepository.ObterPorId(id, cancellationToken)
            ?? throw new NotFoundException(nameof(CardapioProduto), id);

        cardapioProduto.Atualizar(request.PrecoVenda, request.Disponivel);

        await cardapioProdutoRepository.Atualizar(cardapioProduto, cancellationToken);
    }

    public async Task Excluir(Guid id, CancellationToken cancellationToken)
    {
        CardapioProduto cardapioProduto = await cardapioProdutoRepository.ObterPorId(id, cancellationToken)
            ?? throw new NotFoundException(nameof(CardapioProduto), id);

        await cardapioProdutoRepository.Excluir(cardapioProduto, cancellationToken);
    }

    private static CardapioProdutoObterPorIdResponse MapToObterPorIdResponse(CardapioProduto cp) => new()
    {
        Id = cp.Id,
        CardapioId = cp.CardapioId,
        ProdutoId = cp.ProdutoId,
        NomeProduto = cp.Produto.Nome,
        PrecoVenda = cp.PrecoVenda,
        Disponivel = cp.Disponivel
    };

    private static CardapioProdutoObterTodosModel MapToObterTodosModel(CardapioProduto cp) => new()
    {
        Id = cp.Id,
        ProdutoId = cp.ProdutoId,
        NomeProduto = cp.Produto.Nome,
        PrecoVenda = cp.PrecoVenda,
        Disponivel = cp.Disponivel
    };
}
