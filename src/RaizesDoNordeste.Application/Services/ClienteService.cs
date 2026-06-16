using FluentValidation;
using FluentValidation.Results;
using RaizesDoNordeste.Application.DTOs.Requests.Cliente;
using RaizesDoNordeste.Application.Interfaces;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.Application.Services;

public class ClienteService(IClienteRepository clienteRepository, IValidator<CriarClienteRequest> validator) : IClienteService
{
    public async Task Criar(CriarClienteRequest request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var cliente = new Cliente(
            request.Nome,
            request.Email,
            request.DataNascimento,
            request.Telefone,
            request.Observacao,
            request.Documento);

        await clienteRepository.Criar(cliente, cancellationToken);
    }

    public Task Deletar(Guid id, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<Cliente> ObterPorId(Guid id, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<IEnumerable<Cliente>> ObterTodos(CancellationToken cancellationToken) => throw new NotImplementedException();
}
