using RaizesDoNordeste.Domain.Enums;

namespace RaizesDoNordeste.Domain.Entities;

public class MovimentacaoPonto
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid ClienteFidelizacaoId { get; private set; }
    public ClienteFidelizacao ClienteFidelizacao { get; private set; } = null!;
    public TipoMovimentacaoPonto Tipo { get; private set; }
    public decimal Pontos { get; private set; }
    public string Descricao { get; private set; } = string.Empty;
    public DateTime DataMovimentacao { get; private set; } = DateTime.UtcNow;

    public MovimentacaoPonto(Guid clienteFidelizacaoId, TipoMovimentacaoPonto tipo, decimal pontos, string descricao)
    {
        ClienteFidelizacaoId = clienteFidelizacaoId;
        Tipo = tipo;
        Pontos = pontos;
        Descricao = descricao;
    }
    protected MovimentacaoPonto() { }
}
