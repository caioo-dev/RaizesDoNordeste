using RaizesDoNordeste.Domain.Enums;

namespace RaizesDoNordeste.Domain.Entities;

public class ReservaEstoque
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
    public StatusReservaEstoque Status { get; private set; } = StatusReservaEstoque.Pendente;
    public DateTime DataCriacao { get; private set; } = DateTime.UtcNow;
    public DateTime DataExpiracao { get; private set; }

    public ReservaEstoque(
        Guid produtoId,
        Guid unidadeId,
        Guid usuarioId,
        Guid documentoOrigemId,
        TipoMovimentacaoOrigem tipoMovimentacaoOrigem,
        decimal quantidade,
        DateTime dataExpiracao)
    {
        ProdutoId = produtoId;
        UnidadeId = unidadeId;
        UsuarioId = usuarioId;
        DocumentoOrigemId = documentoOrigemId;
        TipoMovimentacaoOrigem = tipoMovimentacaoOrigem;
        Quantidade = quantidade;
        DataExpiracao = dataExpiracao;
    }
    protected ReservaEstoque() { }

    public void Confirmar()
        => Status = StatusReservaEstoque.Confirmada;

    public void Cancelar()
        => Status = StatusReservaEstoque.Cancelada;

    public void Expirar()
        => Status = StatusReservaEstoque.Expirada;
}
