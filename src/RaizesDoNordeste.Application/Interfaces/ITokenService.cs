using RaizesDoNordeste.Domain.Entities;

namespace RaizesDoNordeste.Application.Interfaces;

public interface ITokenService
{
    string GerarToken(Usuario usuario);
}
