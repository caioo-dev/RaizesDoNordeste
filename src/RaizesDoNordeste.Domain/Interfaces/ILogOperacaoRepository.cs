using RaizesDoNordeste.Domain.Entities;

namespace RaizesDoNordeste.Domain.Interfaces;

public interface ILogOperacaoRepository
{
    Task<LogOperacao?> ObterPorId(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<LogOperacao>> ObterPorEntidade(string entidade, Guid entidadeId, CancellationToken cancellationToken);
    Task Registrar(LogOperacao log, CancellationToken cancellationToken);
}
