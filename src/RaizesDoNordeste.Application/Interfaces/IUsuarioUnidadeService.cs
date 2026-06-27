using RaizesDoNordeste.Application.DTOs.Responses.UsuarioUnidade;

namespace RaizesDoNordeste.Application.Interfaces;

public interface IUsuarioUnidadeService
{
    Task<UsuarioUnidadeResponse> ObterUnidades(Guid usuarioId, CancellationToken cancellationToken);
    Task Vincular(Guid usuarioId, Guid unidadeId, CancellationToken cancellationToken);
    Task Desvincular(Guid usuarioId, Guid unidadeId, CancellationToken cancellationToken);
}
