namespace RaizesDoNordeste.Application.DTOs.Requests.Unidade;

public class CriarUnidadeRequest
{
    public string NomeFantasia { get; set; } = string.Empty;
    public string RazaoSocial { get; set; } = string.Empty;
    public string CNPJ { get; set; } = string.Empty;
    public string IE { get; set; } = string.Empty;
}
