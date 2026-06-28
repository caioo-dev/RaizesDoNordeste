using MapsterMapper;
using RaizesDoNordeste.Application.DTOs.Requests.Produto;
using RaizesDoNordeste.Application.DTOs.Responses.Produto;
using RaizesDoNordeste.Application.Interfaces;
using RaizesDoNordeste.CrossCutting.Exceptions;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Enums;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.Application.Services;

public class ProdutoService(
    IProdutoRepository produtoRepository,
    IMapper mapper,
    ILogOperacaoService logOperacaoService) : IProdutoService
{
    public async Task<ProdutoObterPorIdResponse?> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        Produto? produto = await produtoRepository.ObterPorId(id, cancellationToken);
        if (produto is null)
        {
            return null;
        }       

        return mapper.Map<ProdutoObterPorIdResponse>(produto);
    }

    public async Task<ProdutoObterTodosResponse> ObterTodos(CancellationToken cancellationToken)
    {
        IEnumerable<Produto> produtos = await produtoRepository.ObterTodos(cancellationToken);

        return new ProdutoObterTodosResponse
        {
            Produtos = mapper.Map<IEnumerable<ProdutoObterTodosModel>>(produtos)
        };
    }

    public async Task Criar(ProdutoRequest request, CancellationToken cancellationToken)
    {
        Produto produto = mapper.Map<Produto>(request);
        await produtoRepository.Criar(produto, cancellationToken);

        await logOperacaoService.Registrar(produto.Id, TipoAcaoLog.Criacao, produto, cancellationToken);
    }

    public async Task Atualizar(Guid id, ProdutoRequest request, CancellationToken cancellationToken)
    {
        Produto produto = await produtoRepository.ObterPorId(id, cancellationToken)
            ?? throw new NotFoundException(nameof(Produto), id);

        produto.Atualizar(request.Nome, request.Ativo);

        await produtoRepository.Atualizar(produto, cancellationToken);

        await logOperacaoService.Registrar(produto.Id, TipoAcaoLog.Atualizacao, produto, cancellationToken);
    }

    public async Task Excluir(Guid id, CancellationToken cancellationToken)
    {
        Produto produto = await produtoRepository.ObterPorId(id, cancellationToken)
            ?? throw new NotFoundException(nameof(Produto), id);

        produto.Excluir();

        await produtoRepository.Excluir(produto, cancellationToken);

        await logOperacaoService.Registrar(produto.Id, TipoAcaoLog.Exclusao, produto, cancellationToken);
    }
}
