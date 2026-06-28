using RaizesDoNordeste.Application.DTOs.Requests.MovimentacaoEstoque;
using RaizesDoNordeste.Application.DTOs.Responses.MovimentacaoEstoque;
using RaizesDoNordeste.Application.Interfaces;
using RaizesDoNordeste.CrossCutting.Exceptions;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.Application.Services;

public class MovimentacaoEstoqueService(
    IMovimentacaoEstoqueRepository movimentacaoEstoqueRepository,
    IProdutoUnidadeRepository produtoUnidadeRepository,
    IProdutoRepository produtoRepository,
    IUnidadeRepository unidadeRepository) : IMovimentacaoEstoqueService
{
    public async Task<MovimentacaoEstoqueObterPorIdResponse?> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        MovimentacaoEstoque? movimentacao = await movimentacaoEstoqueRepository.ObterPorId(id, cancellationToken);
        if (movimentacao is null)
        {
            return null;
        }
           
        return MapToObterPorIdResponse(movimentacao);
    }

    public async Task<MovimentacaoEstoqueObterTodosResponse> ObterTodos(CancellationToken cancellationToken)
    {
        IEnumerable<MovimentacaoEstoque> movimentacoes = await movimentacaoEstoqueRepository.ObterTodos(cancellationToken);

        return new MovimentacaoEstoqueObterTodosResponse
        {
            Movimentacoes = movimentacoes.Select(MapToObterTodosModel)
        };
    }

    public async Task Registrar(Guid usuarioId, CriarMovimentacaoEstoqueRequest request, CancellationToken cancellationToken)
    {
        bool produtoExiste = await produtoRepository.ExistePorId(request.ProdutoId, cancellationToken);
        if (!produtoExiste)
        {
            throw new NotFoundException(nameof(Produto), request.ProdutoId);
        }

        bool unidadeExiste = await unidadeRepository.ExistePorId(request.UnidadeId, cancellationToken);
        if (!unidadeExiste)
        {
            throw new NotFoundException(nameof(Unidade), request.UnidadeId);
        }       

        ProdutoUnidade produtoUnidade = await produtoUnidadeRepository.ObterPorProdutoEUnidade(request.ProdutoId, request.UnidadeId, cancellationToken)
            ?? throw new NotFoundException("Produto não está vinculado a esta unidade.");

        decimal saldoAnterior = produtoUnidade.EstoqueDisponivel;
        decimal saldoPosterior = saldoAnterior + request.Quantidade;

        if (saldoPosterior < 0)
        {
            throw new ConflictException($"Estoque insuficiente. Saldo atual: {saldoAnterior}, quantidade solicitada: {request.Quantidade}.");
        }

        produtoUnidade.AtualizarEstoque(saldoPosterior);
        await produtoUnidadeRepository.Atualizar(produtoUnidade, cancellationToken);

        var movimentacao = new MovimentacaoEstoque(
            request.ProdutoId,
            request.UnidadeId,
            usuarioId,
            request.DocumentoOrigemId,
            request.TipoMovimentacaoOrigem,
            request.Quantidade,
            request.Observacao,
            saldoAnterior,
            saldoPosterior);

        await movimentacaoEstoqueRepository.Criar(movimentacao, cancellationToken);
    }

    private static MovimentacaoEstoqueObterPorIdResponse MapToObterPorIdResponse(MovimentacaoEstoque m) => new()
    {
        Id = m.Id,
        ProdutoId = m.ProdutoId,
        NomeProduto = m.Produto.Nome,
        UnidadeId = m.UnidadeId,
        NomeFantasiaUnidade = m.Unidade.NomeFantasia,
        UsuarioId = m.UsuarioId,
        DocumentoOrigemId = m.DocumentoOrigemId,
        TipoMovimentacaoOrigem = m.TipoMovimentacaoOrigem,
        Quantidade = m.Quantidade,
        Observacao = m.Observacao,
        DataMovimentacao = m.DataMovimentacao,
        SaldoAnterior = m.SaldoAnterior,
        SaldoPosterior = m.SaldoPosterior
    };

    private static MovimentacaoEstoqueObterTodosModel MapToObterTodosModel(MovimentacaoEstoque m) => new()
    {
        Id = m.Id,
        ProdutoId = m.ProdutoId,
        NomeProduto = m.Produto.Nome,
        UnidadeId = m.UnidadeId,
        NomeFantasiaUnidade = m.Unidade.NomeFantasia,
        TipoMovimentacaoOrigem = m.TipoMovimentacaoOrigem,
        Quantidade = m.Quantidade,
        DataMovimentacao = m.DataMovimentacao,
        SaldoPosterior = m.SaldoPosterior
    };
}
