using RaizesDoNordeste.Domain.Enums;

namespace RaizesDoNordeste.Domain.Entities;

public class PedidoPagamento
{
    public Guid Id { get; set; }

    public Guid PedidoId { get; set; }
    public Pedido Pedido { get; set; } = null!;

    public TipoPagamento TipoPagamento { get; set; }

    public decimal Valor { get; set; }

    public PagamentoStatus Status { get; set; }

    public DateTime? DataPagamento { get; set; }
}
