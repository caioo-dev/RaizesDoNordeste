namespace RaizesDoNordeste.Domain.Entities;

public class Produto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public bool Ativo { get; set; } = true;
    public DateTime DataInclusao { get; set; } = DateTime.Now;
    public DateTime DataExclusao { get; set; }

    public ICollection<CardapioProduto> CardapioProdutos { get; } = [];
    public ICollection<ProdutoUnidade> ProdutoUnidades { get; } = [];
    public ICollection<PedidoProduto> PedidosProdutos { get; } = [];
}
