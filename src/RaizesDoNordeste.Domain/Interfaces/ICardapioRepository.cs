using RaizesDoNordeste.Domain.Entities;

namespace RaizesDoNordeste.Domain.Interfaces;

public interface ICardapioRepository
{
    Task<Cardapio?> ObterPorId(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Cardapio>> ObterTodos(CancellationToken cancellationToken);
    Task Criar(Cardapio cardapio, CancellationToken cancellationToken);
    Task Salvar(Cardapio cardapio, CancellationToken cancellationToken);
}
