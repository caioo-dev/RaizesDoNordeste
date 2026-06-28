using RaizesDoNordeste.CrossCutting.Exceptions;
using RaizesDoNordeste.Domain.Enums;
using RaizesDoNordeste.Domain.ValueObjects;

namespace RaizesDoNordeste.Domain.Entities;

public class Pedido
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid ClienteId { get; private set; }
    public Cliente Cliente { get; private set; } = null!;
    public Guid UsuarioId { get; private set; }
    public Usuario Usuario { get; private set; } = null!;
    public Guid UnidadeId { get; private set; }
    public Unidade Unidade { get; private set; } = null!;
    public EnderecoEntrega EnderecoEntrega { get; private set; } = null!;
    public PedidoStatus Status { get; private set; } = PedidoStatus.Pendente;
    public decimal Total { get; private set; }
    public DateTime? DataEntrega { get; private set; }
    public DateTime DataInclusao { get; private set; } = DateTime.UtcNow;

    public ICollection<PedidoProduto> PedidosProdutos { get; } = [];

    public Pedido(Guid clienteId, Guid usuarioId, Guid unidadeId, EnderecoEntrega enderecoEntrega)
    {
        ClienteId = clienteId;
        UsuarioId = usuarioId;
        UnidadeId = unidadeId;
        EnderecoEntrega = enderecoEntrega;
    }
    protected Pedido() { }

    public void CalcularTotal()
        => Total = PedidosProdutos.Sum(p => p.ValorTotal);

    public void Confirmar()
    {
        ValidarTransicao(PedidoStatus.Pendente, PedidoStatus.EmPreparo);
        Status = PedidoStatus.EmPreparo;
    }

    public void SairParaEntrega()
    {
        ValidarTransicao(PedidoStatus.EmPreparo, PedidoStatus.SaiuParaEntrega);
        Status = PedidoStatus.SaiuParaEntrega;
    }

    public void Entregar()
    {
        ValidarTransicao(PedidoStatus.SaiuParaEntrega, PedidoStatus.Entregue);
        Status = PedidoStatus.Entregue;
        DataEntrega = DateTime.UtcNow;
    }

    public void Cancelar()
    {
        if (Status == PedidoStatus.Entregue || Status == PedidoStatus.Cancelado)
        {
            throw new ConflictException($"Pedido não pode ser cancelado pois está com status '{Status}'.");
        }

        Status = PedidoStatus.Cancelado;
    }

    private void ValidarTransicao(PedidoStatus statusEsperado, PedidoStatus proximoStatus)
    {
        if (Status != statusEsperado)
        {
            throw new ConflictException($"Pedido não pode ir para '{proximoStatus}' pois está com status '{Status}'.");
        }
    }
}
