using RaizesDoNordeste.CrossCutting.Exceptions;
using RaizesDoNordeste.Domain.Enums;

namespace RaizesDoNordeste.Domain.Entities;

public class ClienteFidelizacao
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid ClienteId { get; private set; }
    public Cliente Cliente { get; private set; } = null!;
    public decimal PontosDisponiveis { get; private set; }
    public NivelClienteFidelizacao Nivel { get; private set; } = NivelClienteFidelizacao.Bronze;
    public decimal TotalPontosAcumulados { get; private set; }
    public bool ConsentimentoLGPD { get; private set; }
    public DateTime DataAdesao { get; private set; } = DateTime.UtcNow;
    public DateTime? DataUltimaAtualizacaoNivel { get; private set; }

    public ICollection<MovimentacaoPonto> Movimentacoes { get; } = [];

    public ClienteFidelizacao(Guid clienteId, bool consentimentoLGPD)
    {
        ClienteId = clienteId;
        ConsentimentoLGPD = consentimentoLGPD;
    }

    // EF Core
    protected ClienteFidelizacao() { }

    public void AcumularPontos(decimal pontos, string descricao)
    {
        if (pontos <= 0)
        {
            throw new DomainException("Pontos devem ser maiores que zero.");
        }

        PontosDisponiveis += pontos;
        TotalPontosAcumulados += pontos;

        var movimentacao = new MovimentacaoPonto(Id, TipoMovimentacaoPonto.Acumulo, pontos, descricao);
        Movimentacoes.Add(movimentacao);

        AtualizarNivel();
    }

    public void ResgatarPontos(decimal pontos)
    {
        if (pontos <= 0)
        {
            throw new DomainException("Pontos devem ser maiores que zero.");
        }

        if (PontosDisponiveis < pontos)
        {
            throw new DomainException($"Pontos insuficientes. Disponíveis: {PontosDisponiveis}, solicitado: {pontos}.");
        }

        PontosDisponiveis -= pontos;

        var movimentacao = new MovimentacaoPonto(Id, TipoMovimentacaoPonto.Resgate, pontos, "Resgate de pontos em compra");
        Movimentacoes.Add(movimentacao);
    }

    public void RevogarConsentimentoLGPD()
    {
        ConsentimentoLGPD = false;
    }

    private void AtualizarNivel()
    {
        NivelClienteFidelizacao novoNivel = TotalPontosAcumulados switch
        {
            >= 5000 => NivelClienteFidelizacao.Platina,
            >= 3000 => NivelClienteFidelizacao.Ouro,
            >= 1500 => NivelClienteFidelizacao.Prata,
            _ => NivelClienteFidelizacao.Bronze
        };

        if (novoNivel != Nivel)
        {
            Nivel = novoNivel;
            DataUltimaAtualizacaoNivel = DateTime.UtcNow;
        }
    }
}
