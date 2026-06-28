using RaizesDoNordeste.Domain.Enums;

namespace RaizesDoNordeste.Application.DTOs.Responses.Pedido;

public class PedidoObterTodosResponse
{
    public IEnumerable<PedidoObterTodosModel> Pedidos { get; set; } = [];
}   

public class PedidoObterTodosModel
{
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public string NomeCliente { get; set; } = string.Empty;
    public Guid UnidadeId { get; set; }
    public PedidoStatus Status { get; set; }
    public decimal Total { get; set; }
    public DateTime DataInclusao { get; set; }
}
