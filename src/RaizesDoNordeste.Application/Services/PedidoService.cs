using RaizesDoNordeste.Application.DTOs.Requests.Pedido;
using RaizesDoNordeste.Application.DTOs.Responses.Pedido;
using RaizesDoNordeste.Application.Interfaces;
using RaizesDoNordeste.CrossCutting.Exceptions;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Enums;
using RaizesDoNordeste.Domain.Interfaces;
using RaizesDoNordeste.Domain.ValueObjects;

namespace RaizesDoNordeste.Application.Services;

public class PedidoService(
    IPedidoRepository pedidoRepository,
    IClienteRepository clienteRepository,
    IUnidadeRepository unidadeRepository,
    ICardapioProdutoRepository cardapioProdutoRepository,
    IPedidoStatusHistoricoService pedidoStatusHistoricoService,
    IClienteFidelizacaoRepository clienteFidelizacaoRepository) : IPedidoService
{
    public async Task<PedidoObterPorIdResponse?> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        Pedido? pedido = await pedidoRepository.ObterPorId(id, cancellationToken);
        if (pedido is null)
        {
            return null;
        }

        return MapToObterPorIdResponse(pedido);
    }

    public async Task<PedidoObterTodosResponse> ObterTodos(CancellationToken cancellationToken)
    {
        IEnumerable<Pedido> pedidos = await pedidoRepository.ObterTodos(cancellationToken);

        return new PedidoObterTodosResponse
        {
            Pedidos = pedidos.Select(MapToObterTodosModel)
        };
    }

    public async Task Criar(Guid usuarioId, CriarPedidoRequest request, CancellationToken cancellationToken)
    {
        bool clienteExiste = await clienteRepository.ExistePorId(request.ClienteId, cancellationToken);
        if (!clienteExiste)
        {
            throw new NotFoundException(nameof(Cliente), request.ClienteId);
        }

        bool unidadeExiste = await unidadeRepository.ExistePorId(request.UnidadeId, cancellationToken);
        if (!unidadeExiste)
        {
            throw new NotFoundException(nameof(Unidade), request.UnidadeId);
        }

        var endereco = new EnderecoEntrega(
            request.EnderecoEntrega.Logradouro,
            request.EnderecoEntrega.Numero,
            request.EnderecoEntrega.Bairro,
            request.EnderecoEntrega.Cep);

        var pedido = new Pedido(request.ClienteId, usuarioId, request.UnidadeId, endereco);

        foreach (PedidoProdutoRequest item in request.Produtos)
        {
            CardapioProduto? cardapioProduto = await cardapioProdutoRepository
                .ObterPorCardapioEProduto(request.CardapioId, item.ProdutoId, cancellationToken) ?? throw new NotFoundException($"Produto '{item.ProdutoId}' não encontrado no cardápio.");

            if (!cardapioProduto.Disponivel)
            {
                throw new ConflictException($"Produto '{cardapioProduto.Produto?.Nome}' não está disponível no cardápio.");
            }

            var pedidoProduto = new PedidoProduto(
                pedido.Id,
                item.ProdutoId,
                cardapioProduto.PrecoVenda,
                item.Quantidade);

            pedido.PedidosProdutos.Add(pedidoProduto);
        }

        pedido.CalcularTotal(); 

        await pedidoRepository.Criar(pedido, cancellationToken);

        ClienteFidelizacao? fidelizacao = await clienteFidelizacaoRepository
            .ObterPorClienteId(request.ClienteId, cancellationToken);

        if (fidelizacao is not null && fidelizacao.ConsentimentoLGPD)
        {
            fidelizacao.AcumularPontos(pedido.Total, $"Compra do pedido #{pedido.Id}");
            await clienteFidelizacaoRepository.Atualizar(fidelizacao, cancellationToken);
        }
    }

    public async Task Confirmar(Guid id, Guid usuarioId, CancellationToken cancellationToken)
    {
        Pedido pedido = await pedidoRepository.ObterPorId(id, cancellationToken)
            ?? throw new NotFoundException(nameof(Pedido), id);

        PedidoStatus statusAnterior = pedido.Status;

        pedido.Confirmar();
        await pedidoRepository.Atualizar(pedido, cancellationToken);

        await pedidoStatusHistoricoService.Registrar(pedido.Id, statusAnterior, pedido.Status, usuarioId, null, cancellationToken);
    }

    public async Task SairParaEntrega(Guid id, Guid usuarioId, CancellationToken cancellationToken)
    {
        Pedido pedido = await pedidoRepository.ObterPorId(id, cancellationToken)
            ?? throw new NotFoundException(nameof(Pedido), id);

        PedidoStatus statusAnterior = pedido.Status;

        pedido.SairParaEntrega();
        await pedidoRepository.Atualizar(pedido, cancellationToken);

        await pedidoStatusHistoricoService.Registrar(pedido.Id, statusAnterior, pedido.Status, usuarioId, null, cancellationToken);
    }

    public async Task Entregar(Guid id, Guid usuarioId, CancellationToken cancellationToken)
    {
        Pedido pedido = await pedidoRepository.ObterPorId(id, cancellationToken)
            ?? throw new NotFoundException(nameof(Pedido), id);

        PedidoStatus statusAnterior = pedido.Status;

        pedido.Entregar();
        await pedidoRepository.Atualizar(pedido, cancellationToken);

        await pedidoStatusHistoricoService.Registrar(pedido.Id, statusAnterior, pedido.Status, usuarioId, null, cancellationToken);
    }

    public async Task Cancelar(Guid id, Guid usuarioId, CancellationToken cancellationToken)
    {
        Pedido pedido = await pedidoRepository.ObterPorId(id, cancellationToken)
            ?? throw new NotFoundException(nameof(Pedido), id);

        PedidoStatus statusAnterior = pedido.Status;

        pedido.Cancelar();
        await pedidoRepository.Atualizar(pedido, cancellationToken);

        await pedidoStatusHistoricoService.Registrar(pedido.Id, statusAnterior, pedido.Status, usuarioId, null, cancellationToken);
    }

    private static PedidoObterPorIdResponse MapToObterPorIdResponse(Pedido p) => new()
    {
        Id = p.Id,
        ClienteId = p.ClienteId,
        NomeCliente = p.Cliente.Nome,
        UnidadeId = p.UnidadeId,
        NomeUnidade = p.Unidade.NomeFantasia,
        UsuarioId = p.UsuarioId,
        Status = p.Status,
        Total = p.Total,
        DataEntrega = p.DataEntrega,
        DataInclusao = p.DataInclusao,
        EnderecoEntrega = new EnderecoEntregaResponse
        {
            Logradouro = p.EnderecoEntrega.Logradouro,
            Numero = p.EnderecoEntrega.Numero,
            Bairro = p.EnderecoEntrega.Bairro,
            Cep = p.EnderecoEntrega.Cep
        },
        Produtos = p.PedidosProdutos.Select(pp => new PedidoProdutoResponse
        {
            Id = pp.Id,
            ProdutoId = pp.ProdutoId,
            NomeProduto = pp.Produto.Nome,
            PrecoUnitario = pp.PrecoUnitario,
            Quantidade = pp.Quantidade,
            ValorTotal = pp.ValorTotal
        }).ToList()
    };
    private static PedidoObterTodosModel MapToObterTodosModel(Pedido p) => new()
    {
        Id = p.Id,
        ClienteId = p.ClienteId,
        NomeCliente = p.Cliente.Nome,
        UnidadeId = p.UnidadeId,
        Status = p.Status,
        Total = p.Total,
        DataInclusao = p.DataInclusao
    };
}
