namespace RaizesDoNordeste.Domain.Entities;

public class ProdutoUnidade
{
    public Guid Id { get; set; }
    public Guid ProdutoID { get; set; }
    public Produto Produto { get; set; } = null!;
    public Guid UnidadeID { get; set; }
    public Unidade Unidade { get; set; } = null!;
    public decimal EstoqueDisponivel { get; set; }
    public decimal EstoqueReservado { get; set; }
    public bool Ativo { get; set; } = true;
    public DateTime DataInclusao { get; set; } = DateTime.Now;
    public DateTime DataExclusao { get; set; }
}
