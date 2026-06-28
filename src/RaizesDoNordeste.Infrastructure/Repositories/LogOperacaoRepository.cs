using Microsoft.EntityFrameworkCore;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.Infrastructure.Repositories;

public class LogOperacaoRepository(AppDbContext context) : ILogOperacaoRepository
{
    public async Task<LogOperacao?> ObterPorId(Guid id, CancellationToken cancellationToken)
        => await context.LogsOperacao
            .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);

    public async Task<IEnumerable<LogOperacao>> ObterPorEntidade(string entidade, Guid entidadeId, CancellationToken cancellationToken)
        => await context.LogsOperacao
            .Where(l => l.Entidade == entidade && l.EntidadeId == entidadeId)
            .AsNoTracking()
            .OrderByDescending(l => l.DataOperacao)
            .ToListAsync(cancellationToken);

    public async Task Registrar(LogOperacao log, CancellationToken cancellationToken)
    {
        await context.LogsOperacao.AddAsync(log, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
