namespace RaizesDoNordeste.Domain.Entities;

public class ProdutoUnidade
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid ProdutoID { get; private set; }
    public Produto Produto { get; private set; } = null!;
    public Guid UnidadeID { get; private set; }
    public Unidade Unidade { get; private set; } = null!;
    public decimal EstoqueDisponivel { get; private set; }
    public decimal EstoqueReservado { get; private set; }
    public bool Ativo { get; private set; } = true;
    public DateTime DataInclusao { get; private set; } = DateTime.UtcNow;
    public DateTime? DataExclusao { get; private set; }

    public ProdutoUnidade(Guid produtoId, Guid unidadeId, decimal estoqueDisponivel)
    {
        ProdutoID = produtoId;
        UnidadeID = unidadeId;
        EstoqueDisponivel = estoqueDisponivel;
    }
    protected ProdutoUnidade() { }

    public void Atualizar(decimal estoqueDisponivel, decimal estoqueReservado, bool ativo)
    {
        EstoqueDisponivel = estoqueDisponivel;
        EstoqueReservado = estoqueReservado;
        Ativo = ativo;
    }

    public void Excluir()
    {
        Ativo = false;
        DataExclusao = DateTime.UtcNow;
    }
}
