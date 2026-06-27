namespace RaizesDoNordeste.Application.DTOs.Responses.CardapioProduto;

public class CardapioProdutoObterTodosResponse
{
    public IEnumerable<CardapioProdutoObterTodosModel> Produtos { get; set; } = [];
}

public class CardapioProdutoObterTodosModel
{
    public Guid Id { get; set; }
    public Guid ProdutoId { get; set; }
    public string NomeProduto { get; set; } = string.Empty;
    public decimal PrecoVenda { get; set; }
    public bool Disponivel { get; set; }
}
