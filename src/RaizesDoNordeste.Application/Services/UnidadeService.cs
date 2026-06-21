using RaizesDoNordeste.Application.DTOs.Requests.Unidade;
using RaizesDoNordeste.Application.DTOs.Responses.Unidade;
using RaizesDoNordeste.Application.Interfaces;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Exceptions;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.Application.Services;

public class UnidadeService(IUnidadeRepository unidadeRepository) : IUnidadeService
{
    public async Task Atualizar(Guid id, AtualizarUnidadeRequest request, CancellationToken cancellationToken)
    {
        Unidade? unidade = await unidadeRepository.ObterPorId(id, cancellationToken) ?? throw new NotFoundException(nameof(Unidade), id);

        unidade.Atualizar(
        request.NomeFantasia,
        request.RazaoSocial,
        request.CNPJ,
        request.IE,
        request.Ativo
        );

        await unidadeRepository.Atualizar(unidade, cancellationToken);
    }
    public async Task Criar(CriarUnidadeRequest request, CancellationToken cancellationToken)
    {
        var unidade = new Unidade(
    request.NomeFantasia,
    request.RazaoSocial,
    request.CNPJ,
    request.IE
        );

        await unidadeRepository.Criar(unidade, cancellationToken);
    }
    public async Task Excluir(Guid id, CancellationToken cancellationToken)
    {
        Unidade unidade = await unidadeRepository.ObterPorId(id, cancellationToken)
                    ?? throw new NotFoundException(nameof(Unidade), id);

        unidade.Excluir();

        await unidadeRepository.Atualizar(unidade, cancellationToken);
    }
    public async Task<UnidadeObterPorIdResponse?> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        Unidade? unidade = await unidadeRepository.ObterPorId(id, cancellationToken);
        if (unidade is null)
        {
            return null;
        }

        return new UnidadeObterPorIdResponse
        {
            Id = unidade.Id,
            NomeFantasia = unidade.NomeFantasia,
            RazaoSocial = unidade.RazaoSocial,
            CNPJ = unidade.Cnpj,
            IE = unidade.Ie,
            Ativo = unidade.Ativo
        };
    }
    public async Task<UnidadeObterTodosResponse> ObterTodos(CancellationToken cancellationToken)
    {
        IEnumerable<Unidade> unidades = await unidadeRepository.ObterTodos(cancellationToken);

        return new UnidadeObterTodosResponse
        {
            Unidades = unidades.Select(u => new UnidadeObterTodosModel
            {
                Id = u.Id,
                NomeFantasia = u.NomeFantasia,
                RazaoSocial = u.RazaoSocial,
                CNPJ = u.Cnpj,
                IE = u.Ie,
                Ativo = u.Ativo
            })
        };
    }
}
