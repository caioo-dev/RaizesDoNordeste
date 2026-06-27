namespace RaizesDoNordeste.Application.DTOs.Responses.Produto;

public class ProdutoObterPorIdResponse
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public bool Ativo { get; set; }
    public DateTime DataInclusao { get; set; }
}
