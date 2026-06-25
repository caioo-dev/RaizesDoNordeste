using RaizesDoNordeste.Application.DTOs.Requests.Usuario;
using RaizesDoNordeste.Application.DTOs.Responses.Usuario;

namespace RaizesDoNordeste.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> Register(RegisterRequest request, CancellationToken cancellationToken);
    Task<AuthResponse> Login(LoginRequest request, CancellationToken cancellationToken);
}
