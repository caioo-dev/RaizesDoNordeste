namespace RaizesDoNordeste.Application.DTOs.Requests.CardapioProduto;

public class CriarCardapioProdutoRequest
{
    public Guid ProdutoId { get; set; }
    public decimal PrecoVenda { get; set; }
}
