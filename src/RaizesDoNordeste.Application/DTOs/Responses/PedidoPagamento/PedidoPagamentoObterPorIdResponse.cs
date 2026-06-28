using RaizesDoNordeste.Domain.Enums;

namespace RaizesDoNordeste.Application.DTOs.Responses.PedidoPagamento;

public class PedidoPagamentoObterPorIdResponse
{
    public Guid Id { get; set; }
    public Guid PedidoId { get; set; }
    public TipoPagamento TipoPagamento { get; set; }
    public decimal Valor { get; set; }
    public PagamentoStatus Status { get; set; }
    public DateTime? DataPagamento { get; set; }
}
