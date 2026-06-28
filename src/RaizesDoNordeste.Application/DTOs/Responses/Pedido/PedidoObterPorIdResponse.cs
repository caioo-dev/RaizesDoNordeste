using RaizesDoNordeste.Domain.Enums;

namespace RaizesDoNordeste.Application.DTOs.Responses.Pedido;

public class PedidoObterPorIdResponse
{
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public string NomeCliente { get; set; } = string.Empty;
    public Guid UnidadeId { get; set; }
    public string NomeUnidade { get; set; } = string.Empty;
    public Guid UsuarioId { get; set; }
    public PedidoStatus Status { get; set; }
    public decimal Total { get; set; }
    public EnderecoEntregaResponse EnderecoEntrega { get; set; } = null!;
    public DateTime? DataEntrega { get; set; }
    public DateTime DataInclusao { get; set; }
    public List<PedidoProdutoResponse> Produtos { get; set; } = [];
}
