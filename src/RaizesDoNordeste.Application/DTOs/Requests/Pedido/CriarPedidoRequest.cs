namespace RaizesDoNordeste.Application.DTOs.Requests.Pedido;

public class CriarPedidoRequest
{
    public Guid ClienteId { get; set; }
    public Guid UnidadeId { get; set; }
    public Guid CardapioId { get; set; }
    public EnderecoEntregaRequest EnderecoEntrega { get; set; } = null!;
    public List<PedidoProdutoRequest> Produtos { get; set; } = [];
}
