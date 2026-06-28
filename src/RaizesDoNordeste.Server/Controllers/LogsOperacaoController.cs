using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaizesDoNordeste.Application.DTOs.Responses.LogOperacao;
using RaizesDoNordeste.Application.Interfaces;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.Server.Controllers;

[ApiController]
[Authorize]
[Route("api/logs")]
public class LogsOperacaoController(ILogOperacaoRepository logOperacaoRepository) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<LogOperacaoResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterPorEntidade(
        [FromQuery] string entidade,
        [FromQuery] Guid entidadeId,
        CancellationToken cancellationToken)
    {
        IEnumerable<LogOperacao> logs = await logOperacaoRepository.ObterPorEntidade(entidade, entidadeId, cancellationToken);

        IEnumerable<LogOperacaoResponse> response = logs.Select(l => new LogOperacaoResponse
        {
            Id = l.Id,
            UsuarioId = l.UsuarioId,
            Entidade = l.Entidade,
            EntidadeId = l.EntidadeId,
            Acao = l.Acao,
            DadosJson = l.DadosJson,
            DataOperacao = l.DataOperacao
        });

        return Ok(response);
    }
}
