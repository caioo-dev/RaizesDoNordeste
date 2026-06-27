namespace RaizesDoNordeste.Domain.Entities;

public class CardapioProduto
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid CardapioId { get; private set; }
    public Cardapio Cardapio { get; private set; } = null!;
    public Guid ProdutoId { get; private set; }
    public Produto Produto { get; private set; } = null!;
    public decimal PrecoVenda { get; private set; }
    public bool Disponivel { get; private set; } = true;

    public CardapioProduto(Guid cardapioId, Guid produtoId, decimal precoVenda)
    {
        CardapioId = cardapioId;
        ProdutoId = produtoId;
        PrecoVenda = precoVenda;
    }
    protected CardapioProduto() { }

    public void Atualizar(decimal precoVenda, bool disponivel)
    {
        PrecoVenda = precoVenda;
        Disponivel = disponivel;
    }
}
