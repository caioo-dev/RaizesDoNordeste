using RaizesDoNordeste.Domain.Enums;

namespace RaizesDoNordeste.Application.DTOs.Responses.LogOperacao;

public class LogOperacaoResponse
{
    public Guid Id { get; set; }
    public Guid UsuarioId { get; set; }
    public string Entidade { get; set; } = string.Empty;
    public Guid EntidadeId { get; set; }
    public TipoAcaoLog Acao { get; set; }
    public string DadosJson { get; set; } = string.Empty;
    public DateTime DataOperacao { get; set; }
}
