using RaizesDoNordeste.Domain.Enums;

namespace RaizesDoNordeste.Application.DTOs.Responses.ClienteFidelizacao;

public class ClienteFidelizacaoObterResponse
{
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public string NomeCliente { get; set; } = string.Empty;
    public decimal PontosDisponiveis { get; set; }
    public NivelClienteFidelizacao Nivel { get; set; }
    public decimal TotalPontosAcumulados { get; set; }
    public bool ConsentimentoLGPD { get; set; }
    public DateTime DataAdesao { get; set; }
    public DateTime? DataUltimaAtualizacaoNivel { get; set; }
}
