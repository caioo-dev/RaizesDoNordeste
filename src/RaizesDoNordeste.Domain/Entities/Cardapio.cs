namespace RaizesDoNordeste.Domain.Entities;

public class Cardapio
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid UnidadeId { get; private set; }
    public Unidade Unidade { get; private set; } = null!;
    public string Nome { get; private set; } = string.Empty;
    public bool Ativo { get; private set; } = true;
    public DateTime? DataExclusao { get; private set; }

    public ICollection<CardapioProduto> CardapioProdutos { get; } = [];

    public Cardapio(Guid unidadeId, string nome)
    {
        UnidadeId = unidadeId;
        Nome = nome;
    }
    protected Cardapio() { }

    public void Atualizar(Guid unidadeId, string nome, bool ativo)
    {
        UnidadeId = unidadeId;
        Nome = nome;
        Ativo = ativo;
    }

    public void Excluir()
    {
        Ativo = false;
        DataExclusao = DateTime.UtcNow;
    }
}
