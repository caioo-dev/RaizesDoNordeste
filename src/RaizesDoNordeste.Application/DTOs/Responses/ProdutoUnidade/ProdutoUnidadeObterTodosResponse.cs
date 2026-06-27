namespace RaizesDoNordeste.Application.DTOs.Responses.ProdutoUnidade;

public class ProdutoUnidadeObterTodosResponse
{
    public IEnumerable<ProdutoUnidadeObterTodosModel> Produtos { get; set; } = [];
}

public class ProdutoUnidadeObterTodosModel
{
    public Guid Id { get; set; }
    public Guid ProdutoId { get; set; }
    public string NomeProduto { get; set; } = string.Empty;
    public decimal EstoqueDisponivel { get; set; }
    public decimal EstoqueReservado { get; set; }
    public bool Ativo { get; set; }
}
