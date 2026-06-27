using RaizesDoNordeste.Application.DTOs.Requests.Usuario;
using RaizesDoNordeste.Application.DTOs.Responses.Usuario;
using RaizesDoNordeste.Application.Interfaces;
using RaizesDoNordeste.CrossCutting.Exceptions;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.Application.Services;

public class AuthService(
    IUsuarioRepository usuarioRepository,
    ITokenService tokenService) : IAuthService
{
    public async Task<AuthResponse> Register(RegisterRequest request, CancellationToken cancellationToken)
    {
        bool emailEmUso = await usuarioRepository.ExistePorEmail(request.Email, cancellationToken);
        if (emailEmUso)
        {
            throw new ConflictException("E-mail já cadastrado.");
        }

        string senhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha);

        var usuario = new Usuario(request.Nome, request.Email, senhaHash);

        await usuarioRepository.Criar(usuario, cancellationToken);

        string token = tokenService.GerarToken(usuario);

        return new AuthResponse
        {
            Token = token,
            Nome = usuario.Nome,
            Email = usuario.Email
        };
    }

    public async Task<AuthResponse> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        Usuario? usuario = await usuarioRepository.ObterPorEmail(request.Email, cancellationToken);

        if (usuario is null || !BCrypt.Net.BCrypt.Verify(request.Senha, usuario.SenhaHash))
        {
            throw new UnauthorizedException();
        }

        string token = tokenService.GerarToken(usuario);

        return new AuthResponse
        {
            Token = token,
            Nome = usuario.Nome,
            Email = usuario.Email
        };
    }
}
