namespace RaizesDoNordeste.Application.DTOs.Requests.Pedido;

public class PedidoProdutoRequest
{
    public Guid ProdutoId { get; set; }
    public decimal Quantidade { get; set; }
}
