namespace RaizesDoNordeste.Domain.Entities;

public class PedidoProduto
{
    public Guid Id { get; set; }

    public Guid PedidoId { get; set; }
    public Pedido Pedido { get; set; } = null!;

    public Guid ProdutoId { get; set; }
    public Produto Produto { get; set; } = null!;

    public string NomeProduto { get; set; } = string.Empty;

    public decimal Quantidade { get; set; }

    public decimal PrecoUnitario { get; set; }

    public decimal ValorTotal { get; set; }

    public DateTime DataInclusao { get; set; } = DateTime.UtcNow;
}
