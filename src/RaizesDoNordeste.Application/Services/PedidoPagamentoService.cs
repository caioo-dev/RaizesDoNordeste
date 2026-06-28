using RaizesDoNordeste.Application.DTOs.Requests.PedidoPagamento;
using RaizesDoNordeste.Application.DTOs.Responses.PedidoPagamento;
using RaizesDoNordeste.Application.Interfaces;
using RaizesDoNordeste.CrossCutting.Exceptions;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Enums;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.Application.Services;

public class PedidoPagamentoService(
    IPedidoPagamentoRepository pedidoPagamentoRepository,
    IPedidoRepository pedidoRepository) : IPedidoPagamentoService
{
    public async Task<PedidoPagamentoObterPorIdResponse?> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        PedidoPagamento? pagamento = await pedidoPagamentoRepository.ObterPorId(id, cancellationToken);
        if (pagamento is null)
        {
            return null;
        }

        return MapToObterPorIdResponse(pagamento);
    }

    public async Task<PedidoPagamentoObterTodosResponse> ObterPorPedido(Guid pedidoId, CancellationToken cancellationToken)
    {
        bool pedidoExiste = await pedidoRepository.ExistePorId(pedidoId, cancellationToken);
        if (!pedidoExiste)
        {
            throw new NotFoundException(nameof(Pedido), pedidoId);
        }

        IEnumerable<PedidoPagamento> pagamentos = await pedidoPagamentoRepository.ObterPorPedido(pedidoId, cancellationToken);

        var response = new PedidoPagamentoObterTodosResponse
        {
            TotalPago = pagamentos
                .Where(p => p.Status == PagamentoStatus.Pago)
                .Sum(p => p.Valor)
        };

        foreach (PedidoPagamento pagamento in pagamentos)
        {
            response.Pagamentos.Add(MapToObterTodosModel(pagamento));
        }

        return response;
    }

    public async Task Criar(Guid pedidoId, CriarPedidoPagamentoRequest request, CancellationToken cancellationToken)
    {
        Pedido pedido = await pedidoRepository.ObterPorId(pedidoId, cancellationToken)
            ?? throw new NotFoundException(nameof(Pedido), pedidoId);

        if (pedido.Status == PedidoStatus.Cancelado)
        {
            throw new ConflictException("Não é possível registrar pagamento para um pedido cancelado.");
        }

        if (pedido.Status == PedidoStatus.Entregue)
        {
            throw new ConflictException("Não é possível registrar pagamento para um pedido já entregue.");
        }

        IEnumerable<PedidoPagamento> pagamentosExistentes = await pedidoPagamentoRepository.ObterPorPedido(pedidoId, cancellationToken);

        decimal totalJaPago = pagamentosExistentes
            .Where(p => p.Status == PagamentoStatus.Pago)
            .Sum(p => p.Valor);

        if (totalJaPago + request.Valor > pedido.Total)
        {
            throw new ConflictException($"O valor do pagamento ultrapassa o total do pedido. Total: {pedido.Total}, já pago: {totalJaPago}, novo pagamento: {request.Valor}.");
        }

        var pagamento = new PedidoPagamento(pedidoId, request.TipoPagamento, request.Valor);

        await pedidoPagamentoRepository.Criar(pagamento, cancellationToken);
    }

    public async Task Confirmar(Guid id, CancellationToken cancellationToken)
    {
        PedidoPagamento pagamento = await pedidoPagamentoRepository.ObterPorId(id, cancellationToken)
            ?? throw new NotFoundException(nameof(PedidoPagamento), id);

        pagamento.Confirmar();
        await pedidoPagamentoRepository.Atualizar(pagamento, cancellationToken);
    }

    public async Task Estornar(Guid id, CancellationToken cancellationToken)
    {
        PedidoPagamento pagamento = await pedidoPagamentoRepository.ObterPorId(id, cancellationToken)
            ?? throw new NotFoundException(nameof(PedidoPagamento), id);

        pagamento.Estornar();
        await pedidoPagamentoRepository.Atualizar(pagamento, cancellationToken);
    }

    public async Task Cancelar(Guid id, CancellationToken cancellationToken)
    {
        PedidoPagamento pagamento = await pedidoPagamentoRepository.ObterPorId(id, cancellationToken)
            ?? throw new NotFoundException(nameof(PedidoPagamento), id);

        pagamento.Cancelar();
        await pedidoPagamentoRepository.Atualizar(pagamento, cancellationToken);
    }

    private static PedidoPagamentoObterPorIdResponse MapToObterPorIdResponse(PedidoPagamento p) => new()
    {
        Id = p.Id,
        PedidoId = p.PedidoId,
        TipoPagamento = p.TipoPagamento,
        Valor = p.Valor,
        Status = p.Status,
        DataPagamento = p.DataPagamento
    };

    private static PedidoPagamentoObterTodosModel MapToObterTodosModel(PedidoPagamento p) => new()
    {
        Id = p.Id,
        TipoPagamento = p.TipoPagamento,
        Valor = p.Valor,
        Status = p.Status,
        DataPagamento = p.DataPagamento
    };
}
