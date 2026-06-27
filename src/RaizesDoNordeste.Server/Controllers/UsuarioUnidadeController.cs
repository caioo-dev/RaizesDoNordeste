using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaizesDoNordeste.Application.DTOs.Responses.UsuarioUnidade;
using RaizesDoNordeste.Application.Interfaces;
using RaizesDoNordeste.Server.Filters;

namespace RaizesDoNordeste.Server.Controllers;

[ApiController]
[Route("api/usuario/unidades")]
[Authorize]
public class UsuarioUnidadesController(IUsuarioUnidadeService usuarioUnidadeService) : BaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(UsuarioUnidadeResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterUnidades(CancellationToken cancellationToken)
    {
        UsuarioUnidadeResponse response = await usuarioUnidadeService.ObterUnidades(ObterUsuarioId(), cancellationToken);
        return Ok(response);
    }

    [HttpPost("{unidadeId:guid}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Vincular(Guid unidadeId, CancellationToken cancellationToken)
    {
        await usuarioUnidadeService.Vincular(ObterUsuarioId(), unidadeId, cancellationToken);
        return Created();
    }

    [HttpDelete("{unidadeId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Desvincular(Guid unidadeId, CancellationToken cancellationToken)
    {
        await usuarioUnidadeService.Desvincular(ObterUsuarioId(), unidadeId, cancellationToken);
        return NoContent();
    }
}
