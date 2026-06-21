using RaizesDoNordeste.Domain.Entities;

namespace RaizesDoNordeste.Domain.Interfaces;

public interface IUnidadeRepository
{
    Task<Unidade?> ObterPorId(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Unidade>> ObterTodos(CancellationToken cancellationToken);
    Task Criar(Unidade unidade, CancellationToken cancellationToken);
    Task Atualizar(Unidade unidade, CancellationToken cancellationToken);
}
