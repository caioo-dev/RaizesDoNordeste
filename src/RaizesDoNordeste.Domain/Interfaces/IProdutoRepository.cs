using RaizesDoNordeste.Domain.Entities;

namespace RaizesDoNordeste.Domain.Interfaces;

public interface IProdutoRepository
{
    Task<Produto?> ObterPorId(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Produto>> ObterTodos(CancellationToken cancellationToken);
    Task<bool> ExistePorId(Guid id, CancellationToken cancellationToken);
    Task Criar(Produto produto, CancellationToken cancellationToken);
    Task Atualizar(Produto produto, CancellationToken cancellationToken);
    Task Excluir(Produto produto, CancellationToken cancellationToken);
}
