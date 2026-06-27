using RaizesDoNordeste.Domain.Enums;

namespace RaizesDoNordeste.Application.DTOs.Requests.ReservaEstoque;

public class CriarReservaEstoqueRequest
{
    public Guid ProdutoId { get; set; }
    public Guid UnidadeId { get; set; }
    public Guid DocumentoOrigemId { get; set; }
    public TipoMovimentacaoOrigem TipoMovimentacaoOrigem { get; set; }
    public decimal Quantidade { get; set; }
    public DateTime DataExpiracao { get; set; }
}
