namespace RaizesDoNordeste.Application.DTOs.Responses.Pedido;

public class PedidoProdutoResponse
{
    public Guid Id { get; set; }
    public Guid ProdutoId { get; set; }
    public string NomeProduto { get; set; } = string.Empty;
    public decimal PrecoUnitario { get; set; }
    public decimal Quantidade { get; set; }
    public decimal ValorTotal { get; set; }
}
