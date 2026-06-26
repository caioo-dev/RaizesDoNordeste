using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaizesDoNordeste.Application.DTOs.Requests.Cliente;
using RaizesDoNordeste.Application.DTOs.Responses.Cliente;
using RaizesDoNordeste.Application.Interfaces;

namespace RaizesDoNordeste.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public sealed class ClienteController(IClienteService clienteService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarClienteRequest request, CancellationToken cancellationToken)
    {
        await clienteService.Criar(request, cancellationToken);
        return Created();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Deletar(Guid id, CancellationToken cancellationToken)
    {
        await clienteService.Deletar(id, cancellationToken);
        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        ClienteObterPorIdResponse? cliente = await clienteService.ObterPorId(id, cancellationToken);

        if (cliente is null)
        {
            return NotFound();
        }

        return Ok(cliente);
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos(CancellationToken cancellationToken)
    {
        ClienteObterTodosResponse response = await clienteService.ObterTodos(cancellationToken);
        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar(Guid id,
        [FromBody] AtualizarClienteRequest request,
        CancellationToken cancellationToken
        )
    {
        await clienteService.Atualizar(id, request, cancellationToken);
        return NoContent();
    }
}
