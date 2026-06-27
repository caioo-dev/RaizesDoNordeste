namespace RaizesDoNordeste.Application.DTOs.Responses.ProdutoUnidade;

public class ProdutoUnidadeObterPorIdResponse
{
    public Guid Id { get; set; }
    public Guid ProdutoId { get; set; }
    public string NomeProduto { get; set; } = string.Empty;
    public Guid UnidadeId { get; set; }
    public decimal EstoqueDisponivel { get; set; }
    public decimal EstoqueReservado { get; set; }
    public bool Ativo { get; set; }
    public DateTime DataInclusao { get; set; }
}
