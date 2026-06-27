using Microsoft.EntityFrameworkCore;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.Infrastructure.Repositories;

public class UsuarioUnidadeRepository(AppDbContext context) : IUsuarioUnidadeRepository
{
    public async Task<UsuarioUnidade?> ObterVinculo(Guid usuarioId, Guid unidadeId, CancellationToken cancellationToken)
        => await context.UsuariosUnidades
            .FirstOrDefaultAsync(uu => uu.UsuarioId == usuarioId && uu.UnidadeId == unidadeId, cancellationToken);

    public async Task<IEnumerable<UsuarioUnidade>> ObterPorUsuario(Guid usuarioId, CancellationToken cancellationToken)
        => await context.UsuariosUnidades
            .Include(uu => uu.Unidade)
            .Where(uu => uu.UsuarioId == usuarioId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task Vincular(UsuarioUnidade usuarioUnidade, CancellationToken cancellationToken)
    {
        await context.UsuariosUnidades.AddAsync(usuarioUnidade, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Desvincular(UsuarioUnidade usuarioUnidade, CancellationToken cancellationToken)
    {
        context.UsuariosUnidades.Remove(usuarioUnidade);
        await context.SaveChangesAsync(cancellationToken);
    }
}
