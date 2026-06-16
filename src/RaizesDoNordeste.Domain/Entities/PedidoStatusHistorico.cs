using RaizesDoNordeste.Domain.Enums;

namespace RaizesDoNordeste.Domain.Entities;

public class PedidoStatusHistorico
{
    public Guid Id { get; set; }

    public Guid PedidoId { get; set; }
    public Pedido Pedido { get; set; } = null!;

    public PedidoStatus StatusAnterior { get; set; }

    public PedidoStatus StatusNovo { get; set; }

    public Guid UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;

    public string? Observacao { get; set; }

    public DateTime DataAlteracao { get; set; } = DateTime.UtcNow;
}
