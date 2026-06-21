using RaizesDoNordeste.Application.DTOs.Requests.Cliente;
using RaizesDoNordeste.Application.DTOs.Responses.Cliente;

namespace RaizesDoNordeste.Application.Interfaces;

public interface IClienteService
{
    Task Criar(CriarClienteRequest request, CancellationToken cancellationToken);
    Task<ClienteObterPorIdResponse?> ObterPorId(Guid id, CancellationToken cancellationToken);
    Task<ClienteObterTodosResponse> ObterTodos(CancellationToken cancellationToken);
    Task Deletar(Guid id, CancellationToken cancellationToken);
    Task Atualizar(Guid id, AtualizarClienteRequest request, CancellationToken cancellationToken);
}
