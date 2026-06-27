using RaizesDoNordeste.Application.DTOs.Requests.ProdutoUnidade;
using RaizesDoNordeste.Application.DTOs.Responses.ProdutoUnidade;
using RaizesDoNordeste.Application.Interfaces;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Exceptions;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.Application.Services;

public class ProdutoUnidadeService(
    IProdutoUnidadeRepository produtoUnidadeRepository,
    IProdutoRepository produtoRepository,
    IUnidadeRepository unidadeRepository) : IProdutoUnidadeService
{
    public async Task<ProdutoUnidadeObterPorIdResponse?> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        ProdutoUnidade? produtoUnidade = await produtoUnidadeRepository.ObterPorId(id, cancellationToken);
        if (produtoUnidade is null)
        {
            return null;
        }       

        return MapToObterPorIdResponse(produtoUnidade);
    }

    public async Task<ProdutoUnidadeObterTodosResponse> ObterPorUnidade(Guid unidadeId, CancellationToken cancellationToken)
    {
        bool unidadeExiste = await unidadeRepository.ExistePorId(unidadeId, cancellationToken);
        if (!unidadeExiste)
        {
            throw new NotFoundException(nameof(Unidade), unidadeId);
        }    

        IEnumerable<ProdutoUnidade> produtos = await produtoUnidadeRepository.ObterPorUnidade(unidadeId, cancellationToken);

        return new ProdutoUnidadeObterTodosResponse
        {
            Produtos = produtos.Select(MapToObterTodosModel)
        };
    }

    public async Task Criar(Guid unidadeId, ProdutoUnidadeRequest request, CancellationToken cancellationToken)
    {
        bool unidadeExiste = await unidadeRepository.ExistePorId(unidadeId, cancellationToken);
        if (!unidadeExiste)
        {
            throw new NotFoundException(nameof(Unidade), unidadeId);
        }
            

        bool produtoExiste = await produtoRepository.ExistePorId(request.ProdutoId, cancellationToken);
        if (!produtoExiste)
        {
            throw new NotFoundException(nameof(Produto), request.ProdutoId);
        }
            

        bool vinculoExiste = await produtoUnidadeRepository.ExisteVinculo(request.ProdutoId, unidadeId, cancellationToken);
        if (vinculoExiste)
        {
            throw new ConflictException("Produto já está vinculado a esta unidade.");
        }

        var produtoUnidade = new ProdutoUnidade(request.ProdutoId, unidadeId, request.EstoqueDisponivel);

        await produtoUnidadeRepository.Criar(produtoUnidade, cancellationToken);
    }

    public async Task Atualizar(Guid id, ProdutoUnidadeRequest request, CancellationToken cancellationToken)
    {
        ProdutoUnidade produtoUnidade = await produtoUnidadeRepository.ObterPorId(id, cancellationToken)
            ?? throw new NotFoundException(nameof(ProdutoUnidade), id);

        produtoUnidade.Atualizar(request.EstoqueDisponivel, request.EstoqueReservado, request.Ativo);

        await produtoUnidadeRepository.Atualizar(produtoUnidade, cancellationToken);
    }

    public async Task Excluir(Guid id, CancellationToken cancellationToken)
    {
        ProdutoUnidade produtoUnidade = await produtoUnidadeRepository.ObterPorId(id, cancellationToken)
            ?? throw new NotFoundException(nameof(ProdutoUnidade), id);

        produtoUnidade.Excluir();

        await produtoUnidadeRepository.Excluir(produtoUnidade, cancellationToken);
    }

    private static ProdutoUnidadeObterPorIdResponse MapToObterPorIdResponse(ProdutoUnidade pu) => new()
    {
        Id = pu.Id,
        ProdutoId = pu.ProdutoID,
        NomeProduto = pu.Produto.Nome,
        UnidadeId = pu.UnidadeID,
        EstoqueDisponivel = pu.EstoqueDisponivel,
        EstoqueReservado = pu.EstoqueReservado,
        Ativo = pu.Ativo,
        DataInclusao = pu.DataInclusao
    };

    private static ProdutoUnidadeObterTodosModel MapToObterTodosModel(ProdutoUnidade pu) => new()
    {
        Id = pu.Id,
        ProdutoId = pu.ProdutoID,
        NomeProduto = pu.Produto.Nome,
        EstoqueDisponivel = pu.EstoqueDisponivel,
        EstoqueReservado = pu.EstoqueReservado,
        Ativo = pu.Ativo
    };
}
