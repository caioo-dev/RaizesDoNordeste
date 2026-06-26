using MapsterMapper;
using RaizesDoNordeste.Application.DTOs.Requests.Cardapio;
using RaizesDoNordeste.Application.DTOs.Responses.Cardapio;
using RaizesDoNordeste.Application.Interfaces;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Exceptions;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.Application.Services;

public class CardapioService(
    ICardapioRepository cardapioRepository,
    IMapper mapper,
    IUnidadeRepository unidadeRepository) : ICardapioService
{
    public async Task<CardapioObterPorIdResponse?> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        Cardapio? cardapio = await cardapioRepository.ObterPorId(id, cancellationToken);
        if (cardapio is null)
        {
            return null;
        }    

        return mapper.Map<CardapioObterPorIdResponse>(cardapio);
    }

    public async Task<CardapioObterTodosResponse> ObterTodos(CancellationToken cancellationToken)
    {
        IEnumerable<Cardapio> cardapios = await cardapioRepository.ObterTodos(cancellationToken);

        return new CardapioObterTodosResponse
        {
            Cardapios = mapper.Map<IEnumerable<CardapioObterTodosModel>>(cardapios)
        };
    }

    public async Task Criar(CardapioRequest request, CancellationToken cancellationToken)
    {
        await ValidarUnidade(request.UnidadeId, cancellationToken);

        Cardapio cardapio = mapper.Map<Cardapio>(request);
        await cardapioRepository.Criar(cardapio, cancellationToken);
    }

    public async Task Atualizar(Guid id, CardapioRequest request, CancellationToken cancellationToken)
    {
        Cardapio cardapio = await cardapioRepository.ObterPorId(id, cancellationToken)
            ?? throw new NotFoundException(nameof(Cardapio), id);

        await ValidarUnidade(request.UnidadeId, cancellationToken);

        cardapio.Atualizar(request.UnidadeId, request.Nome, request.Ativo);

        await cardapioRepository.Salvar(cardapio, cancellationToken);
    }

    public async Task Excluir(Guid id, CancellationToken cancellationToken)
    {
        Cardapio cardapio = await cardapioRepository.ObterPorId(id, cancellationToken)
            ?? throw new NotFoundException(nameof(Cardapio), id);

        cardapio.Excluir();

        await cardapioRepository.Salvar(cardapio, cancellationToken);
    }

    private async Task ValidarUnidade(Guid unidadeId, CancellationToken cancellationToken)
    {
        bool existe = await unidadeRepository.ExistePorId(unidadeId, cancellationToken);
        if (!existe)
        {
            throw new NotFoundException(nameof(Unidade), unidadeId);
        }
    }
}
