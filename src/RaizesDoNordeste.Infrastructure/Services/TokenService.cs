using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RaizesDoNordeste.Application.Interfaces;
using RaizesDoNordeste.Domain.Entities;

namespace RaizesDoNordeste.Infrastructure.Services;

public class TokenService(IConfiguration configuration) : ITokenService
{
    public string GerarToken(Usuario usuario)
    {
        string secretKey = configuration["Jwt:SecretKey"]!;
        string issuer = configuration["Jwt:Issuer"]!;
        string audience = configuration["Jwt:Audience"]!;
        int expiresInMinutes = int.Parse(configuration["Jwt:ExpiresInMinutes"]!, CultureInfo.InvariantCulture);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        Claim[] claims =
        [
            new(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, usuario.Email),
            new(JwtRegisteredClaimNames.Name, usuario.Nome),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        ];

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiresInMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
