using RaizesDoNordeste.Domain.Enums;

namespace RaizesDoNordeste.Application.DTOs.Responses.PedidoStatusHistorico;

public class PedidoStatusHistoricoResponse
{
    public Guid Id { get; set; }
    public PedidoStatus StatusAnterior { get; set; }
    public PedidoStatus StatusNovo { get; set; }
    public Guid UsuarioId { get; set; }
    public string? Observacao { get; set; }
    public DateTime DataAlteracao { get; set; }
}
