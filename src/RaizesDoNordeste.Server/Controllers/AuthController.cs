using Microsoft.AspNetCore.Mvc;
using RaizesDoNordeste.Application.DTOs.Requests.Usuario;
using RaizesDoNordeste.Application.DTOs.Responses.Usuario;
using RaizesDoNordeste.Application.Interfaces;

namespace RaizesDoNordeste.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        AuthResponse response = await authService.Register(request, cancellationToken);
        return StatusCode(StatusCodes.Status201Created, response);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        AuthResponse response = await authService.Login(request, cancellationToken);
        return Ok(response);
    }
}
