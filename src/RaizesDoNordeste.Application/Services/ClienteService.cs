using MapsterMapper;
using RaizesDoNordeste.Application.DTOs.Requests.Cliente;
using RaizesDoNordeste.Application.DTOs.Responses.Cliente;
using RaizesDoNordeste.Application.Interfaces;
using RaizesDoNordeste.CrossCutting.Exceptions;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.Application.Services;

public class ClienteService(IClienteRepository clienteRepository, IMapper mapper) : IClienteService
{
    public async Task Atualizar(Guid id, AtualizarClienteRequest request, CancellationToken cancellationToken)
    {
        Cliente? cliente = await clienteRepository.ObterPorId(id, cancellationToken) ?? throw new NotFoundException(nameof(Unidade), id);

        cliente.Atualizar(
            request.Nome,
            request.Email,
            request.Telefone,
            request.Documento,
            request.Observacao,
            request.Ativo,
            request.DataNascimento);

        await clienteRepository.Atualizar(cliente, cancellationToken);
    }

    public async Task Criar(CriarClienteRequest request, CancellationToken cancellationToken)
    {
        Cliente cliente = mapper.Map<Cliente>(request);
        await clienteRepository.Criar(cliente, cancellationToken);
    }

    public async Task Deletar(Guid id, CancellationToken cancellationToken)
    {
        Cliente? cliente = await clienteRepository.ObterPorId(id, cancellationToken) ?? throw new NotFoundException(nameof(Unidade), id);

        cliente.Desativar();

        await clienteRepository.Atualizar(cliente, cancellationToken);
    }
    public async Task<ClienteObterPorIdResponse?> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        //TODO IMPLEMENTAR VALIDATION
        Cliente? cliente = await clienteRepository.ObterPorId(id, cancellationToken);

        if (cliente is null)
        {
            return null;
        }

        return mapper.Map<ClienteObterPorIdResponse>(cliente);

    }
    public async Task<ClienteObterTodosResponse> ObterTodos(CancellationToken cancellationToken)
    {
        IEnumerable<Cliente> clientes =  await clienteRepository.ObterTodos(cancellationToken);

        return new ClienteObterTodosResponse
        {
            Clientes = mapper.Map<IEnumerable<ClienteObterTodosModel>>(clientes)
        };
    }
}
