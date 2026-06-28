using RaizesDoNordeste.Domain.Enums;

namespace RaizesDoNordeste.Domain.Entities;

public class MovimentacaoEstoque
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid ProdutoId { get; private set; }
    public Produto Produto { get; private set; } = null!;
    public Guid UnidadeId { get; private set; }
    public Unidade Unidade { get; private set; } = null!;
    public Guid UsuarioId { get; private set; }
    public Usuario Usuario { get; private set; } = null!;
    public Guid DocumentoOrigemId { get; private set; }
    public TipoMovimentacaoOrigem TipoMovimentacaoOrigem { get; private set; }
    public decimal Quantidade { get; private set; }
    public string Observacao { get; private set; } = string.Empty;
    public DateTime DataMovimentacao { get; private set; } = DateTime.UtcNow;
    public decimal SaldoAnterior { get; private set; }
    public decimal SaldoPosterior { get; private set; }

    public MovimentacaoEstoque(
        Guid produtoId,
        Guid unidadeId,
        Guid usuarioId,
        Guid documentoOrigemId,
        TipoMovimentacaoOrigem tipoMovimentacaoOrigem,
        decimal quantidade,
        string observacao,
        decimal saldoAnterior,
        decimal saldoPosterior)
    {
        ProdutoId = produtoId;
        UnidadeId = unidadeId;
        UsuarioId = usuarioId;
        DocumentoOrigemId = documentoOrigemId;
        TipoMovimentacaoOrigem = tipoMovimentacaoOrigem;
        Quantidade = quantidade;
        Observacao = observacao;
        SaldoAnterior = saldoAnterior;
        SaldoPosterior = saldoPosterior;
    }
    protected MovimentacaoEstoque() { }
}
