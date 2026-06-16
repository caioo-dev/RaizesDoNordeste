namespace RaizesDoNordeste.Domain.Entities;

public class CardapioProduto
{
    public Guid Id { get; set; }

    public Guid CardapioId { get; set; }
    public Cardapio Cardapio { get; set; } = null!;

    public Guid ProdutoId { get; set; }
    public Produto Produto { get; set; } = null!;

    public decimal PrecoVenda { get; set; }
    public bool Disponivel { get; set; } = true;
}
