using Mapster;
using RaizesDoNordeste.Application.DTOs.Requests.Cardapio;
using RaizesDoNordeste.Application.DTOs.Requests.Cliente;
using RaizesDoNordeste.Application.DTOs.Requests.Produto;
using RaizesDoNordeste.Application.DTOs.Responses.Cardapio;
using RaizesDoNordeste.Application.DTOs.Responses.Cliente;
using RaizesDoNordeste.Application.DTOs.Responses.Produto;
using RaizesDoNordeste.Domain.Entities;

namespace RaizesDoNordeste.Application.MapsterConfig;

public static class MapsterConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<Cliente, ClienteObterPorIdResponse>
            .NewConfig();

        TypeAdapterConfig<Cliente, ClienteObterTodosModel>
            .NewConfig();

        TypeAdapterConfig<CriarClienteRequest, Cliente>
            .NewConfig()
            .ConstructUsing(src => new Cliente(
                src.Nome,
                src.Email,
                src.DataNascimento,
                src.Telefone,
                src.Observacao,
                src.Documento));

        TypeAdapterConfig<Cardapio, CardapioObterPorIdResponse>
            .NewConfig();

        TypeAdapterConfig<Cardapio, CardapioObterTodosModel>
            .NewConfig();

        TypeAdapterConfig<CardapioRequest, Cardapio>
            .NewConfig()
            .ConstructUsing(src => new Cardapio(
                src.UnidadeId,
                src.Nome));

        TypeAdapterConfig<Produto, ProdutoObterPorIdResponse>
            .NewConfig();

        TypeAdapterConfig<Produto, ProdutoObterTodosModel>
            .NewConfig();

        TypeAdapterConfig<ProdutoRequest, Produto>
            .NewConfig()
            .ConstructUsing(src => new Produto(src.Nome));
    }
}
