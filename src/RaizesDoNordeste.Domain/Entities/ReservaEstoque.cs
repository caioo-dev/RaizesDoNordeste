using RaizesDoNordeste.Domain.Enums;

namespace RaizesDoNordeste.Domain.Entities;

public class ReservaEstoque
{
    public Guid Id { get; set; }
    public Guid ProdutoId { get; set; }
    public Produto Produto { get; set; } = null!;
    public Guid UnidadeId { get; set; }
    public Unidade Unidade { get; set; } = null!;
    public Guid UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
    public Guid DocumentoOrigemId { get; set; }
    public TipoMovimentacaoOrigem TipoMovimentacaoOrigem { get; set; }
    public decimal Quantidade { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public DateTime DataExpiracao { get; set; } = DateTime.Now.AddDays(7); 
}
