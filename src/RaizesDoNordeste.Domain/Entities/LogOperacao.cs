using RaizesDoNordeste.Domain.Enums;

namespace RaizesDoNordeste.Domain.Entities;

public class LogOperacao
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid UsuarioId { get; private set; }
    public Usuario Usuario { get; private set; } = null!;
    public string Entidade { get; private set; } = string.Empty;
    public Guid EntidadeId { get; private set; }
    public TipoAcaoLog Acao { get; private set; }
    public string DadosJson { get; private set; } = string.Empty;
    public DateTime DataOperacao { get; private set; } = DateTime.UtcNow;

    public LogOperacao(Guid usuarioId, string entidade, Guid entidadeId, TipoAcaoLog acao, string dadosJson)
    {
        UsuarioId = usuarioId;
        Entidade = entidade;
        EntidadeId = entidadeId;
        Acao = acao;
        DadosJson = dadosJson;
    }
    protected LogOperacao() { }
}
