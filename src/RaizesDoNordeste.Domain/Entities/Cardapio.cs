namespace RaizesDoNordeste.Domain.Entities;

public class Cardapio
{
    public Guid Id { get; set; }
    public Guid UnidadeId { get; set; }
    public Unidade Unidade { get; set; } = null!;
    public string Nome { get; set; } = string.Empty;
    public bool Ativo { get; set; } = true;
    public DateTime DataExclusao { get; set; }

    public ICollection<CardapioProduto> CardapioProdutos { get; } = [];
}
