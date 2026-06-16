using RaizesDoNordeste.Application.DTOs.Requests.Cliente;
using RaizesDoNordeste.Domain.Entities;

namespace RaizesDoNordeste.Application.Interfaces;

public interface IClienteService
{
    Task Criar(CriarClienteRequest request, CancellationToken cancellationToken);
    Task<Cliente> ObterPorId(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Cliente>> ObterTodos(CancellationToken cancellationToken);
    Task Deletar(Guid id, CancellationToken cancellationToken);
}
