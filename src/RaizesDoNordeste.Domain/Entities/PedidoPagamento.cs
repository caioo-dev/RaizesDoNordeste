using RaizesDoNordeste.CrossCutting.Exceptions;
using RaizesDoNordeste.Domain.Enums;

namespace RaizesDoNordeste.Domain.Entities;

public class PedidoPagamento
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid PedidoId { get; private set; }
    public Pedido Pedido { get; private set; } = null!;
    public TipoPagamento TipoPagamento { get; private set; }
    public decimal Valor { get; private set; }
    public PagamentoStatus Status { get; private set; } = PagamentoStatus.Pendente;
    public DateTime? DataPagamento { get; private set; }

    public PedidoPagamento(Guid pedidoId, TipoPagamento tipoPagamento, decimal valor)
    {
        PedidoId = pedidoId;
        TipoPagamento = tipoPagamento;
        Valor = valor;
    }
    protected PedidoPagamento() { }

    public void Confirmar()
    {
        if (Status != PagamentoStatus.Pendente)
        {
            throw new ConflictException($"Pagamento não pode ser confirmado pois está com status '{Status}'.");
        }

        Status = PagamentoStatus.Pago;
        DataPagamento = DateTime.UtcNow;
    }

    public void Estornar()
    {
        if (Status != PagamentoStatus.Pago)
        {
            throw new ConflictException($"Pagamento não pode ser estornado pois está com status '{Status}'.");
        }

        Status = PagamentoStatus.Estornado;
    }

    public void Cancelar()
    {
        if (Status == PagamentoStatus.Pago || Status == PagamentoStatus.Estornado)
        {
            throw new ConflictException($"Pagamento não pode ser cancelado pois está com status '{Status}'.");
        }

        Status = PagamentoStatus.Cancelado;
    }
}
