using RaizesDoNordeste.Application.DTOs.Responses.PedidoStatusHistorico;
using RaizesDoNordeste.Application.Interfaces;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Enums;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.Application.Services;

public class PedidoStatusHistoricoService(
    IPedidoStatusHistoricoRepository pedidoStatusHistoricoRepository) : IPedidoStatusHistoricoService
{
    public async Task<IEnumerable<PedidoStatusHistoricoResponse>> ObterPorPedido(Guid pedidoId, CancellationToken cancellationToken)
    {
        IEnumerable<PedidoStatusHistorico> historicos = await pedidoStatusHistoricoRepository.ObterPorPedido(pedidoId, cancellationToken);

        return historicos.Select(h => new PedidoStatusHistoricoResponse
        {
            Id = h.Id,
            StatusAnterior = h.StatusAnterior,
            StatusNovo = h.StatusNovo,
            UsuarioId = h.UsuarioId,
            Observacao = h.Observacao,
            DataAlteracao = h.DataAlteracao
        });
    }

    public async Task Registrar(Guid pedidoId, PedidoStatus statusAnterior, PedidoStatus statusNovo, Guid usuarioId, string? observacao, CancellationToken cancellationToken)
    {
        var historico = new PedidoStatusHistorico(pedidoId, statusAnterior, statusNovo, usuarioId, observacao);
        await pedidoStatusHistoricoRepository.Registrar(historico, cancellationToken);
    }
}
