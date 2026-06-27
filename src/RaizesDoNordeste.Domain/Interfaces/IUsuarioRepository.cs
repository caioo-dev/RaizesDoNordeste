using RaizesDoNordeste.Domain.Entities;

namespace RaizesDoNordeste.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> ObterPorEmail(string email, CancellationToken cancellationToken);
    Task<bool> ExistePorEmail(string email, CancellationToken cancellationToken);
    Task<bool> ExistePorId(Guid id, CancellationToken cancellationToken);
    Task Criar(Usuario usuario, CancellationToken cancellationToken);
}
