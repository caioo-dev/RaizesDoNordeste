namespace RaizesDoNordeste.Application.DTOs.Requests.ClienteFidelizacao;

public class AderirProgramaFidelizacaoRequest
{
    public Guid ClienteId { get; set; }
    public bool ConsentimentoLGPD { get; set; }
}
