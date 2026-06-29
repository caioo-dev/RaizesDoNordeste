using RaizesDoNordeste.Domain.Entities;

namespace RaizesDoNordeste.Domain.Interfaces;

public interface IClienteFidelizacaoRepository
{
    Task<ClienteFidelizacao?> ObterPorClienteId(Guid clienteId, CancellationToken cancellationToken);
    Task<ClienteFidelizacao?> ObterPorId(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<ClienteFidelizacao>> ObterTodos(CancellationToken cancellationToken);
    Task<bool> ExistePorClienteId(Guid clienteId, CancellationToken cancellationToken);
    Task Criar(ClienteFidelizacao clienteFidelizacao, CancellationToken cancellationToken);
    Task Atualizar(ClienteFidelizacao clienteFidelizacao, CancellationToken cancellationToken);
}

public interface IMovimentacaoPontoRepository
{
    Task<IEnumerable<MovimentacaoPonto>> ObterPorClienteFidelizacao(Guid clienteFidelizacaoId, CancellationToken cancellationToken);
}
