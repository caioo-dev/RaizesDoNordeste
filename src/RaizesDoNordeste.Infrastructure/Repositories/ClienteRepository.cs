using Microsoft.EntityFrameworkCore;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.Infrastructure.Repositories;

public class ClienteRepository(AppDbContext context) : IClienteRepository
{

    public async Task Atualizar(Cliente cliente, CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Criar(Cliente cliente, CancellationToken cancellationToken)
    {
        await context.AddAsync(cliente, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
    }
    public async Task<Cliente?> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        return await context.Clientes.FindAsync([id], cancellationToken);
        //return await context.Clientes.FirstOrDefaultAsync(c => c.Id == id && c.Ativo == true, cancellationToken);
    }

    public async Task<IEnumerable<Cliente>> ObterTodos(CancellationToken cancellationToken)
    {
        return await context.Clientes.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistePorId(Guid id, CancellationToken cancellationToken)
    => await context.Clientes
        .AnyAsync(c => c.Id == id && c.Ativo, cancellationToken);
}
