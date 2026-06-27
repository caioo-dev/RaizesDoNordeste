using Microsoft.EntityFrameworkCore;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.Infrastructure.Repositories;

public class UsuarioRepository(AppDbContext context) : IUsuarioRepository
{
    public async Task<Usuario?> ObterPorEmail(string email, CancellationToken cancellationToken)
        => await context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == email && u.Ativo, cancellationToken);

    public async Task<bool> ExistePorEmail(string email, CancellationToken cancellationToken)
        => await context.Usuarios
            .AnyAsync(u => u.Email == email, cancellationToken);

    public async Task Criar(Usuario usuario, CancellationToken cancellationToken)
    {
        await context.Usuarios.AddAsync(usuario, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistePorId(Guid id, CancellationToken cancellationToken)
    => await context.Usuarios
        .AnyAsync(u => u.Id == id && u.Ativo, cancellationToken);
}
