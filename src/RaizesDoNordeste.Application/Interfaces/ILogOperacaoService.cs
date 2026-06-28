using RaizesDoNordeste.Domain.Enums;

namespace RaizesDoNordeste.Application.Interfaces;

public interface ILogOperacaoService
{
    Task Registrar<T>(Guid entidadeId, TipoAcaoLog acao, T dados, CancellationToken cancellationToken);
}
