using RaizesDoNordeste.Domain.Entities;

namespace RaizesDoNordeste.Domain.Interfaces;

public interface IClienteRepository
{
    Task Criar(Cliente cliente, CancellationToken cancellationToken);
    Task<Cliente> ObterPorId(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Cliente>> ObterTodos(CancellationToken cancellationToken);
    Task Deletar(Guid id, CancellationToken cancellationToken);

}
