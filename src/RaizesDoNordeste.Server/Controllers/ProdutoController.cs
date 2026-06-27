using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaizesDoNordeste.Application.DTOs.Requests.Produto;
using RaizesDoNordeste.Application.DTOs.Responses.Produto;
using RaizesDoNordeste.Application.Interfaces;

namespace RaizesDoNordeste.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProdutosController(IProdutoService produtoService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ProdutoObterTodosResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterTodos(CancellationToken cancellationToken)
    {
        ProdutoObterTodosResponse response = await produtoService.ObterTodos(cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProdutoObterPorIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        ProdutoObterPorIdResponse? response = await produtoService.ObterPorId(id, cancellationToken);
        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Criar([FromBody] ProdutoRequest request, CancellationToken cancellationToken)
    {
        await produtoService.Criar(request, cancellationToken);
        return Created();
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] ProdutoRequest request, CancellationToken cancellationToken)
    {
        await produtoService.Atualizar(id, request, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Excluir(Guid id, CancellationToken cancellationToken)
    {
        await produtoService.Excluir(id, cancellationToken);
        return NoContent();
    }
}
