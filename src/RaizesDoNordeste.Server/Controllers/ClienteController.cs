using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using RaizesDoNordeste.Application.DTOs.Requests.Cliente;
using RaizesDoNordeste.Application.Interfaces;

namespace RaizesDoNordeste.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class ClienteController(IClienteService clienteService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarClienteRequest request, CancellationToken cancellationToken)
    {
        await clienteService.Criar(request, cancellationToken);
        return Ok();
    }
}
