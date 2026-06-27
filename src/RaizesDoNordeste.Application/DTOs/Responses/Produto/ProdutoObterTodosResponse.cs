namespace RaizesDoNordeste.Application.DTOs.Responses.Produto;

public class ProdutoObterTodosResponse
{
    public IEnumerable<ProdutoObterTodosModel> Produtos { get; set; } = [];
}

public class ProdutoObterTodosModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public bool Ativo { get; set; }
}
