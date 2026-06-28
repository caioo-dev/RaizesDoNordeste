using RaizesDoNordeste.Domain.Enums;

namespace RaizesDoNordeste.Application.DTOs.Responses.MovimentacaoEstoque;

public class MovimentacaoEstoqueObterPorIdResponse
{
    public Guid Id { get; set; }
    public Guid ProdutoId { get; set; }
    public string NomeProduto { get; set; } = string.Empty;
    public Guid UnidadeId { get; set; }
    public string NomeFantasiaUnidade { get; set; } = string.Empty;
    public Guid UsuarioId { get; set; }
    public Guid DocumentoOrigemId { get; set; }
    public TipoMovimentacaoOrigem TipoMovimentacaoOrigem { get; set; }
    public decimal Quantidade { get; set; }
    public string Observacao { get; set; } = string.Empty;
    public DateTime DataMovimentacao { get; set; }
    public decimal SaldoAnterior { get; set; }
    public decimal SaldoPosterior { get; set; }
}
