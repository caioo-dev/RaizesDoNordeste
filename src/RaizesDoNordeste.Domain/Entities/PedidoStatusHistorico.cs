using RaizesDoNordeste.Domain.Enums;

namespace RaizesDoNordeste.Domain.Entities;

public class PedidoStatusHistorico
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid PedidoId { get; private set; }
    public Pedido Pedido { get; private set; } = null!;
    public PedidoStatus StatusAnterior { get; private set; }
    public PedidoStatus StatusNovo { get; private set; }
    public Guid UsuarioId { get; private set; }
    public Usuario Usuario { get; private set; } = null!;
    public string? Observacao { get; private set; }
    public DateTime DataAlteracao { get; private set; } = DateTime.UtcNow;

    public PedidoStatusHistorico(Guid pedidoId, PedidoStatus statusAnterior, PedidoStatus statusNovo, Guid usuarioId, string? observacao = null)
    {
        PedidoId = pedidoId;
        StatusAnterior = statusAnterior;
        StatusNovo = statusNovo;
        UsuarioId = usuarioId;
        Observacao = observacao;
    }
    protected PedidoStatusHistorico() { }
}
