using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using RaizesDoNordeste.Application.Interfaces;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Enums;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.Application.Services;

public class LogOperacaoService(
    ILogOperacaoRepository logOperacaoRepository,
    IHttpContextAccessor httpContextAccessor) : ILogOperacaoService
{
    public async Task Registrar<T>(Guid entidadeId, TipoAcaoLog acao, T dados, CancellationToken cancellationToken)
    {
        Guid usuarioId = ObterUsuarioId();
        string entidade = typeof(T).Name;
        string dadosJson = JsonSerializer.Serialize(dados);

        var log = new LogOperacao(usuarioId, entidade, entidadeId, acao, dadosJson);

        await logOperacaoRepository.Registrar(log, cancellationToken);
    }

    private Guid ObterUsuarioId()
    {
        string? sub = httpContextAccessor.HttpContext?.User
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (sub is null || !Guid.TryParse(sub, out Guid usuarioId))
        {
            return Guid.Empty;
        }

        return usuarioId;
    }
}
