using Mapster;
using RaizesDoNordeste.Application.DTOs.Requests.Cliente;
using RaizesDoNordeste.Application.DTOs.Responses.Cliente;
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
    }
}
