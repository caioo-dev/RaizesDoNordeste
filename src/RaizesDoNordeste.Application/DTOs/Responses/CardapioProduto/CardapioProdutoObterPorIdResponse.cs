namespace RaizesDoNordeste.Application.DTOs.Responses.CardapioProduto;

public class CardapioProdutoObterPorIdResponse
{
    public Guid Id { get; set; }
    public Guid CardapioId { get; set; }
    public Guid ProdutoId { get; set; }
    public string NomeProduto { get; set; } = string.Empty;
    public decimal PrecoVenda { get; set; }
    public bool Disponivel { get; set; }
}
