namespace RaizesDoNordeste.Application.DTOs.Responses.Cardapio;

public class CardapioObterPorIdResponse
{
    public Guid Id { get; set; }
    public Guid UnidadeId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public bool Ativo { get; set; }
}
