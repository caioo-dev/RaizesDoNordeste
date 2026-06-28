using RaizesDoNordeste.Domain.Enums;

namespace RaizesDoNordeste.Application.DTOs.Requests.MovimentacaoEstoque;

public class CriarMovimentacaoEstoqueRequest
{
    public Guid ProdutoId { get; set; }
    public Guid UnidadeId { get; set; }
    public Guid DocumentoOrigemId { get; set; }
    public TipoMovimentacaoOrigem TipoMovimentacaoOrigem { get; set; }
    public decimal Quantidade { get; set; }
    public string Observacao { get; set; } = string.Empty;
}
