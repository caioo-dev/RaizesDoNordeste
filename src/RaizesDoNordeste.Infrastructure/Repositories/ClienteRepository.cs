using Microsoft.EntityFrameworkCore;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.Infrastructure.Repositories;

public class ClienteRepository(AppDbContext context) : IClienteRepository
{
    private readonly AppDbContext _context = context;

    public async Task Atualizar(Cliente cliente, CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Criar(Cliente cliente, CancellationToken cancellationToken)
    {
        await _context.AddAsync(cliente, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task<Cliente?> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Clientes.FindAsync([id], cancellationToken);
        //return await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id && c.Ativo == true, cancellationToken);
    }

    public async Task<IEnumerable<Cliente>> ObterTodos(CancellationToken cancellationToken)
    {
        return await _context.Clientes.AsNoTracking().ToListAsync(cancellationToken);
    }
}
