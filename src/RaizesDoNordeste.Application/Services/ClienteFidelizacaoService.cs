using RaizesDoNordeste.Application.DTOs.Requests.ClienteFidelizacao;
using RaizesDoNordeste.Application.DTOs.Responses.ClienteFidelizacao;
using RaizesDoNordeste.Application.Interfaces;
using RaizesDoNordeste.CrossCutting.Exceptions;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.Application.Services;

public class ClienteFidelizacaoService(
    IClienteFidelizacaoRepository clienteFidelizacaoRepository,
    IMovimentacaoPontoRepository movimentacaoPontoRepository,
    IClienteRepository clienteRepository) : IClienteFidelizacaoService
{
    public async Task<ClienteFidelizacaoObterResponse?> ObterPorClienteId(Guid clienteId, CancellationToken cancellationToken)
    {
        ClienteFidelizacao? fidelizacao = await clienteFidelizacaoRepository.ObterPorClienteId(clienteId, cancellationToken);
        if (fidelizacao is null)
        {
            return null;
        }

        return MapToObterResponse(fidelizacao);
    }

    public async Task<ClienteFidelizacaoComMovimentacoesResponse?> ObterComMovimentacoes(Guid clienteId, CancellationToken cancellationToken)
    {
        ClienteFidelizacao? fidelizacao = await clienteFidelizacaoRepository.ObterPorClienteId(clienteId, cancellationToken);
        if (fidelizacao is null)
        {
            return null;
        }

        IEnumerable<MovimentacaoPonto> movimentacoes = await movimentacaoPontoRepository.ObterPorClienteFidelizacao(fidelizacao.Id, cancellationToken);

        var response = new ClienteFidelizacaoComMovimentacoesResponse
        {
            Cliente = MapToObterResponse(fidelizacao)
        };

        foreach (MovimentacaoPonto mov in movimentacoes)
        {
            response.Movimentacoes.Add(new MovimentacaoPontoResponse
            {
                Id = mov.Id,
                Tipo = mov.Tipo,
                Pontos = mov.Pontos,
                Descricao = mov.Descricao,
                DataMovimentacao = mov.DataMovimentacao
            });
        }

        return response;
    }

    public async Task Aderir(AderirProgramaFidelizacaoRequest request, CancellationToken cancellationToken)
    {
        bool clienteExiste = await clienteRepository.ExistePorId(request.ClienteId, cancellationToken);
        if (!clienteExiste)
        {
            throw new NotFoundException(nameof(Cliente), request.ClienteId);
        }

        bool jaAderiuAoPrograma = await clienteFidelizacaoRepository.ExistePorClienteId(request.ClienteId, cancellationToken);
        if (jaAderiuAoPrograma)
        {
            throw new ConflictException("Cliente já aderiu ao programa de fidelização.");
        }

        if (!request.ConsentimentoLGPD)
        {
            throw new ConflictException("É necessário aceitar o consentimento de LGPD para aderir ao programa.");
        }

        var clienteFidelizacao = new ClienteFidelizacao(request.ClienteId, request.ConsentimentoLGPD);

        await clienteFidelizacaoRepository.Criar(clienteFidelizacao, cancellationToken);
    }

    public async Task RevogarConsentimentoLGPD(Guid clienteId, CancellationToken cancellationToken)
    {
        ClienteFidelizacao clienteFidelizacao = await clienteFidelizacaoRepository.ObterPorClienteId(clienteId, cancellationToken)
            ?? throw new NotFoundException(nameof(ClienteFidelizacao), clienteId);

        clienteFidelizacao.RevogarConsentimentoLGPD();
        await clienteFidelizacaoRepository.Atualizar(clienteFidelizacao, cancellationToken);
    }

    private static ClienteFidelizacaoObterResponse MapToObterResponse(ClienteFidelizacao cf) => new()
    {
        Id = cf.Id,
        ClienteId = cf.ClienteId,
        NomeCliente = cf.Cliente.Nome,
        PontosDisponiveis = cf.PontosDisponiveis,
        Nivel = cf.Nivel,
        TotalPontosAcumulados = cf.TotalPontosAcumulados,
        ConsentimentoLGPD = cf.ConsentimentoLGPD,
        DataAdesao = cf.DataAdesao,
        DataUltimaAtualizacaoNivel = cf.DataUltimaAtualizacaoNivel
    };
}
