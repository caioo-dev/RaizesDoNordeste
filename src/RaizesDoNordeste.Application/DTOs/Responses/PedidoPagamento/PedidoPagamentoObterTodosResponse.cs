using RaizesDoNordeste.Domain.Enums;

namespace RaizesDoNordeste.Application.DTOs.Responses.PedidoPagamento;

public class PedidoPagamentoObterTodosResponse
{
    public ICollection<PedidoPagamentoObterTodosModel> Pagamentos { get; } = [];
    public decimal TotalPago { get; set; }
}
public class PedidoPagamentoObterTodosModel
{
    public Guid Id { get; set; }
    public TipoPagamento TipoPagamento { get; set; }
    public decimal Valor { get; set; }
    public PagamentoStatus Status { get; set; }
    public DateTime? DataPagamento { get; set; }
}
