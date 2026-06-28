using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaizesDoNordeste.Application.DTOs.Requests.MovimentacaoEstoque;
using RaizesDoNordeste.Application.DTOs.Responses.MovimentacaoEstoque;
using RaizesDoNordeste.Application.Interfaces;
using RaizesDoNordeste.Server.Filters;

namespace RaizesDoNordeste.Server.Controllers;

[ApiController]
[Authorize]
[Route("api/movimentacoes-estoque")]
public class MovimentacoesEstoqueController(IMovimentacaoEstoqueService movimentacaoEstoqueService) : BaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(MovimentacaoEstoqueObterTodosResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterTodos(CancellationToken cancellationToken)
    {
        MovimentacaoEstoqueObterTodosResponse response = await movimentacaoEstoqueService.ObterTodos(cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(MovimentacaoEstoqueObterPorIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        MovimentacaoEstoqueObterPorIdResponse? response = await movimentacaoEstoqueService.ObterPorId(id, cancellationToken);
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
    public async Task<IActionResult> Registrar([FromBody] CriarMovimentacaoEstoqueRequest request, CancellationToken cancellationToken)
    {
        await movimentacaoEstoqueService.Registrar(ObterUsuarioId(), request, cancellationToken);
        return Created();
    }
}
