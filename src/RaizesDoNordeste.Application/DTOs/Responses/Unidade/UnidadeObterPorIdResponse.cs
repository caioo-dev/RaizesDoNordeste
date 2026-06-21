namespace RaizesDoNordeste.Application.DTOs.Responses.Unidade;

public class UnidadeObterPorIdResponse
{
    public Guid Id { get; set; }
    public string NomeFantasia { get; set; } = string.Empty;
    public string RazaoSocial { get; set; } = string.Empty;
    public string CNPJ { get; set; } = string.Empty;
    public string IE { get; set; } = string.Empty;
    public bool Ativo { get; set; }
}
