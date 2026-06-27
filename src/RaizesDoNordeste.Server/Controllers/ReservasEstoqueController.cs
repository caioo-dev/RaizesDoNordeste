using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaizesDoNordeste.Application.DTOs.Requests.ReservaEstoque;
using RaizesDoNordeste.Application.DTOs.Responses.ReservaEstoque;
using RaizesDoNordeste.Application.Interfaces;
using RaizesDoNordeste.Server.Filters;

namespace RaizesDoNordeste.Server.Controllers;

[ApiController]
[Authorize]
[Route("api/reservas-estoque")]
public class ReservasEstoqueController(IReservaEstoqueService reservaEstoqueService) : BaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(ReservaEstoqueObterTodosResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterTodos(CancellationToken cancellationToken)
    {
        ReservaEstoqueObterTodosResponse response = await reservaEstoqueService.ObterTodos(cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ReservaEstoqueObterPorIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        ReservaEstoqueObterPorIdResponse? response = await reservaEstoqueService.ObterPorId(id, cancellationToken);
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
    public async Task<IActionResult> Criar([FromBody] CriarReservaEstoqueRequest request, CancellationToken cancellationToken)
    {
        await reservaEstoqueService.Criar(ObterUsuarioId(), request, cancellationToken);
        return Created();
    }

    [HttpPatch("{id:guid}/confirmar")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Confirmar(Guid id, CancellationToken cancellationToken)
    {
        await reservaEstoqueService.Confirmar(id, cancellationToken);
        return NoContent();
    }

    [HttpPatch("{id:guid}/cancelar")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Cancelar(Guid id, CancellationToken cancellationToken)
    {
        await reservaEstoqueService.Cancelar(id, cancellationToken);
        return NoContent();
    }
}
