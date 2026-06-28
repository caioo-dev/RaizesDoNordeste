using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaizesDoNordeste.Application.DTOs.Requests.PedidoPagamento;
using RaizesDoNordeste.Application.DTOs.Responses.PedidoPagamento;
using RaizesDoNordeste.Application.Interfaces;
using RaizesDoNordeste.Server.Filters;

namespace RaizesDoNordeste.Server.Controllers;

[ApiController]
[Authorize]
[Route("api/pedidos/{pedidoId:guid}/pagamentos")]
public class PedidoPagamentosController(IPedidoPagamentoService pedidoPagamentoService) : BaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(PedidoPagamentoObterTodosResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorPedido(Guid pedidoId, CancellationToken cancellationToken)
    {
        PedidoPagamentoObterTodosResponse response = await pedidoPagamentoService.ObterPorPedido(pedidoId, cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PedidoPagamentoObterPorIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        PedidoPagamentoObterPorIdResponse? response = await pedidoPagamentoService.ObterPorId(id, cancellationToken);
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
    public async Task<IActionResult> Criar(Guid pedidoId, [FromBody] CriarPedidoPagamentoRequest request, CancellationToken cancellationToken)
    {
        await pedidoPagamentoService.Criar(pedidoId, request, cancellationToken);
        return Created();
    }

    [HttpPatch("{id:guid}/confirmar")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Confirmar(Guid id, CancellationToken cancellationToken)
    {
        await pedidoPagamentoService.Confirmar(id, cancellationToken);
        return NoContent();
    }

    [HttpPatch("{id:guid}/estornar")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Estornar(Guid id, CancellationToken cancellationToken)
    {
        await pedidoPagamentoService.Estornar(id, cancellationToken);
        return NoContent();
    }

    [HttpPatch("{id:guid}/cancelar")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Cancelar(Guid id, CancellationToken cancellationToken)
    {
        await pedidoPagamentoService.Cancelar(id, cancellationToken);
        return NoContent();
    }
}
