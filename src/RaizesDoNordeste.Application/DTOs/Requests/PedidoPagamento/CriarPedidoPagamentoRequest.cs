using RaizesDoNordeste.Domain.Enums;

namespace RaizesDoNordeste.Application.DTOs.Requests.PedidoPagamento;

public class CriarPedidoPagamentoRequest
{
    public TipoPagamento TipoPagamento { get; set; }
    public decimal Valor { get; set; }
}
