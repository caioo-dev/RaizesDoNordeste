using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaizesDoNordeste.Application.DTOs.Requests.ProdutoUnidade;
using RaizesDoNordeste.Application.DTOs.Responses.ProdutoUnidade;
using RaizesDoNordeste.Application.Interfaces;

namespace RaizesDoNordeste.Server.Controllers;

[ApiController]
[Route("api/unidades/{unidadeId:guid}/produtos")]
[Authorize]
public class UnidadeProdutosController(IProdutoUnidadeService produtoUnidadeService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ProdutoUnidadeObterTodosResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorUnidade(Guid unidadeId, CancellationToken cancellationToken)
    {
        ProdutoUnidadeObterTodosResponse response = await produtoUnidadeService.ObterPorUnidade(unidadeId, cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProdutoUnidadeObterPorIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        ProdutoUnidadeObterPorIdResponse? response = await produtoUnidadeService.ObterPorId(id, cancellationToken);
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
    public async Task<IActionResult> Criar(Guid unidadeId, [FromBody] ProdutoUnidadeRequest request, CancellationToken cancellationToken)
    {
        await produtoUnidadeService.Criar(unidadeId, request, cancellationToken);
        return Created();
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] ProdutoUnidadeRequest request, CancellationToken cancellationToken)
    {
        await produtoUnidadeService.Atualizar(id, request, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Excluir(Guid id, CancellationToken cancellationToken)
    {
        await produtoUnidadeService.Excluir(id, cancellationToken);
        return NoContent();
    }
}
