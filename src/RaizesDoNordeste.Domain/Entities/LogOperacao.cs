using RaizesDoNordeste.Domain.Enums;

namespace RaizesDoNordeste.Domain.Entities;

public class LogOperacao
{
    public Guid Id { get; set; }

    public Guid UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;

    public string Entidade { get; set; } = string.Empty;

    public Guid EntidadeId { get; set; }

    public TipoAcaoLog Acao { get; set; }

    public string DadosJson { get; set; } = string.Empty;

    public DateTime DataOperacao { get; set; } = DateTime.UtcNow;
}
