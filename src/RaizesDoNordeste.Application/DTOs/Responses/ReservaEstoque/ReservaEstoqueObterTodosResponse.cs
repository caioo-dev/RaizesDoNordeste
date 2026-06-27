using RaizesDoNordeste.Domain.Enums;

namespace RaizesDoNordeste.Application.DTOs.Responses.ReservaEstoque;

public class ReservaEstoqueObterTodosResponse
{
    public IEnumerable<ReservaEstoqueObterTodosModel> Reservas { get; set; } = [];
}

public class ReservaEstoqueObterTodosModel
{
    public Guid Id { get; set; }
    public Guid ProdutoId { get; set; }
    public string NomeProduto { get; set; } = string.Empty;
    public Guid UnidadeId { get; set; }
    public string NomeFantasiaUnidade { get; set; } = string.Empty;
    public decimal Quantidade { get; set; }
    public StatusReservaEstoque Status { get; set; }
    public DateTime DataExpiracao { get; set; }
}
