using Microsoft.EntityFrameworkCore;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.Infrastructure.Repositories;

public class ClienteFidelizacaoRepository(AppDbContext context) : IClienteFidelizacaoRepository
{
    public async Task<ClienteFidelizacao?> ObterPorClienteId(Guid clienteId, CancellationToken cancellationToken)
        => await context.ClientesFidelizacao
            .Include(cf => cf.Cliente)
            .Include(cf => cf.Movimentacoes)
            .FirstOrDefaultAsync(cf => cf.ClienteId == clienteId, cancellationToken);

    public async Task<ClienteFidelizacao?> ObterPorId(Guid id, CancellationToken cancellationToken)
        => await context.ClientesFidelizacao
            .Include(cf => cf.Cliente)
            .Include(cf => cf.Movimentacoes)
            .FirstOrDefaultAsync(cf => cf.Id == id, cancellationToken);

    public async Task<IEnumerable<ClienteFidelizacao>> ObterTodos(CancellationToken cancellationToken)
        => await context.ClientesFidelizacao
            .Include(cf => cf.Cliente)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task<bool> ExistePorClienteId(Guid clienteId, CancellationToken cancellationToken)
        => await context.ClientesFidelizacao
            .AnyAsync(cf => cf.ClienteId == clienteId, cancellationToken);

    public async Task Criar(ClienteFidelizacao clienteFidelizacao, CancellationToken cancellationToken)
    {
        await context.ClientesFidelizacao.AddAsync(clienteFidelizacao, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Atualizar(ClienteFidelizacao clienteFidelizacao, CancellationToken cancellationToken)
        => await context.SaveChangesAsync(cancellationToken);
}

// Infrastructure/Repositories/MovimentacaoPontoRepository.cs
public class MovimentacaoPontoRepository(AppDbContext context) : IMovimentacaoPontoRepository
{
    public async Task<IEnumerable<MovimentacaoPonto>> ObterPorClienteFidelizacao(Guid clienteFidelizacaoId, CancellationToken cancellationToken)
        => await context.MovimentacoesPontos
            .Where(mp => mp.ClienteFidelizacaoId == clienteFidelizacaoId)
            .AsNoTracking()
            .OrderByDescending(mp => mp.DataMovimentacao)
            .ToListAsync(cancellationToken);
}
