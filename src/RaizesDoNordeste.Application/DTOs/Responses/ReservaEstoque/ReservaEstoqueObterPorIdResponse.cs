using RaizesDoNordeste.Domain.Enums;

namespace RaizesDoNordeste.Application.DTOs.Responses.ReservaEstoque;

public class ReservaEstoqueObterPorIdResponse
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
    public StatusReservaEstoque Status { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime DataExpiracao { get; set; }
}
