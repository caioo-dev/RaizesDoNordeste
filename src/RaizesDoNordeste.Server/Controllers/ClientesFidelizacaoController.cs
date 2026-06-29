using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaizesDoNordeste.Application.DTOs.Requests.ClienteFidelizacao;
using RaizesDoNordeste.Application.DTOs.Responses.ClienteFidelizacao;
using RaizesDoNordeste.Application.Interfaces;
using RaizesDoNordeste.Server.Filters;

namespace RaizesDoNordeste.Server.Controllers;

[ApiController]
[Authorize]
[Route("api/clientes/{clienteId:guid}/fidelizacao")]
public class ClientesFidelizacaoController(IClienteFidelizacaoService clienteFidelizacaoService) : BaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(ClienteFidelizacaoObterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Obter(Guid clienteId, CancellationToken cancellationToken)
    {
        ClienteFidelizacaoObterResponse? response = await clienteFidelizacaoService.ObterPorClienteId(clienteId, cancellationToken);
        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("movimentacoes")]
    [ProducesResponseType(typeof(ClienteFidelizacaoComMovimentacoesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterComMovimentacoes(Guid clienteId, CancellationToken cancellationToken)
    {
        ClienteFidelizacaoComMovimentacoesResponse? response = await clienteFidelizacaoService.ObterComMovimentacoes(clienteId, cancellationToken);
        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Aderir(Guid clienteId, CancellationToken cancellationToken)
    {
        var request = new AderirProgramaFidelizacaoRequest { ClienteId = clienteId, ConsentimentoLGPD = true };
        await clienteFidelizacaoService.Aderir(request, cancellationToken);
        return Created();
    }

    [HttpDelete("consentimento-lgpd")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RevogarConsentimentoLGPD(Guid clienteId, CancellationToken cancellationToken)
    {
        await clienteFidelizacaoService.RevogarConsentimentoLGPD(clienteId, cancellationToken);
        return NoContent();
    }
}
