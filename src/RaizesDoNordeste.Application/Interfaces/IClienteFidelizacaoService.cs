using RaizesDoNordeste.Application.DTOs.Requests.ClienteFidelizacao;
using RaizesDoNordeste.Application.DTOs.Responses.ClienteFidelizacao;

namespace RaizesDoNordeste.Application.Interfaces;

public interface IClienteFidelizacaoService
{
    Task<ClienteFidelizacaoObterResponse?> ObterPorClienteId(Guid clienteId, CancellationToken cancellationToken);
    Task<ClienteFidelizacaoComMovimentacoesResponse?> ObterComMovimentacoes(Guid clienteId, CancellationToken cancellationToken);
    Task Aderir(AderirProgramaFidelizacaoRequest request, CancellationToken cancellationToken);
    Task RevogarConsentimentoLGPD(Guid clienteId, CancellationToken cancellationToken);
}
