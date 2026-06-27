using RaizesDoNordeste.Application.DTOs.Requests.ReservaEstoque;
using RaizesDoNordeste.Application.DTOs.Responses.ReservaEstoque;
using RaizesDoNordeste.Application.Interfaces;
using RaizesDoNordeste.CrossCutting.Exceptions;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Enums;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.Application.Services;

public class ReservaEstoqueService(
    IReservaEstoqueRepository reservaEstoqueRepository,
    IProdutoRepository produtoRepository,
    IUnidadeRepository unidadeRepository) : IReservaEstoqueService
{
    public async Task<ReservaEstoqueObterPorIdResponse?> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        ReservaEstoque? reserva = await reservaEstoqueRepository.ObterPorId(id, cancellationToken);
        if (reserva is null)
        {
            return null;
        }

        return MapToObterPorIdResponse(reserva);
    }

    public async Task<ReservaEstoqueObterTodosResponse> ObterTodos(CancellationToken cancellationToken)
    {
        IEnumerable<ReservaEstoque> reservas = await reservaEstoqueRepository.ObterTodos(cancellationToken);

        return new ReservaEstoqueObterTodosResponse
        {
            Reservas = reservas.Select(MapToObterTodosModel)
        };
    }

    public async Task Criar(Guid usuarioId, CriarReservaEstoqueRequest request, CancellationToken cancellationToken)
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
          
        var reserva = new ReservaEstoque(
            request.ProdutoId,
            request.UnidadeId,
            usuarioId,
            request.DocumentoOrigemId,
            request.TipoMovimentacaoOrigem,
            request.Quantidade,
            request.DataExpiracao);

        await reservaEstoqueRepository.Criar(reserva, cancellationToken);
    }

    public async Task Confirmar(Guid id, CancellationToken cancellationToken)
    {
        ReservaEstoque reserva = await reservaEstoqueRepository.ObterPorId(id, cancellationToken)
            ?? throw new NotFoundException(nameof(ReservaEstoque), id);

        if (reserva.Status != StatusReservaEstoque.Pendente)
        {
            throw new ConflictException($"Reserva não pode ser confirmada pois está com status '{reserva.Status}'.");
        }
            

        reserva.Confirmar();
        await reservaEstoqueRepository.Atualizar(reserva, cancellationToken);
    }

    public async Task Cancelar(Guid id, CancellationToken cancellationToken)
    {
        ReservaEstoque reserva = await reservaEstoqueRepository.ObterPorId(id, cancellationToken)
            ?? throw new NotFoundException(nameof(ReservaEstoque), id);

        if (reserva.Status == StatusReservaEstoque.Cancelada)
        {
            throw new ConflictException("Reserva já está cancelada.");
        }

        reserva.Cancelar();
        await reservaEstoqueRepository.Atualizar(reserva, cancellationToken);
    }

    private static ReservaEstoqueObterPorIdResponse MapToObterPorIdResponse(ReservaEstoque r) => new()
    {
        Id = r.Id,
        ProdutoId = r.ProdutoId,
        NomeProduto = r.Produto.Nome,
        UnidadeId = r.UnidadeId,
        NomeFantasiaUnidade = r.Unidade.NomeFantasia,
        UsuarioId = r.UsuarioId,
        DocumentoOrigemId = r.DocumentoOrigemId,
        TipoMovimentacaoOrigem = r.TipoMovimentacaoOrigem,
        Quantidade = r.Quantidade,
        Status = r.Status,
        DataCriacao = r.DataCriacao,
        DataExpiracao = r.DataExpiracao
    };

    private static ReservaEstoqueObterTodosModel MapToObterTodosModel(ReservaEstoque r) => new()
    {
        Id = r.Id,
        ProdutoId = r.ProdutoId,
        NomeProduto = r.Produto.Nome,
        UnidadeId = r.UnidadeId,
        NomeFantasiaUnidade = r.Unidade.NomeFantasia,
        Quantidade = r.Quantidade,
        Status = r.Status,
        DataExpiracao = r.DataExpiracao
    };
}
