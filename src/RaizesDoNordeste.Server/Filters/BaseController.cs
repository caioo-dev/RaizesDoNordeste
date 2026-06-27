using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace RaizesDoNordeste.Server.Filters;

public abstract class BaseController : ControllerBase
{
    protected Guid ObterUsuarioId()
        => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
}
