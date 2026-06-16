using RaizesDoNordeste.Domain.Enums;
using RaizesDoNordeste.Domain.ValueObjects;

namespace RaizesDoNordeste.Domain.Entities;

public class Pedido
{
    public Guid Id { get; set; }

    public Guid ClienteId { get; set; }
    public Cliente Cliente { get; set; } = null!;

    public Guid UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;

    public Guid UnidadeId { get; set; }
    public Unidade Unidade { get; set; } = null!;

    public EnderecoEntrega EnderecoEntrega { get; set; } = null!;

    public PedidoStatus Status { get; set; }

    public decimal Total { get; set; }

    public DateTime? DataEntrega { get; set; }
    public DateTime DataInclusao { get; set; } = DateTime.UtcNow;

    public ICollection<PedidoProduto> PedidosProdutos { get; } = [];
}
