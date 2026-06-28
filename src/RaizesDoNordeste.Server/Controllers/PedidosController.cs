using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaizesDoNordeste.Application.DTOs.Requests.Pedido;
using RaizesDoNordeste.Application.DTOs.Responses.Pedido;
using RaizesDoNordeste.Application.DTOs.Responses.PedidoStatusHistorico;
using RaizesDoNordeste.Application.Interfaces;
using RaizesDoNordeste.Application.Services;
using RaizesDoNordeste.Server.Filters;

namespace RaizesDoNordeste.Server.Controllers;

[ApiController]
[Authorize]
[Route("api/pedidos")]
public class PedidosController(IPedidoService pedidoService, IPedidoStatusHistoricoService pedidoStatusHistoricoService) : BaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(PedidoObterTodosResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterTodos(CancellationToken cancellationToken)
    {
        PedidoObterTodosResponse response = await pedidoService.ObterTodos(cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PedidoObterPorIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        PedidoObterPorIdResponse? response = await pedidoService.ObterPorId(id, cancellationToken);
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
    public async Task<IActionResult> Criar([FromBody] CriarPedidoRequest request, CancellationToken cancellationToken)
    {
        await pedidoService.Criar(ObterUsuarioId(), request, cancellationToken);
        return Created();
    }

    [HttpPatch("{id:guid}/confirmar")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Confirmar(Guid id, CancellationToken cancellationToken)
    {
        await pedidoService.Confirmar(id, ObterUsuarioId(), cancellationToken);
        return NoContent();
    }

    [HttpPatch("{id:guid}/sair-para-entrega")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> SairParaEntrega(Guid id, CancellationToken cancellationToken)
    {
        await pedidoService.SairParaEntrega(id, ObterUsuarioId(), cancellationToken);
        return NoContent();
    }

    [HttpPatch("{id:guid}/entregar")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Entregar(Guid id, CancellationToken cancellationToken)
    {
        await pedidoService.Entregar(id, ObterUsuarioId(), cancellationToken);
        return NoContent();
    }

    [HttpPatch("{id:guid}/cancelar")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Cancelar(Guid id, CancellationToken cancellationToken)
    {
        await pedidoService.Cancelar(id, ObterUsuarioId(), cancellationToken);
        return NoContent();
    }

    [HttpGet("{id:guid}/historico")]
    [ProducesResponseType(typeof(IEnumerable<PedidoStatusHistoricoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterHistorico(Guid id, CancellationToken cancellationToken)
    {
        IEnumerable<PedidoStatusHistoricoResponse> response = await pedidoStatusHistoricoService.ObterPorPedido(id, cancellationToken);
        return Ok(response);
    }
}
