namespace RaizesDoNordeste.Application.DTOs.Responses.ClienteFidelizacao;

public class ClienteFidelizacaoComMovimentacoesResponse
{
    public ClienteFidelizacaoObterResponse Cliente { get; set; } = null!;
    public List<MovimentacaoPontoResponse> Movimentacoes { get; set; } = [];
}
