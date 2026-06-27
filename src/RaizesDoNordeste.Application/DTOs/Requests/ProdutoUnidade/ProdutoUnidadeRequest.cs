namespace RaizesDoNordeste.Application.DTOs.Requests.ProdutoUnidade;

public class ProdutoUnidadeRequest
{
    public Guid ProdutoId { get; set; }
    public decimal EstoqueDisponivel { get; set; }
    public decimal EstoqueReservado { get; set; }
    public bool Ativo { get; set; }
}
