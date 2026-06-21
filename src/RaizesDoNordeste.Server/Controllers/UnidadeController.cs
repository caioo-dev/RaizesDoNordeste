using Microsoft.AspNetCore.Mvc;
using RaizesDoNordeste.Application.DTOs.Requests.Unidade;
using RaizesDoNordeste.Application.DTOs.Responses.Unidade;
using RaizesDoNordeste.Application.Interfaces;

namespace RaizesDoNordeste.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UnidadesController(IUnidadeService unidadeService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(UnidadeObterTodosResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterTodos(CancellationToken cancellationToken)
    {
        UnidadeObterTodosResponse response = await unidadeService.ObterTodos(cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(UnidadeObterPorIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        UnidadeObterPorIdResponse? response = await unidadeService.ObterPorId(id, cancellationToken);
        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Criar([FromBody] CriarUnidadeRequest request, CancellationToken cancellationToken)
    {
        await unidadeService.Criar(request, cancellationToken);
        return Created();
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] AtualizarUnidadeRequest request, CancellationToken cancellationToken)
    {
        await unidadeService.Atualizar(id, request, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Excluir(Guid id, CancellationToken cancellationToken)
    {
        await unidadeService.Excluir(id, cancellationToken);
        return NoContent();
    }
}
