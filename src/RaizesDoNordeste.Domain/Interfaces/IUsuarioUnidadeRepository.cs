using RaizesDoNordeste.Domain.Entities;

namespace RaizesDoNordeste.Domain.Interfaces;

public interface IUsuarioUnidadeRepository
{
    Task<UsuarioUnidade?> ObterVinculo(Guid usuarioId, Guid unidadeId, CancellationToken cancellationToken);
    Task<IEnumerable<UsuarioUnidade>> ObterPorUsuario(Guid usuarioId, CancellationToken cancellationToken);
    Task Vincular(UsuarioUnidade usuarioUnidade, CancellationToken cancellationToken);
    Task Desvincular(UsuarioUnidade usuarioUnidade, CancellationToken cancellationToken);
}
