using RaizesDoNordeste.Domain.Enums;

namespace RaizesDoNordeste.Application.DTOs.Responses.MovimentacaoEstoque;

public class MovimentacaoEstoqueObterTodosResponse
{
    public IEnumerable<MovimentacaoEstoqueObterTodosModel> Movimentacoes { get; set; } = [];
}
public class MovimentacaoEstoqueObterTodosModel
{
    public Guid Id { get; set; }
    public Guid ProdutoId { get; set; }
    public string NomeProduto { get; set; } = string.Empty;
    public Guid UnidadeId { get; set; }
    public string NomeFantasiaUnidade { get; set; } = string.Empty;
    public TipoMovimentacaoOrigem TipoMovimentacaoOrigem { get; set; }
    public decimal Quantidade { get; set; }
    public DateTime DataMovimentacao { get; set; }
    public decimal SaldoPosterior { get; set; }
}
