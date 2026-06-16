using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.Infrastructure.Repositories;

public class ClienteRepository(AppDbContext context) : IClienteRepository
{
    private readonly AppDbContext _context = context;

    public async Task Criar(Cliente cliente, CancellationToken cancellationToken)
    {
        await _context.AddAsync(cliente, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
    public Task Deletar(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    public Task<Cliente> ObterPorId(Guid id, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<IEnumerable<Cliente>> ObterTodos(CancellationToken cancellationToken) => throw new NotImplementedException();
}
