namespace RaizesDoNordeste.Domain.Entities;

public class Produto
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Nome { get; private set; } = string.Empty;
    public bool Ativo { get; private set; } = true;
    public DateTime DataInclusao { get; private set; } = DateTime.UtcNow;
    public DateTime? DataExclusao { get; private set; }

    public ICollection<CardapioProduto> CardapioProdutos { get; } = [];
    public ICollection<ProdutoUnidade> ProdutoUnidades { get; } = [];
    public ICollection<PedidoProduto> PedidosProdutos { get; } = [];

    public Produto(string nome)
    {
        Nome = nome;
    }
    protected Produto() { }

    public void Atualizar(string nome, bool ativo)
    {
        Nome = nome;
        Ativo = ativo;
    }

    public void Excluir()
    {
        Ativo = false;
        DataExclusao = DateTime.UtcNow;
    }
}
