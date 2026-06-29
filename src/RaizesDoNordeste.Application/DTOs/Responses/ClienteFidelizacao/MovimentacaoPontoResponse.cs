using RaizesDoNordeste.Domain.Enums;

namespace RaizesDoNordeste.Application.DTOs.Responses.ClienteFidelizacao;

public class MovimentacaoPontoResponse
{
    public Guid Id { get; set; }
    public TipoMovimentacaoPonto Tipo { get; set; }
    public decimal Pontos { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataMovimentacao { get; set; }
}
