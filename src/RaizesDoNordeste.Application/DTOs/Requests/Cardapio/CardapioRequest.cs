namespace RaizesDoNordeste.Application.DTOs.Requests.Cardapio;

public class CardapioRequest
{
    public Guid UnidadeId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public bool Ativo { get; set; }
}
